using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ArtcoCustomControl;

namespace Artco
{
    public partial class MainForm
    {
        public static Size stage_size { get; set; }
        public static int moving_unit { get; set; }        

        private void StagePaint(object sender, PaintEventArgs e)
        {
            foreach (var sprite in ActivatedSpriteController.sprite_list) {
                if (!sprite.is_visible)
                    continue;

                if (sprite.speak_text != null) {
                    Point sprite_point = new Point(sprite.x, sprite.y);
                    string sprite_text = SubstituteVariables(sprite.speak_text);

                    ShowSpeakBox(sprite_point, sprite_text, e);
                }

                lock (sprite) {
                    e.Graphics.DrawImage(sprite.cur_img, sprite.x, sprite.y);
                }

                for (int j = 0; j < sprite.cloned_sprite_list.Count; j++) {
                    var clone = sprite.cloned_sprite_list[j];
                    if (!clone.is_visible)
                        continue;

                    lock (clone) {
                        e.Graphics.DrawImage(clone.cur_img, clone.x, clone.y);
                    }
                }
            }
        }

        private string SubstituteVariables(string speak_text)
        {
            string substituted_text = speak_text;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Regex reg_exp = new Regex(@"\{(.*?)\}", RegexOptions.Compiled);

            try {
                foreach (Match match in reg_exp.Matches(speak_text)) {
                    string key = match.Value.Substring(1, match.Value.Length - 2);
                    if(dic.ContainsKey(key) == false)
                        dic.Add(key, UserVariableManager.user_variables[key].GetValue().ToString());
                }
            } catch (KeyNotFoundException e) {
                Debug.Print(e.Message);
            }

            foreach (string key in dic.Keys) {
                substituted_text = substituted_text.Replace("{" + key + "}", dic[key]);
            }

            return substituted_text;
        }

        private SizeF MeasureTextArea(string text, PaintEventArgs e)
        {
            SizeF layout_size = new SizeF(140, 30 * 6);
            SizeF string_size = e.Graphics.MeasureString(text, DynamicResources.font, layout_size);

            if(string_size.Height >= 30 *3) {
                layout_size.Width = 280;
                string_size = e.Graphics.MeasureString(text, DynamicResources.font, layout_size);
            }

            return string_size;
        }

        private void ShowSpeakBox(Point point, string text, PaintEventArgs e)
        {
            Size string_area = Size.Ceiling(MeasureTextArea(text, e));
            Size speak_box_size = new Size(string_area.Width + 40, string_area.Height + 60);
            Bitmap speak_box = new Bitmap(Properties.Resources.SpeakBox, speak_box_size);
            Point speak_box_point = new Point(point.X + 20, point.Y - speak_box.Height);
            Rectangle text_rect = new Rectangle(speak_box_point.X + 20, speak_box_point.Y + 20, string_area.Width, string_area.Height);

            e.Graphics.DrawImage(speak_box, speak_box_point.X, speak_box_point.Y);
            e.Graphics.DrawString(text, DynamicResources.font, Brushes.DimGray, text_rect);
        }

        private void SelectBackCB(object sender)
        {
            var background = (Background)sender;
            if (background == null)
                return;

            StopProject();
            stage_player.SetBackground(background);

            if (practice_mode != null)
                FinishPracticeMode();

            if (stage_player.GetBackground().mode == 1) {
                if(Properties.Settings.Default.resolution != 0) {
                    new MsgBoxForm("Please choose 1920x1080 resolution in setting form").Show();
                    return;
                }
                RemoveAllSprite();
                ShowPracticeMenu();

                practice_mode = new PracticeMode();
                practice_mode.SetGameEnvironment();
            } else {
                ShowMoveMenu();
                stage_player.SetInitializeImage();
            }
        }

        private void ShowPracticeResultForm()
        {
            if (InvokeRequired) {
                Invoke(new Action(ShowPracticeResultForm));
            } else {
                EffectSound.finish_game_sound.Play();
                StopProject();

                PracticeResultControl practice_result_control = new PracticeResultControl {
                    Dock = DockStyle.Fill
                };

                practice_result_control.NextStepClick += (sernder, e) => {
                    var next_back = Background.GetNextBack();
                    if (next_back == null) {
                        return;
                    }

                    SelectBackCB(next_back);

                    stage_panel.Controls.Remove(practice_result_control);
                    practice_result_control.Dispose();
                };

                practice_result_control.RestartClick += (sernder, e) => {
                    stage_panel.Controls.Remove(practice_result_control);
                    practice_result_control.Dispose();
                };

                practice_result_control.FinishClick += (sernder, e) => {
                    FinishPracticeMode();
                    Btn_SelectBackground_Click(null, null);

                    stage_panel.Controls.Remove(practice_result_control);
                    practice_result_control.Dispose();
                };

                stage_panel.Controls.Add(practice_result_control);
            }
        }

        private void FinishPracticeMode()
        {
            StagePlayer.UnsetFlag(StagePlayer.Flag.GAME);
            practice_mode = null;
            RemoveAllSprite();
            ShowMoveMenu();
        }

