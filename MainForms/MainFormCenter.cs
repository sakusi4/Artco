using System;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class MainForm
    {
        private void StagePaint(object sender, PaintEventArgs e)
        {
            foreach (var sprite in ActivatedSpriteController.sprite_list) {
                if (!sprite.is_visible)
                    continue;

                if (sprite.speak_text != null) {
                    ShowSpeakBox(sprite, e);
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

        private SizeF MeasureTextRectangle(string text, PaintEventArgs e)
        {
            SizeF layout_size = new SizeF(140, 30 * 6);
            SizeF string_size = e.Graphics.MeasureString(text, DynamicResources.font, layout_size);

            if(string_size.Height >= 30 *3) {
                layout_size.Width = 280;
                string_size = e.Graphics.MeasureString(text, DynamicResources.font, layout_size);
            }

            return string_size;
        }

        private void ShowSpeakBox(ActivatedSprite sprite, PaintEventArgs e)
        {
            Size string_size = Size.Ceiling(MeasureTextRectangle(sprite.speak_text, e));
            Size speak_box_size = new Size(string_size.Width + 40, string_size.Height + 40);
            Bitmap speak_box = new Bitmap(DynamicResources.b_speak_box, speak_box_size);

            int x = sprite.x + 20;
            int y = sprite.y - speak_box.Height;

            Rectangle text_rect = new Rectangle(x + 20, y + 20, string_size.Width, string_size.Height);

            e.Graphics.DrawImage(speak_box, x, y);
            e.Graphics.DrawString(sprite.speak_text, DynamicResources.font, Brushes.DimGray, text_rect);
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

                var result_form = new PracticeResultForm {
                    Cursor = new Cursor(Properties.Resources.Cursor.GetHicon()),
                    Location = new Point(461, 104),
                    ShowInTaskbar = false
                };
                result_form.ShowDialog();

                if (result_form.exit_code == 1) {
                    var next_back = Background.GetNextBack();
                    if (next_back == null) {
                        return;
                    }

                    SelectBackCB(next_back);
                } else if (result_form.exit_code == -1) {
                    FinishPracticeMode();
                    Btn_SelectBackground_Click(null, null);
                }
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
            if (is_full)
                StagePlayer.SetFlags(StagePlayer.Flag.FULLSCREEN);
            else
                StagePlayer.UnsetFlag(StagePlayer.Flag.FULLSCREEN);

            this.ThreadSafe(x => x.WindowState = (is_full) ? FormWindowState.Maximized : FormWindowState.Normal);

            if (is_full)
                pbx_Stage.Invoke(new Action(() => pbx_Stage.BringToFront()));

            pbx_Stage.ThreadSafe(x => x.Location = (is_full) ? new Point(0, 40) : new Point(461, 104));
            pbx_Stage.ThreadSafe(x => x.Width = (is_full) ? 1920 : 1000);
            pbx_Stage.ThreadSafe(x => x.Height = (is_full) ? 1040 : 550);

            stage_panel.ThreadSafe(x => x.Width = (is_full) ? 1920 : 1000);
            stage_panel.ThreadSafe(x => x.Height = (is_full) ? 1040 : 550);

            ActivatedSpriteController.TranslateSizeAndLoc(is_full);
            RuntimeEnv.width = stage_panel.Width;
            RuntimeEnv.height = stage_panel.Height;

            btn_RecordingReady.ThreadSafe(x => x.Visible = is_full);
            btn_FullRunAnimate.ThreadSafe(x => x.Visible = is_full);
            btn_FullStopAnimate.ThreadSafe(x => x.Visible = is_full);
            btn_ReturnHome.ThreadSafe(x => x.Visible = !is_full);

            Bitmap close_btn = (is_full) ? Properties.Resources.ReleaseFullModeBtn : Properties.Resources.Close;
            btn_Close.ThreadSafe(x => x.Image = close_btn);

            InvalidateStageForm();
        }
    }
}