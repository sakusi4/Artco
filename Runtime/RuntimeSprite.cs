using System;
using System.Collections.Generic;
using System.Threading;

namespace Artco
{
    public class RuntimeSprite
    {
        public ManualResetEvent manual_reset_event { get; set; } = new ManualResetEvent(false);
        public List<RuntimeSpriteCode> runtime_sprite_codes { get; set; } = new List<RuntimeSpriteCode>();
        public List<Thread> runtime_sprite_threads { get; set; } = new List<Thread>();

        public int sprite_idx;

        public void RunSprites()
        {
            var sprite = ActivatedSpriteController.GetSprite(sprite_idx);
            sprite.SetPC(sprite.code_list.Count);

            for (int line = 0; line < sprite.code_list.Count; line++) {
                sprite.wait_signal_obj.Add(new ManualResetEvent(false));

                RuntimeSpriteCode runtime_sprite_code = new RuntimeSpriteCode {
                    code_line_idx = line
                };
                runtime_sprite_codes.Add(runtime_sprite_code);

                Thread runtime_sprite_thread = new Thread(new ParameterizedThreadStart(runtime_sprite_code.RunCode));
                runtime_sprite_thread.IsBackground = true;
                runtime_sprite_threads.Add(runtime_sprite_thread);

                runtime_sprite_thread.Start(sprite);
            }

            manual_reset_event.WaitOne();

            // 복제 스프라이트 종료
            for (int idx = 0; idx < sprite.cloned_sprite_list.Count; idx++) {
                var clone = sprite.cloned_sprite_list[idx];
                clone.sprite_runner.runtime_sprite_codes[0].StopCode();
            }
            for (int idx = 0; idx < sprite.cloned_sprite_list.Count; idx++) {
                var clone = sprite.cloned_sprite_list[idx];
                clone.sprite_runner.runtime_sprite_threads[0].Join();
            }

            // 각 코드라인 종료
            for (int idx = 0; idx < runtime_sprite_codes.Count; idx++) {
                runtime_sprite_codes[idx].StopCode();
            }
            for (int idx = 0; idx < runtime_sprite_threads.Count; idx++) {
                runtime_sprite_threads[idx].Join();
            }

            Block.mciSendString("close wav", null, 0, IntPtr.Zero);
        }

        public void StopSprites()
        {
            manual_reset_event.Set();
        }

        public ManualResetEvent GetLineFinishEvent(int line)
        {
            return runtime_sprite_codes[line].line_finish_event;
        }
    }
}