        private void Btn_SelectBackground_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING))
                return;

            EffectSound.mouse_click_sound.Play();
            StopProject();

            Back1StorageForm back1_form = new Back1StorageForm(SelectBackCB);
            back1_form.Show();
        }

        private void Btn_SelectEduBackground_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING))
                return;

            EffectSound.mouse_click_sound.Play();
            StopProject();

            Back2StorageForm back2_form = new Back2StorageForm(SelectBackCB);
            back2_form.Show();
        }

        private void Btn_SelectBackMusic_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            using MusicStorageForm music_form = new MusicStorageForm();
            music_form.ShowDialog();
        }

        private void SetStageInfo()
        {
            stage_size = new Size(stage_panel.Width, stage_panel.Height);

            // 무대 사이즈가 거의 2:1이므로 상하좌우를 같은 크기로 움직이게됨
            moving_unit = stage_size.Width / 200;            
        }

        private void StageMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            var sprite = ActivatedSpriteController.GetActSpriteWithLoc(e.X, e.Y);
            if (sprite == null)
                return;

            sprite.sprite_view.EditSprite(null, null);
        }

        private static ActivatedSprite _grabbed_sprite = null;

        private void StageMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var sprite = ActivatedSpriteController.GetActSpriteWithLoc(e.X, e.Y);
            if (sprite == null)
                return;

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) {
                Block.SendSignalToSprite(sprite, 4);
                return;
            }

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;
            
            _grabbed_sprite = sprite;
            ActivatedSpriteController.Focus(ActivatedSpriteController.sprite_list.IndexOf(sprite));
        }

        private void StageMouseMove(object sender, MouseEventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            lbl_Xpos.Text = "X : " + e.X;
            lbl_Ypos.Text = "Y : " + e.Y;

            if (_grabbed_sprite == null)
                return;

            _grabbed_sprite.x = e.X - (_grabbed_sprite.width / 2);
            _grabbed_sprite.y = e.Y - (_grabbed_sprite.height / 2);
            InvalidateStageForm();
        }

        private void StageMouseUp(object sender, MouseEventArgs e)
        {
            if (_grabbed_sprite == null)
                return;

            _grabbed_sprite = null;
        }

        private void BtnFullRunAnimateClick(object sender, EventArgs e)
        {
            StartProject();
        }

        private void BtnFullStopAnimateClick(object sender, EventArgs e)
        {
            StopProject();
        }

        private void Btn_FullScreen_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME))
                return;

            EffectSound.mouse_click_sound.Play();
            ChangeScreenState(true);
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            if (!StagePlayer.ORCheckFlags(StagePlayer.Flag.FULLSCREEN))
                Application.Exit();

            EffectSound.mouse_click_sound.Play();

            // 실행 중이 아니라면 화면 전환
            // 실행 중이라면 배경 완료 스레드에서 처리
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) {
                _change_screen_state = ChangeScreenState;
            } else {
                ChangeScreenState(false);
            }
        }

        public delegate void DChangeScreenState(bool is_full);

        private void ChangeScreenState(bool is_full)
        {
            if (is_full) {
                StagePlayer.SetFlags(StagePlayer.Flag.FULLSCREEN);
                SetPanelsVisible(false);

            } else {
                StagePlayer.UnsetFlag(StagePlayer.Flag.FULLSCREEN);
                SetPanelsVisible(true);
            }

            ActivatedSpriteController.TranslateSizeAndLoc(is_full);
           
            btn_ReturnHome.ThreadSafe(x => x.Visible = !is_full);
            btn_FullRunAnimate.ThreadSafe(x => x.Visible = is_full);
            btn_FullStopAnimate.ThreadSafe(x => x.Visible = is_full);
            btn_RecordingReady.ThreadSafe(x => x.Visible = is_full);            

            Bitmap close_btn = (is_full) ? Properties.Resources.ReleaseFullModeBtn : Properties.Resources.Close;
            btn_Close.ThreadSafe(x => x.Image = close_btn);

            SetStageInfo();
            InvalidateStageForm();
        }

        private void SetPanelsVisible(bool flag)
        {
            SuspendLayout();

            pnl_stage_bottom.ThreadSafe(x => x.Visible = flag);
            pnl_stage_left.ThreadSafe(x => x.Visible = flag);
            pnl_stage_right.ThreadSafe(x => x.Visible = flag);
            pnl_stage_top.ThreadSafe(x => x.Visible = flag);

            pnl_col_padding0.ThreadSafe(x => x.Visible = flag);
            pnl_col_padding1.ThreadSafe(x => x.Visible = flag);
            pnl_col_padding2.ThreadSafe(x => x.Visible = flag);
            pnl_row_padding0.ThreadSafe(x => x.Visible = flag);
            pnl_row_padding1.ThreadSafe(x => x.Visible = flag);
            pnl_row_padding2.ThreadSafe(x => x.Visible = flag);
            pnl_row_padding3.ThreadSafe(x => x.Visible = flag);

            pnl_BlockTab.ThreadSafe(x => x.Visible = flag);
            pnl_Left.ThreadSafe(x => x.Visible = flag);
            pnl_Menu.ThreadSafe(x => x.Visible = flag);
            pnl_Right.ThreadSafe(x => x.Visible = flag);
            pnl_EditorBox.ThreadSafe(x => x.Visible = flag);

            ResumeLayout();
        }
    }
}