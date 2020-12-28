using System.Threading;

namespace Artco
{
    public class RuntimeSpriteCode
    {
        public bool is_stop_code { get; set; } = false;
        public ManualResetEvent line_finish_event { get; set; } = new ManualResetEvent(false);
        public ActivatedSprite sprite { get; set; }
        public int code_line_idx { get; set; }

        public void RunCode(object sprite)
        {
            MainForm.practice_mode?.StartCheckSolution();
            this.sprite = (ActivatedSprite)sprite;
            if (this.sprite.code_list.Count <= 0)
                return;

            do {
                this.sprite.pc[code_line_idx] = 0;
                line_finish_event.Reset();

                ref var pc = ref this.sprite.pc[code_line_idx];
                for (; pc < this.sprite.code_list[code_line_idx].Count; pc++) {
                    lock (this) {
                        if (is_stop_code)
                            break;
                    }

                    var code = this.sprite.code_list[code_line_idx][pc];
                    Block.funcs[code.name].Invoke(code, new object[] { this.sprite, code_line_idx, code.values });
                    Thread.Sleep(10);
                }

                line_finish_event.Set();

                if (is_stop_code)
                    return;

            } while (this.sprite.code_list[code_line_idx][0].event_type != 0);
        }

        public void StopCode()
        {
            lock (this)
                is_stop_code = true;

            // 신호 기다리는 블럭들을 종료하기 위함
            // 복제 스프라이트는 해당 속성이 없으므로 검사 필요
            if (sprite.wait_signal_obj.Count > 0)
                sprite.wait_signal_obj[code_line_idx].Set();
        }
    }
}