using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Artco
{
    public class PracticeMode
    {
        public List<Bitmap> bitmap_list { get; set; } = new List<Bitmap>()
                {
                        Properties.Resources.Artco_Right1, Properties.Resources.Artco_Front1,
                        Properties.Resources.Artco_Left1, Properties.Resources.Artco_Back1
                };

        public List<Bitmap> jump_bitmap_list { get; set; } = new List<Bitmap>()
                {
                        Properties.Resources.Artco_Right2, Properties.Resources.Artco_Front2,
                        Properties.Resources.Artco_Left2, Properties.Resources.Artco_Back2
                };

        // 수정 필요 : public
        public string intro_path;
        public List<string> solution = new List<string>();
        public List<(int, int)> map = new List<(int, int)>();
        public ActivatedSprite player;
        public ManualResetEvent finish_event;
        public StagePlayer intro_player;
        public Bunifu.Framework.UI.BunifuImageButton start_btn;

        public void SetGameEnvironment()
        {
            SetGameDatas(GetGameDatas());
            CreatePlayer();
            PlayIntro();
            CreateStartButton();
        }

        public MultiMap<string> GetGameDatas() => Utility.ReadXMLWithLevel("Level" + MainForm.stage_player.GetBackground().level);

        public void SetGameDatas(MultiMap<string> datas)
        {
            if (!string.IsNullOrEmpty(intro_path))
                return;

            intro_path = FileManager.http_root_dir + datas["Intro"][0];

            string[] solution = datas["Solution"][0].Split(',');
            for (int i = 0; i < solution.Length; i++)
                this.solution.Add(solution[i]);

            string[] map = datas["Points"][0].Split(',');
            for (int i = 0; i < map.Length; i += 2)
                this.map.Add((int.Parse(map[i]), int.Parse(map[i + 1])));
        }

        public void CreatePlayer()
        {
            if (player != null) {
                return;
            }

            Sprite sprite = new Sprite("小飞", FileManager.http_root_dir + "themes/default/Artco_Front1.png", false, null);
            MainForm.select_sprite_cb.Invoke(sprite);

            player = ActivatedSpriteController.cur_sprite;
            player.is_visible = false;
            player.org_img_list = ActivatedSpriteController.CloneBitmapList(bitmap_list);
            player.img_list = player.org_img_list;
            player.x = map[0].Item1;
            player.y = map[0].Item2;
            player.arrow = 5;

            MainForm.invalidate_stage_form.Invoke();
        }

        public void PlayIntro()
        {
            intro_player = new StagePlayer(MainForm.draw_background, FinishIntro);
            intro_player.SetBackground(new Background(null, -1, 0, intro_path, null, false));
            intro_player.Start();
        }

        public void CreateStartButton()
        {
            var image = Properties.Resources.PracticeStartBtn;
            start_btn = new Bunifu.Framework.UI.BunifuImageButton {
                Width = image.Width,
                Height = image.Height,
                Image = image,
                BackColor = Color.Transparent,
                Location = new Point(820, 450)
            };
            start_btn.Click += (sender, args) => FinishIntro();

            MainForm.stage_panel.Controls.Add(start_btn);
        }

        public void FinishIntro()
        {
            if (start_btn.InvokeRequired) {
                start_btn.Invoke(new Action(FinishIntro));
            } else {
                MainForm.stage_panel.Controls.Remove(start_btn);
                intro_player.Stop();
                Ready();
            }
        }

        public void Ready()
        {
            player.is_visible = true;
            StagePlayer.SetFlags(StagePlayer.Flag.GAME);
            MainForm.stage_player.SetInitializeImage();
        }

        public bool CheckSolution()
        {
            player.sprite_runner.GetLineFinishEvent(0).WaitOne();
            if (player.code_list[0].Count != solution.Count) {
                MainForm.stop_project?.Invoke();
                return false;
            }

            for (int i = 0; i < player.code_list[0].Count; i++) {
                var code = player.code_list[0][i];
                string[] split = solution[i].Split(':');
                if (!code.name.Equals(split[0])) {
                    MainForm.stop_project?.Invoke();
                    return false;
                }

                if (split.Length > 1) {
                    if (code.values.Length < 1)
                        return false;

                    if (!code.values[0].Equals(split[1]))
                        return false;
                }
            }

            return true;
        }

        public async void StartCheckSolution()
        {
            bool ret = await Task.Run(CheckSolution);
            if (!ret) {
                EffectSound.move_failed_sound.Play();
                return;
            }

            MainForm.show_practice_result_form.Invoke();
        }

        public bool CheckPath(int arrow, int x, int y, int n = 100)
        {
            if (arrow == 5)
                x += n;
            else if (arrow == 6)
                y += n;
            else if (arrow == 7)
                x -= n;
            else if (arrow == 8)
                y -= n;
            else
                return false;

            for (int i = 0; i < map.Count; i++) {
                if (map[i].Item1 == x && map[i].Item2 == y)
                    return true;
            }

            return false;
        }
    }
}