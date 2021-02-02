using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Artco
{
    internal static class RuntimeEnv
    {
        public static bool is_stop { get; set; }
        public static List<RuntimeSprite> runtime_sprites { get; set; } = new List<RuntimeSprite>();
        public static List<Thread> runtime_threads { get; set; } = new List<Thread>();

        public static void RunActivatedSprites()
        {
            is_stop = false;
            foreach (var sprite in ActivatedSpriteController.sprite_list) {
                sprite.sx = sprite.x;
                sprite.sy = sprite.y;
                sprite.sarrow = sprite.arrow;

                sprite.sprite_runner = new RuntimeSprite {
                    sprite_idx = ActivatedSpriteController.sprite_list.IndexOf(sprite)
                };

                runtime_sprites.Add(sprite.sprite_runner);

                Thread thread = new Thread(sprite.sprite_runner.RunSprites);
                runtime_threads.Add(thread);

                thread.IsBackground = true;
                thread.Start();
            }
        }

        public static async void StopActivatedSprites()
        {
            is_stop = true;
            await Task.Run(() => {
                for (int i = 0; i < runtime_sprites.Count; i++)
                    runtime_sprites[i].StopSprites();

                for (int i = 0; i < runtime_threads.Count; i++)
                    runtime_threads[i].Join();
            });

            runtime_sprites.Clear();
            runtime_threads.Clear();
            ActivatedSpriteController.InitActSprites();
            UserVariableManager.InitializeVariables();
            MainForm.finish_act_sprites_cb?.Invoke();
        }

        public static bool ReadCodeValues(ActivatedSprite s)
        {
            int cnt = 0;
            for (int i = 0; i < s.code_list.Count; i++) {
                for (int j = 0; j < s.code_list[i].Count; j++) {
                    var code = s.code_list[i][j];
                    var name = code.name;
                    var controls = code.block_view.controls;

                    if (name.Equals("ControlLoop") || name.Equals("ControlLoopN") ||
                        name.Equals("GameLoopN") || name.Equals("ControlCondition"))
                        cnt++;

                    if (name.Equals("ControlFlag") || name.Equals("GameFlag"))
                        cnt--;

                    if (controls != null) {
                        int length = controls.Count;

                        code.values = new string[length];
                        for (int k = 0; k < length; k++) {
                            controls[k].ThreadSafe(x => code.values[k] = x.Text);
                            if (string.IsNullOrEmpty(code.values[k]))
                                return false;

                            if (code.category == 2 &&
                                !double.TryParse(code.values[k], out double value) &&
                                !UserVariableManager.user_variables.TryGetValue(code.values[k], out UserVariable variable)) {
                                return false;
                            }
                        }
                    }
                }
            }

            if (cnt != 0)
                return false;

            return true;
        }
    }
}