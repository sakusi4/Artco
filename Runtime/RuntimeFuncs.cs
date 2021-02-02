using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Artco
{
    partial class Block
    {
        // Event
        public void EventStart(ActivatedSprite s, int line_num, string[] values) { }
        public void EventRecvSig(ActivatedSprite s, int line_num, string[] values) => WaitForSignal(s, line_num);
        public void EventInputKey(ActivatedSprite s, int line_num, string[] values) => WaitForSignal(s, line_num);
        public void EventTouch(ActivatedSprite s, int line_num, string[] values) => WaitForTouch(s, line_num, values);
        public void EventClickSprite(ActivatedSprite s, int line_num, string[] values) => WaitForSignal(s, line_num);
        public void EventClone(ActivatedSprite s, int line_num, string[] values) => WaitForSignal(s, line_num);

        // Move
        public void MoveRight1(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 5, 1, true);
        public void MoveRight5(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 5, 5, true);
        public void MoveRight10(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 5, 10, true);
        public void MoveDown1(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 6, 1, true);
        public void MoveDown5(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 6, 5, true);
        public void MoveDown10(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 6, 10, true);
        public void MoveLeft1(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 7, 1, true);
        public void MoveLeft5(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 7, 5, true);
        public void MoveLeft10(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 7, 10, true);
        public void MoveUp1(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 8, 1, true);
        public void MoveUp5(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 8, 5, true);
        public void MoveUp10(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 8, 10, true);
        public void MoveRDownN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 2, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveRUpN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 1, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveLDownN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 3, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveLUpN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 4, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveRightN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 5, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveDownN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 6, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveLeftN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 7, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveUpN(ActivatedSprite s, int line_num, string[] values) => MoveConstant(s, 8, (int)GetConstantOrVariable(s, values[0]), false);
        public void MoveEmpty(ActivatedSprite s, int line_num, string[] values) { }

        // Action
        public void ActionSlow(ActivatedSprite s, int line_num, string[] values) => LeftRightMove(s, line_num, 300, 0);
        public void ActionSlowN(ActivatedSprite s, int line_num, string[] values) => LeftRightMove(s, line_num, 300, (int)GetConstantOrVariable(s, values[0]));
        public void ActionFast(ActivatedSprite s, int line_num, string[] values) => LeftRightMove(s, line_num, 100, 0);
        public void ActionFastN(ActivatedSprite s, int line_num, string[] values) => LeftRightMove(s, line_num, 100, (int)GetConstantOrVariable(s, values[0]));
        public void ActionFlash(ActivatedSprite s, int line_num, string[] values) => FlashSprite(s, line_num, 0);
        public void ActionFlashN(ActivatedSprite s, int line_num, string[] values) => FlashSprite(s, line_num, (int)GetConstantOrVariable(s, values[0]));
        public void ActionRRotate(ActivatedSprite s, int line_num, string[] values) => RotateImage(s, 90, true);
        public void ActionRRotateN(ActivatedSprite s, int line_num, string[] values) => RotateArrowValue(s, true, (int)GetConstantOrVariable(s, values[0]));
        public void ActionLRotate(ActivatedSprite s, int line_num, string[] values) => RotateImage(s, 90, false);
        public void ActionLRotateN(ActivatedSprite s, int line_num, string[] values) => RotateArrowValue(s, false, (int)GetConstantOrVariable(s, values[0]));
        public void ActionRotateLoop(ActivatedSprite s, int line_num, string[] values) => RotateImageLoop(s, line_num, 0);
        public void ActionRotateLoopN(ActivatedSprite s, int line_num, string[] values) => RotateImageLoop(s, line_num, (int)GetConstantOrVariable(s, values[0]));
        public void ActionWave(ActivatedSprite s, int line_num, string[] values) => LeftRightWaveMove(s, line_num, 10.0, 0);
        public void ActionWaveN(ActivatedSprite s, int line_num, string[] values) => LeftRightWaveMove(s, line_num, 10.0, (int)GetConstantOrVariable(s, values[0]));
        public void ActionTWave(ActivatedSprite s, int line_num, string[] values) => UpDownWaveMove(s, line_num, 10.0, 0);
        public void ActionTWaveN(ActivatedSprite s, int line_num, string[] values) => UpDownWaveMove(s, line_num, 10.0, (int)GetConstantOrVariable(s, values[0]));
        public void ActionRandomMove(ActivatedSprite s, int line_num, string[] values) => RandomMove(s, line_num);
        public void ActionZigzag(ActivatedSprite s, int line_num, string[] values) => LeftRightWaveMove(s, line_num, 20.0, 0);
        public void ActionZigzagN(ActivatedSprite s, int line_num, string[] values) => LeftRightWaveMove(s, line_num, 20.0, (int)GetConstantOrVariable(s, values[0]));
        public void ActionTZigzag(ActivatedSprite s, int line_num, string[] values) => UpDownWaveMove(s, line_num, 20.0, 0);
        public void ActionTZigzagN(ActivatedSprite s, int line_num, string[] values) => UpDownWaveMove(s, line_num, 20.0, (int)GetConstantOrVariable(s, values[0]));
        public void ActionBounce(ActivatedSprite s, int line_num, string[] values) => BounceSprite(s, line_num);
        public void ActionJump(ActivatedSprite s, int line_num, string[] values) => JumpSprite(s, line_num, 0);
        public void ActionJumpN(ActivatedSprite s, int line_num, string[] values) => JumpSprite(s, line_num, (int)GetConstantOrVariable(s, values[0]));
        public void ActionRLJump(ActivatedSprite s, int line_num, string[] values) => ArrowJumpSprite(s, line_num, 0);
        public void ActionRLJumpN(ActivatedSprite s, int line_num, string[] values) => ArrowJumpSprite(s, line_num, (int)GetConstantOrVariable(s, values[0]));
        public void ActionAnimate(ActivatedSprite s, int line_num, string[] values) => AnimateSprite(s, line_num, values, 0);
        public void ActionAnimateN(ActivatedSprite s, int line_num, string[] values) => AnimateSprite(s, line_num, values, (int)GetConstantOrVariable(s, values[0]));

        // Control
        public void ControlTime1(ActivatedSprite s, int line_num, string[] values) => SleepVariable(s, line_num, 1);
        public void ControlTime2(ActivatedSprite s, int line_num, string[] values) => SleepVariable(s, line_num, 2);
        public void ControlTimeN(ActivatedSprite s, int line_num, string[] values) => SleepVariable(s, line_num, GetConstantOrVariable(s, values[0]));
        public void ControlLoopN(ActivatedSprite s, int line_num, string[] values) => PushLoopStack(s, line_num, values);
        public void ControlLoop(ActivatedSprite s, int line_num, string[] values) => InitLoopVariables(s, line_num);
        public void ControlFlag(ActivatedSprite s, int line_num, string[] values) => PopLoopStack(s, line_num, values);
        public void ControlFlipX(ActivatedSprite s, int line_num, string[] values) => FlipXBitmap(s);
        public void ControlFlipY(ActivatedSprite s, int line_num, string[] values) => FlipYBitmap(s);
        public void ControlNextSprite(ActivatedSprite s, int line_num, string[] values) => ChangeSpriteBitmap(s);
        public void ControlShow(ActivatedSprite s, int line_num, string[] values) => s.is_visible = true;
        public void ControlHide(ActivatedSprite s, int line_num, string[] values) => s.is_visible = false;
        public void ControlSound(ActivatedSprite s, int line_num, string[] values) => PlaySound(s, line_num, values);
        public void ControlSpeak(ActivatedSprite s, int line_num, string[] values) => s.speak_text = values[0];
        public void ControlSpeakStop(ActivatedSprite s, int line_num, string[] values) => s.speak_text = null;
        public void ControlChangeBack(ActivatedSprite s, int line_num, string[] values) => ChangeBackground(values);
        public void ControlSendSig(ActivatedSprite s, int line_num, string[] values) => BroadCastWithKey(1, values[0]);
        public void ControlSendSigWait(ActivatedSprite s, int line_num, string[] values) => BroadCastWithKey(1, values[0], true);
        public void ControlMoveXY(ActivatedSprite s, int line_num, string[] values) => SetXYPosition(s, values);
        public void ControlSetVal(ActivatedSprite s, int line_num, string[] values) => SetVariableValue(s, values);
        public void ControlChangeVal(ActivatedSprite s, int line_num, string[] values) => ChangeVariableValue(s, values);
        public void ControlStop(ActivatedSprite s, int line_num, string[] values) => CodeStop(s, line_num, values);
        public void ControlClone(ActivatedSprite s, int line_num, string[] values) => CreateClone(s);
        public void ControlCondition(ActivatedSprite s, int line_num, string[] values) => CheckCondition(s, line_num, values[0]);

        // Practice
        public void GameLoopN(ActivatedSprite s, int line_num, string[] values) => ControlLoopN(s, line_num, values);
        public void GameFlag(ActivatedSprite s, int line_num, string[] values) => ControlFlag(s, line_num, values);
        public void GameRight(ActivatedSprite s, int line_num, string[] values) => TurnAndMoveForward(s, 5);
        public void GameDown(ActivatedSprite s, int line_num, string[] values) => TurnAndMoveForward(s, 6);
        public void GameLeft(ActivatedSprite s, int line_num, string[] values) => TurnAndMoveForward(s, 7);
        public void GameUp(ActivatedSprite s, int line_num, string[] values) => TurnAndMoveForward(s, 8);
        public void GameJump(ActivatedSprite s, int line_num, string[] values) => PracticeArrowJump(s);

        /*************************** Implementation ****************************/
        public void WaitForSignal(ActivatedSprite s, int line_num)
        {
            s.wait_signal_obj[line_num].WaitOne();
            s.wait_signal_obj[line_num].Reset();
        }

        public void WaitForTouch(ActivatedSprite s, int line_num, string[] values)
        {
            List<string> walls = new List<string>() { "左框", "右框", "上框", "下框", "整体框" };
            string name = values[0];

            int x = s.x;
            int y = s.y;
            int width = s.width;
            int height = s.height;

            var ret = walls.Find(item => item.Equals(name));
            if (ret != null) {
                int left, right, top, bottom;

                left = x;
                right = x + width;
                top = y;
                bottom = y + height;

                if ((name.Equals(walls[0]) && left > 0) ||
                    (name.Equals(walls[1]) && right < MainForm.stage_size.Width) ||
                    (name.Equals(walls[2]) && top > 0) ||
                    (name.Equals(walls[3]) && bottom < MainForm.stage_size.Height)) {
                    s.pc[line_num] = -1;
                }

                if (name.Equals(walls[4])) {
                    if (left > 0 && right < MainForm.stage_size.Width && top > 0 && bottom < MainForm.stage_size.Height)
                        s.pc[line_num] = -1;
                }

                return;
            } else {
                ActivatedSprite target_sprite = null;
                foreach (var sprite in ActivatedSpriteController.sprite_list) {
                    if (sprite.name.Equals(name))
                        target_sprite = sprite;
                }

                if (target_sprite == null) {
                    s.pc[line_num] = -1;
                    return;
                }

                Rectangle rc1, rc2;
                int target_x = target_sprite.x;
                int target_y = target_sprite.y;
                int target_width = target_sprite.width;
                int target_height = target_sprite.height;

                rc1 = new Rectangle(x, y, width, height);
                rc2 = new Rectangle(target_x, target_y, target_width, target_height);

                var result = Rectangle.Intersect(rc1, rc2);
                if (result.IsEmpty)
                    s.pc[line_num] = -1;
            }
        }

        public void MoveConstant(ActivatedSprite s, int arrow, int n, bool is_delay)
        {
            s.arrow = arrow;
            MoveSprite(s, n, 1);

            if (is_delay)
                Thread.Sleep(1000);
        }

        public void LeftRightMove(ActivatedSprite s, int line_num, int delay, int n)
        {
            if (s.arrow != 5 && s.arrow != 7)
                s.arrow = 5;

            int width = s.width;
            int i = (n > 0) ? 0 : -1;
            while (i < n) {
                if (IsFinish(s, line_num))
                    return;

                if ((s.arrow == 5 && s.x + width < MainForm.stage_size.Width) || (s.arrow == 7 && s.x > 0)) {
                    s.x += (s.arrow == 5) ? 10 : -10;
                } else {
                    FlipXBitmap(s);
                    s.arrow = (s.arrow == 5) ? 7 : 5;

                    i = (n > 0) ? i + 1 : i;
                }
                Thread.Sleep(delay);
            }
        }

        public void RotateImage(ActivatedSprite s, double angle, bool cw)
        {
            lock (s) {
                Bitmap origin_bmp = s.GetOriginalImg();
                double degree = (cw) ? 360.0 - angle : angle;
                (s.cur_img, s.x, s.y) = ImageUtility.RotateImage(origin_bmp, s.cur_img, degree, s.x, s.y);
            }
        }

        public void RotateImageLoop(ActivatedSprite s, int line_num, int n)
        {
            for (int i = (n > 0) ? 0 : -1; i < n && !IsFinish(s, line_num); i = (n > 0) ? i + 1 : i) {
                for (double angle = 0; angle <= 360 && !IsFinish(s, line_num); angle += 10) {
                    RotateImage(s, angle, true);
                    Thread.Sleep(300);
                }
            }
        }

        public void RotateArrowValue(ActivatedSprite s, bool cw, int value)
        {
            // 같은 방향으로 회전하는 경우 각도를 더해준다.
            if (s.is_cw != cw) {
                s.angle = 0;
                s.is_cw = cw;
            }

            s.angle += value;
            RotateImage(s, s.angle, cw);
        }

        public void LeftRightWaveMove(ActivatedSprite s, int line_num, double factor, int n)
        {
            if (s.arrow != 5 && s.arrow != 7)
                s.arrow = 5;

            int width = s.width;
            int zero_point = s.y;

            int i = (n > 0) ? 0 : -1;
            while (i < n) {
                for (double angle = 1; angle < 360; angle++) {
                    if (IsFinish(s, line_num))
                        return;

                    if ((s.arrow == 5 && s.x + width < MainForm.stage_size.Width) || (s.arrow == 7 && s.x > 0)) {
                        double radian = Math.PI * angle / 180.0;
                        s.y = (int)(-Math.Sin(radian * factor) * 50.0) + zero_point;
                        s.x += (s.arrow == 5) ? 10 : -10;
                    } else {
                        FlipXBitmap(s);

                        s.arrow = (s.arrow == 5) ? 7 : 5;
                        i = (n > 0) ? i + 1 : i;
                        break;
                    }

                    Thread.Sleep(100);
                }
            }
        }

        public void UpDownWaveMove(ActivatedSprite s, int line_num, double factor, int n)
        {
            if (s.arrow != 6 && s.arrow != 8)
                s.arrow = 6;

            int height = s.height;
            int zero_point = s.x;
            int i = (n > 0) ? 0 : -1;

            while (i < n) {
                for (double angle = 1; angle < 360; angle++) {
                    if (IsFinish(s, line_num))
                        return;

                    if ((s.arrow == 6 && s.y + height < MainForm.stage_size.Height) || (s.arrow == 8 && s.y > 0)) {
                        double radian = Math.PI * angle / 180.0;
                        s.x = (int)(Math.Sin(radian * factor) * 50.0) + zero_point;
                        s.y += (s.arrow == 6) ? 10 : -10;
                    } else {
                        s.arrow = (s.arrow == 6) ? 8 : 6;
                        i = (n > 0) ? i + 1 : i;
                        break;
                    }

                    Thread.Sleep(100);
                }
            }
        }

        public void RandomMove(ActivatedSprite s, int line_num)
        {
            s.arrow = new Random(Guid.NewGuid().GetHashCode()).Next(1, 9); // 1~8 난수 생성
            int n = new Random(Guid.NewGuid().GetHashCode()).Next(1, 6);
            MoveSprite(s, 20, n, 100);

            s.pc[line_num] -= 1;
        }

        public void MoveSprite(ActivatedSprite s, int value, int n, int delay = 1)
        {
            /* 4  8  1
               7     5
               3  6  2 */
            var actions = new List<Func<ActivatedSprite, int, int>>()
            {
                RightUpMove, RightDownMove, LeftDownMove, LeftUpMove,
                RightMove, DownMove, LeftMove, UpMove
            };

            for (int i = 0; i < n; i++) {
                actions[s.arrow - 1].Invoke(s, value * MainForm.moving_unit);
                Thread.Sleep(delay);
            }
        }

        public void JumpSprite(ActivatedSprite s, int line_num, int n)
        {
            int i = (n > 0) ? 0 : -1;
            while (i < n) {
                for (int j = 5; j > 0; j--) {
                    s.arrow = 8;
                    MoveSprite(s, j * 10, 1, 100);

                    if (IsFinish(s, line_num))
                        return;
                }

                for (int j = 1; j <= 5; j++) {
                    s.arrow = 6;
                    MoveSprite(s, j * 10, 1, 100);

                    if (IsFinish(s, line_num))
                        return;
                }

                i = (n > 0) ? i + 1 : i;
            }
        }

        public void BounceSprite(ActivatedSprite s, int line_num)
        {
            if (s.arrow != -1) {
                int new_arrow = GetNextArrow(s);
                if (new_arrow != -1)
                    s.arrow = new_arrow;

                Thread.Sleep(50);
                s.pc[line_num] -= 1;
            }
        }

        public int GetNextArrow(ActivatedSprite s)
        {
            var actions = new List<Func<ActivatedSprite, int, int>>()
            {
                RightUpMove, RightDownMove, LeftDownMove, LeftUpMove
            };

            int wall = actions[s.arrow - 1].Invoke(s, 10);
            if (wall != -1)
                return ChangeArrow(s.arrow, wall);

            return -1;
        }

        public void ArrowJumpSprite(ActivatedSprite s, int line_num, int n)
        {
            if (s.arrow != 5 && s.arrow != 7)
                s.arrow = 5;

            int zero_point = s.y;

            int i = (n > 0) ? 0 : -1;
            for (; i < n; i = (n > 0) ? i + 1 : i) {
                for (double angle = 0; angle < 180; angle += 10) {
                    if (IsFinish(s, line_num))
                        return;

                    double radian = Math.PI * angle / 180.0;
                    s.y = (int)(-Math.Sin(radian) * 50.0) + zero_point;
                    s.x = (s.arrow == 5) ? s.x + 5 : s.x - 5;

                    Thread.Sleep(50);
                }
            }
        }

        public void AnimateSprite(ActivatedSprite s, int line_num, string[] values, int n)
        {
            int i = (n > 0) ? 0 : -1;
            for (; i < n; i = (n > 0) ? i + 1 : i) {
                if (IsFinish(s, line_num))
                    return;

                ControlNextSprite(s, line_num, values);
                Thread.Sleep(1000);
            }
        }

        public void SleepVariable(ActivatedSprite s, int line_num, double value)
        {
            if (value < 0.1)
                return;

            if (value < 1) {
                Thread.Sleep((int)(value * 1000.0));
                return;
            }

            for (int i = 0; i < value; i++) {
                if (IsFinish(s, line_num))
                    return;

                Thread.Sleep(1 * 1000);
            }
        }

        public void PushLoopStack(ActivatedSprite s, int line_num, string[] values)
        {
            s.loop_stack.Push(line_num, s.pc[line_num], (int)GetConstantOrVariable(s, values[0]));
        }

        public void PopLoopStack(ActivatedSprite s, int line_num, string[] values)
        {
            int next_pc = s.loop_stack.Pop(line_num);
            if (next_pc != 0)
                s.pc[line_num] = next_pc;
        }

        public void InitLoopVariables(ActivatedSprite s, int line_num)
        {
            s.loop_stack.Push(line_num, s.pc[line_num], 0);
        }

        public void FlipXBitmap(ActivatedSprite s)
        {
            lock (s) {
                for (int i = 0; i < s.img_list_count; i++) {
                    s.img_list[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
                }
            }
        }

        public void FlipYBitmap(ActivatedSprite s)
        {
            lock (s) {
                for (int i = 0; i < s.img_list_count; i++) {
                    s.img_list[i].RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
            }
        }

        public void ChangeSpriteBitmap(ActivatedSprite s, bool is_next = true)
        {
            if (is_next) {
                int max = s.img_list_count - 1;
                s.cur_img_num = (s.cur_img_num < max) ? s.cur_img_num + 1 : 0;

                lock (s)
                    s.cur_img = s.GetOriginalImg();
            } else {
                s.cur_img_num = (s.cur_img_num > 0) ? s.cur_img_num - 1 : s.img_list_count - 1;

                lock (s)
                    s.cur_img = s.GetOriginalImg();
            }
        }

        [DllImport("winmm.dll", CharSet = CharSet.Unicode)]
        public static extern int mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        public void PlaySound(ActivatedSprite s, int line_num, string[] values)
        {
            var sound = Sound.GetNameToSound(values[0]);
            if (sound == null)
                return;

            string path = sound.local_path;
            if (!File.Exists(path))
                return;

            string open_path = "open " + path + " alias wav type waveaudio wait";
            mciSendString(open_path, null, 0, IntPtr.Zero);

            StringBuilder length_buf = new StringBuilder(32);
            mciSendString("status wav length", length_buf, length_buf.Capacity, IntPtr.Zero);

            int.TryParse(length_buf.ToString(), out int length);

            mciSendString("play wav", null, 0, IntPtr.Zero);
            Thread.Sleep(length);

            mciSendString("close wav", null, 0, IntPtr.Zero);
        }

        public void ChangeBackground(string[] values)
        {
            var back = Background.GetNameToBack(values[0]);
            if (back == null)
                return;

            MainForm.stage_player.SetBackground(back);

            MainForm.stage_player.Stop();
            MainForm.stage_player.stage_player_thread.Join();

            MainForm.stage_player.Start();
        }

        public void SetXYPosition(ActivatedSprite s, string[] values)
        {
            int x = (int)GetConstantOrVariable(s, values[0]);
            int y = (int)GetConstantOrVariable(s, values[1]);

            var (rate_x, rate_y) = StagePlayer.GetFullScreenSizeFactor();

            s.x = (StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN)) ? (int)(x * rate_x) : x;
            s.y = (StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN)) ? (int)(y * rate_y) : y;
        }

        public void FlashSprite(ActivatedSprite s, int line_num, int n)
        {
            int i = (n > 0) ? 0 : -1;
            for (; i < (n * 2); i = (n > 0) ? i + 1 : i) {
                if (IsFinish(s, line_num))
                    return;

                s.is_visible ^= true;
                Thread.Sleep(500);
            }
        }

        public static void BroadCastWithKey(int event_type, string key, bool wait = false)
        {
            List<Task> tasks = new List<Task>();
            foreach (var sprite in ActivatedSpriteController.sprite_list) {
                Task task = new Task(() => {
                    for (int j = 0; j < sprite.code_list.Count; j++) {
                        var code = sprite.code_list[j][0];
                        if (code.block_view.controls == null)
                            continue;

                        if (code.event_type != event_type)
                            continue;

                        if (code.values[0].Equals(key)) {
                            sprite.wait_signal_obj[j].Set();

                            if (wait)
                                sprite.sprite_runner.GetLineFinishEvent(j).WaitOne();
                        }
                    }
                });

                tasks.Add(task);
                task.Start();
            }

            if (wait) {
                foreach (var task in tasks)
                    task.Wait();
            }
        }

        public void SetVariableValue(ActivatedSprite s, string[] values)
        {
            if (UserVariableManager.user_variables.TryGetValue(values[0], out UserVariable variable)) {
                if (double.TryParse(values[1], out double n))
                    variable.SetValue(n);
            }
        }

        public void ChangeVariableValue(ActivatedSprite s, string[] values)
        {
            if (UserVariableManager.user_variables.TryGetValue(values[0], out UserVariable variable)) {
                if (double.TryParse(values[1], out double n))
                    variable.ChangeValue(n);
            }
        }

        public void CodeStop(ActivatedSprite s, int line_num, string[] values)
        {
            s.sprite_runner.StopSprites();
        }

        public void TurnAndMoveForward(ActivatedSprite s, int target)
        {
            TurnSprite(s, target);
            MoveForward(s);
        }

        public void PracticeArrowJump(ActivatedSprite s)
        {
            if (s.arrow != 5 && s.arrow != 7)
                TurnSprite(s, 5);

            if (MainForm.practice_mode.CheckPath(s.arrow, s.x, s.y, 200)) {
                EffectSound.move_successed_sound.Play();

                lock (s)
                    s.cur_img = new Bitmap(MainForm.practice_mode.jump_bitmap_list[s.cur_img_num]);

                int delay = 5;
                int zero = s.y;
                for (double i = 0; i <= 180; i++) {
                    s.y = (int)(-Math.Sin(Math.PI * i / 180.0) * 100.0) + zero;
                    s.x = (s.arrow == 5) ? s.x + 1 : s.x - 1;

                    if (i % 10 == 0 && i != 0)
                        s.x = (s.arrow == 5) ? s.x + 1 : s.x - 1;

                    if (i % 20 == 0 && i != 0 && i < 91)
                        delay++;
                    else if (i % 20 == 0 && i != 0 && i > 90)
                        delay--;

                    Thread.Sleep(delay);
                }

                s.x = (s.arrow == 5) ? s.x + 1 : s.x - 1;

                lock (s)
                    s.cur_img = new Bitmap(MainForm.practice_mode.bitmap_list[s.cur_img_num]);
            } else {
                EffectSound.move_failed_sound.Play();
            }
        }

        public void CreateClone(ActivatedSprite s)
        {
            var clone = s.GetClone();
            int event_idx = -1;
            for (int i = 0; i < s.code_list.Count; i++) {
                if (s.code_list[i][0].event_type == 5) {
                    event_idx = i;
                    break;
                }
            }

            if (event_idx == -1)
                return;

            clone.code_list.Add(new List<Block>());
            clone.code_list[0].Add(new Block(blocks[0][0], true));
            for (int i = 1; i < s.code_list[event_idx].Count; i++)
                clone.code_list[0].Add(new Block(s.code_list[event_idx][i], true));

            clone.SetPC(1);
            clone.sprite_runner = new RuntimeSprite {
                runtime_sprite_codes = new List<RuntimeSpriteCode>(),
                runtime_sprite_threads = new List<Thread>()
            };

            var runtime_sprite_code = new RuntimeSpriteCode {
                code_line_idx = 0
            };

            clone.sprite_runner.runtime_sprite_codes.Add(runtime_sprite_code);

            Thread runtime_sprite_thread = new Thread(new ParameterizedThreadStart(runtime_sprite_code.RunCode));
            runtime_sprite_thread.IsBackground = true;
            clone.sprite_runner.runtime_sprite_threads.Add(runtime_sprite_thread);

            s.cloned_sprite_list.Add(clone);
            runtime_sprite_thread.Start(clone);
        }

        public void CheckCondition(ActivatedSprite s, int line_num, string exp)
        {
            string l_value_str = string.Empty;
            string r_value_str = string.Empty;
            string op = string.Empty;
            int pos = 0;

            for (int i = pos; i < exp.Length; i++) {
                if (exp[i].Equals(' ')) {
                    pos = i + 1;
                    break;
                }

                l_value_str += exp[i];
            }

            for (int i = pos; i < exp.Length; i++) {
                if (exp[i].Equals(' ')) {
                    pos = i + 1;
                    break;
                }

                op += exp[i];
            }

            for (int i = pos; i < exp.Length; i++) {
                r_value_str += exp[i];
            }

            int l_value = (int)GetConstantOrVariable(s, l_value_str);
            int r_value = (int)GetConstantOrVariable(s, r_value_str);

            if (op.Equals(">=")) {
                if (l_value >= r_value) {
                    ControlLoopN(s, line_num, new string[1] { "1" });
                    return;
                }
            } else if (op.Equals(">")) {
                if (l_value > r_value) {
                    ControlLoopN(s, line_num, new string[1] { "1" });
                    return;
                }
            } else if (op.Equals("==")) {
                if (l_value == r_value) {
                    ControlLoopN(s, line_num, new string[1] { "1" });
                    return;
                }
            } else if (op.Equals("<")) {
                if (l_value < r_value) {
                    ControlLoopN(s, line_num, new string[1] { "1" });
                    return;
                }
            } else if (op.Equals("<=")) {
                if (l_value <= r_value) {
                    ControlLoopN(s, line_num, new string[1] { "1" });
                    return;
                }
            }

            int pc = s.pc[line_num];
            while (!s.code_list[line_num][pc].name.Equals("ControlFlag"))
                pc++;

            s.pc[line_num] = pc;
        }

        public void TurnSprite(ActivatedSprite s, int target)
        {
            int diff = s.arrow - target;
            if (diff != 0) {
                for (int i = 0; i < Math.Abs(diff); i++) {
                    if (diff > 0)
                        ChangeSpriteBitmap(s, false);
                    else
                        ChangeSpriteBitmap(s, true);
                }

                s.arrow = target;
            }

            Thread.Sleep(500);
        }

        public void MoveForward(ActivatedSprite s)
        {
            int x = s.x;
            int y = s.y;
            if (MainForm.practice_mode.CheckPath(s.arrow, x, y)) {
                MoveSprite(s, 100, 1);
                EffectSound.move_successed_sound.Play();
            } else {
                EffectSound.move_failed_sound.Play();
            }

            Thread.Sleep(500);
        }

        public int ChangeArrow(int arrow, int wall)
        {
            if (arrow == wall)
                return (arrow > 1) ? arrow - 1 : 4;

            int arrow_temp = (arrow > 1) ? arrow - 1 : 4;
            if (arrow_temp == wall)
                return (arrow < 4) ? arrow + 1 : 1;

            return -1;
        }

        public int DownMove(ActivatedSprite s, int n)
        {
            int y = s.y;
            int next_y = y + n + s.height;
            if (next_y <= MainForm.stage_size.Height)
                s.y += n;
            else
                s.y = MainForm.stage_size.Height - s.height;

            return 0;
        }

        public int RightMove(ActivatedSprite s, int n)
        {
            int x = s.x;
            int next_x = x + n + s.width;
            if (next_x <= MainForm.stage_size.Width)
                s.x += n;
            else
                s.x = MainForm.stage_size.Width - s.width;

            return 0;
        }

        public int LeftMove(ActivatedSprite s, int n)
        {
            int x = s.x;

            if (x - n >= 0)
                s.x -= n;
            else
                s.x = 0;

            return 0;
        }

        public int UpMove(ActivatedSprite s, int n)
        {
            int y = s.y;

            if (y - n >= 0)
                s.y -= n;
            else
                s.y = 0;

            return 0;
        }

        public int RightUpMove(ActivatedSprite s, int n)
        {
            int x = s.x;
            int y = s.y;

            if (x + s.width + n < MainForm.stage_size.Width) {
                if (y - n > 0) {
                    s.x += n;
                    s.y -= n;                    
                    return -1;
                } else {
                    return 4;
                }
            } else {
                return 1;
            }
        }

        public int RightDownMove(ActivatedSprite s, int n)
        {
            int x = s.x;
            int y = s.y;

            if (x + s.width + n < MainForm.stage_size.Width) {
                if (y + s.height + n < MainForm.stage_size.Height) {
                    s.x += n;
                    s.y += n;
                    return -1;
                } else {
                    return 2;
                }
            } else {
                return 1;
            }
        }

        public int LeftDownMove(ActivatedSprite s, int n)
        {
            int x = s.x;
            int y = s.y;

            if (x - n > 0) {
                if (y + s.height + n < MainForm.stage_size.Height) {
                    s.x -= n;
                    s.y += n;
                    return -1;
                } else {
                    return 2;
                }
            } else {
                return 3;
            }
        }

        public int LeftUpMove(ActivatedSprite s, int n)
        {
            int x = s.x;
            int y = s.y;

            if (x - n > 0) {
                if (y - n > 0) {
                    s.x -= n;
                    s.y -= n;
                    return -1;
                } else {
                    return 4;
                }
            } else {
                return 3;
            }
        }

        public static void SendSignalToSprite(ActivatedSprite s, int event_type)
        {
            for (int j = 0; j < s.code_list.Count; j++) {
                var code = s.code_list[j][0];
                if (code.event_type != event_type)
                    continue;

                s.wait_signal_obj[j].Set();
            }
        }

        public static double GetConstantOrVariable(ActivatedSprite s, string value)
        {
            if (value.Equals("X"))
                return s.x + s.width;

            if (value.Equals("Y"))
                return s.y + s.height;

            if (value.Equals("Width"))
                return MainForm.stage_size.Width;

            if (value.Equals("Height"))
                return MainForm.stage_size.Height;

            if (double.TryParse(value, out double n)) {
                return n;
            } else {
                if (UserVariableManager.user_variables.TryGetValue(value, out UserVariable variable)) {
                    return (double)variable.GetValue();
                } else {
                    new MsgBoxForm($"请输入正确的数值").ShowDialog();
                    MainForm.stop_project?.Invoke();
                }
            }

            return -2;
        }

        public static bool IsFinish(ActivatedSprite s, int line_idx)
        {
            return s.sprite_runner.runtime_sprite_codes[line_idx].is_stop_code;
        }
    }
}
