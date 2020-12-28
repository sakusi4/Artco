using System;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class MainForm : Form
    {
        private readonly ToolTip _right_menu_tooltip = new ToolTip();
        private Point _form_loc = new Point(0, 0);
        private bool _is_form_move;

        public static StagePanel stage_panel { get; set; } = new StagePanel();
        public static StagePlayer stage_player { get; set; }
        public static PracticeMode practice_mode { get; set; }
        public static Action show_practice_result_form { get; set; }
        public static Action<Background> select_back_cb { get; set; }
        public static Action<Sprite> select_sprite_cb { get; set; }
        public static Action<Image> draw_background { get; set; }
        public static Action<string> set_recording_time { get; set; }
        public static Action<bool> show_recording_time { get; set; }
        public static Action<bool> show_recording_btn { get; set; }
        public static Action invalidate_stage_form { get; set; }
        public static Action invalidate_editor_panel { get; set; }
        public static Action invalidate_blocks_panel { get; set; }
        public static Action finish_stage_player_cb { get; set; }
        public static Action finish_act_sprites_cb { get; set; }
        public static Action remove_all_sprite { get; set; }
        public static Action start_project { get; set; }
        public static Action stop_project { get; set; }
        public static Action release_focus { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

            SetDelegates();
            AttachTooltipRightMenu();
            SetStagePanelProperties();
            SetWindowFormTheme(Properties.Settings.Default.Theme);
            SetMouseWheelEvent();

            RuntimeEnv.SetStageSize(pbx_Stage.Width, pbx_Stage.Height);
            
            CodeEditor.parent_panel = pnl_EditorBox;
            SpriteView.sprite_list_panel = pnl_SpriteList;
        }

        private void SetDelegates()
        {
            show_practice_result_form = ShowPracticeResultForm;
            finish_stage_player_cb = FinishStagePlayerCB;
            invalidate_stage_form = InvalidateStageForm;
            invalidate_editor_panel = InvalidateEditorPanel;
            invalidate_blocks_panel = InvalidateBlockPanel;
            draw_background = DrawBackground;
            finish_act_sprites_cb = FinishActSpritesCB;
            select_back_cb = SelectBackCB;
            select_sprite_cb = SelectSpriteCB;
            remove_all_sprite = RemoveAllSprite;
            stop_project = StopProject;
            start_project = StartProject;
            set_recording_time = SetRecordingTime;
            show_recording_time = ShowRecordingTime;
            show_recording_btn = ShowRecordingBtn;
            release_focus = ReleaseFocus;
        }

        private void AttachTooltipRightMenu()
        {
            _right_menu_tooltip.AutoPopDelay = 5000;
            _right_menu_tooltip.InitialDelay = 100;
            _right_menu_tooltip.ReshowDelay = 500;
            _right_menu_tooltip.ShowAlways = true;

            _right_menu_tooltip.SetToolTip(btn_SelectSprite, "图片素材库");
            _right_menu_tooltip.SetToolTip(btn_SelectBackground, "背景素材库");
            _right_menu_tooltip.SetToolTip(btn_SelectEduBackground, "练习");
            _right_menu_tooltip.SetToolTip(btn_SaveProject, "保存项目");
            _right_menu_tooltip.SetToolTip(btn_OpenProject, "打开项目");
            _right_menu_tooltip.SetToolTip(btn_SelectSound, "配音");
            _right_menu_tooltip.SetToolTip(btn_Update, "刷新素材库");
            _right_menu_tooltip.SetToolTip(btn_SettingBtn, "查看公告");
        }

        private void SetMouseWheelEvent()
        {
            pnl_SpriteList.AutoScrollPosition = new Point(0, 0);
            pnl_SpriteList.VerticalScroll.Maximum = 2000;

            pnl_SpriteList.MouseWheel += Pnl_SpriteList_MouseWheel;
            pnl_EditorBox.MouseWheel += Pnl_EditorBox_MouseWheel;
        }

        private void SetStagePanelProperties()
        {
            stage_panel.BackColor = Color.Transparent;
            stage_panel.BorderStyle = BorderStyle.None;
            stage_panel.Width = pbx_Stage.Width;
            stage_panel.Height = pbx_Stage.Height;
            stage_panel.Location = new Point(0, 0);

            stage_panel.Paint += StagePaint;
            stage_panel.MouseDoubleClick += StageMouseDoubleClick;
            stage_panel.MouseDown += StageMouseDown;
            stage_panel.MouseMove += StageMouseMove;
            stage_panel.MouseUp += StageMouseUp;

            pbx_Stage.Controls.Add(stage_panel);
            stage_panel.Show();
        }

        // 수정 필요: 함수 위치 이동
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg != WM_KEYDOWN)
                return false;

            if ((StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) &&
                ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))) {
                if (keyData == Keys.Right) {
                    Block.BroadCastWithKey(2, "向右键");
                } else if (keyData == Keys.Left) {
                    Block.BroadCastWithKey(2, "向左键");
                } else if (keyData == Keys.Up) {
                    Block.BroadCastWithKey(2, "向上键");
                } else if (keyData == Keys.Down) {
                    Block.BroadCastWithKey(2, "向下键");
                } else if (keyData == Keys.Space) {
                    Block.BroadCastWithKey(2, "空格键");
                } else if (keyData >= Keys.A && keyData <= Keys.Z) {
                    Block.BroadCastWithKey(2, keyData.ToString().ToLower());
                } else {
                    return false;
                }

                return true;
            } else if ((StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) &&
                  ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))) {
                if (keyData == Keys.Left) {
                    MoveCodeBlock(true);
                } else if (keyData == Keys.Right) {
                    MoveCodeBlock(false);
                } else {
                    return false;
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Btn_StartProject_Click(object sender, EventArgs e)
        {
            StartProject();
        }

        private void Btn_StopAnimate_Click(object sender, EventArgs e)
        {
            StopProject();
        }

        // 수정 필요
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // 빠른 테스트용
            stage_player = new StagePlayer(DrawBackground, FinishStagePlayerCB);
            StagePlayer.SetFlagZero();
            StagePlayer.SetHomeImage();

            //string path = "./themes/" + Setting._language + "/Stage_Default";            
            //string audioName = path + ".wav";
            //string videoName = path + ".mp4";

            //StagePlayer.SetFlagZero();
            //StagePlayer.SetFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.LOADING, StagePlayer.Flag.NOREPEAT);

            //_stagePlayer = new StagePlayer(SafeDrawBackground, () =>
            //{
            //    _stagePlayer = new StagePlayer(SafeDrawBackground, BackThreadStopComplete);
            //    StagePlayer.SetFlagZero();
            //    StagePlayer.SetHomeImage();

            //    Music.ReleaseMusic();
            //});

            //_stagePlayer.SetBackground(new Background(null, -1, 0, videoName, null, true));
            //_stagePlayer.Start();

            //Music.SetMusic(audioName);
            //Music.PlayBackMusic();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) &&
                !StagePlayer.ORCheckFlags(StagePlayer.Flag.LOADING)) {
                e.Graphics.DrawImage(Properties.Resources.StageBackOutline, 427, 70);
            }
        }

        private void StartProject()
        {
            ReleaseFocus();

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) ||
                stage_player.GetBackground() == null) {
                return;
            }

            if (!CheckError()) {
                new MsgBoxForm("错误的编码编辑").ShowDialog();
                return;
            }

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.RECORDING)) {
                StartRecording();
            }

            Music.PlayBackMusic();

            stage_player.SaveStartBackground();
            stage_player.Start();

            RuntimeEnv.RunActivatedSprites();

            StagePlayer.SetFlags(StagePlayer.Flag.PLAYING);
            SafeInvalidateStageOutline();
        }

        private bool CheckError()
        {
            foreach (var sprite in ActivatedSpriteController.sprite_list) {
                if (!RuntimeEnv.ReadCodeValues(sprite)) {
                    return false;
                }
            }
            return true;
        }

        private void StopProject()
        {
            if (!StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) ||
                StagePlayer.ORCheckFlags(StagePlayer.Flag.LOADING)) {
                return;
            }

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.RECORDING)) {
                StopRecording();
            }

            RuntimeEnv.StopActivatedSprites();
        }

        public void FinishActSpritesCB()
        {
            stage_player.Stop();
        }

        private DChangeScreenState _change_screen_state;

        public void FinishStagePlayerCB()
        {
            if (!RuntimeEnv.is_stop)
                return;

            Music.StopBackMusic();

            StagePlayer.UnsetFlag(StagePlayer.Flag.PLAYING);
            SafeInvalidateStageOutline();

            _change_screen_state?.Invoke(false);
            _change_screen_state = null;

            stage_player.LoadStartBackground();
            stage_player.SetInitializeImage();
        }

        private void StartRecording()
        {
            if (!VideoRecord.Start()) {
                return;
            }

            btn_RecordingReady.Visible = false;
        }

        private void StopRecording()
        {
            if (!VideoRecord.Stop()) {
                return;
            }

            StagePlayer.UnsetFlag(StagePlayer.Flag.RECORDING);
            btn_RecordingReady.Visible = true;
        }

        private void Btn_RecordingReady_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.RECORDING)) {
                return;
            }

            RenameSpriteForm rename_sprite_form = new RenameSpriteForm(true);
            if (rename_sprite_form.ShowDialog() != DialogResult.OK) {
                return;
            }

            VideoRecord.SetVideoName(rename_sprite_form.new_name);
            StagePlayer.SetFlags(StagePlayer.Flag.RECORDING);
        }

        private void Btn_SelectSound_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            EffectSound.mouse_click_sound.Play();

            using SoundStorageForm sound_form = new SoundStorageForm();
            sound_form.ShowDialog();
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) {
                return;
            }

            Sprite.UpdateSpriteData();
            using var msg_box = new MsgBoxForm("刷新成功");
            msg_box.ShowDialog();
        }

        private void Btn_SettingBtn_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) {
                return;
            }

            using var setting_form = new SettingForm { change_theme = SetWindowFormTheme };
            setting_form.ShowDialog();
        }

        private void Btn_OpenDocBtn_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) {
                return;
            }

            using var about_form = new AboutForm();
            about_form.ShowDialog();
        }

        private void Btn_ReturnHome_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) {
                return;
            }

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.GAME)) {
                FinishPracticeMode();
            }

            RemoveAllSprite();
            StagePlayer.SetHomeImage();
        }

        private void Btn_Minimize_Click(object sender, EventArgs e)
        {
            EffectSound.mouse_click_sound.Play();
            WindowState = FormWindowState.Minimized;
        }

        private int _sprite_panel_pos;

        private void Pnl_SpriteList_MouseWheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;

            if (lines > 0) {
                if (_sprite_panel_pos - 50 > 0) {
                    _sprite_panel_pos -= 50;
                    pnl_SpriteList.VerticalScroll.Value = _sprite_panel_pos;
                } else {
                    _sprite_panel_pos = 0;
                    pnl_SpriteList.AutoScrollPosition = new Point(0, _sprite_panel_pos);
                }
            } else if (lines < 0) {
                if (_sprite_panel_pos + 50 < pnl_SpriteList.VerticalScroll.Maximum) {
                    _sprite_panel_pos += 50;
                    pnl_SpriteList.VerticalScroll.Value = _sprite_panel_pos;
                } else {
                    _sprite_panel_pos = pnl_SpriteList.VerticalScroll.Maximum;
                    pnl_SpriteList.AutoScrollPosition = new Point(0, _sprite_panel_pos);
                }
            }
        }

        private void Btn_ToggleBgm_Click(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            Music.is_play_bgm ^= true;
            btn_ToggleBgm.Image = (Music.is_play_bgm) ? Properties.Resources.BgmOnBtn : Properties.Resources.BgmOffBtn;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _right_menu_tooltip?.Dispose();
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y < 50) {
                _is_form_move = true;
                _form_loc.X = e.X;
                _form_loc.Y = e.Y;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _is_form_move) {
                Location = new Point(Location.X + (e.X - _form_loc.X), Location.Y + (e.Y - _form_loc.Y));
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_is_form_move) {
                return;
            }

            if (Math.Abs(Location.X) < 50 && Math.Abs(Location.Y) < 50) {
                Location = new Point(0, 0);
            }

            _is_form_move = false;
        }

        private void InvalidateEditorPanel()
        {
            if (pnl_EditorBox.InvokeRequired) {
                var d = new Action(InvalidateEditorPanel);
                pnl_EditorBox.Invoke(d);
            } else {
                pnl_EditorBox.Invalidate();
            }
        }

        private void InvalidateBlockPanel()
        {
            if (pnl_Blocks.InvokeRequired) {
                var d = new Action(InvalidateBlockPanel);
                pnl_Blocks.Invoke(d);
            } else {
                pnl_Blocks.Invalidate();
            }
        }

        private void InvalidateStageForm()
        {
            if (stage_panel.InvokeRequired) {
                var d = new Action(InvalidateStageForm);
                stage_panel.Invoke(d);
            } else {
                stage_panel.Invalidate();
            }
        }

        public void DrawBackground(Image image)
        {
            if (pbx_Stage.InvokeRequired) {
                var d = new Action<Image>(DrawBackground);
                pbx_Stage.Invoke(d, image);
            } else {
                pbx_Stage.Image?.Dispose();
                pbx_Stage.Image = image;
            }
        }

        private void SafeClearSpritePanel()
        {
            if (pnl_SpriteList.InvokeRequired) {
                var d = new Action(SafeClearSpritePanel);
                pnl_SpriteList.Invoke(d);
            } else {
                pnl_SpriteList.Controls.Clear();
            }
        }

        private void SafeInvalidateStageOutline()
        {
            if (InvokeRequired) {
                var d = new Action(SafeInvalidateStageOutline);
                Invoke(d);
            } else {
                int width = Properties.Resources.StageBackOutline.Width;
                int height = Properties.Resources.StageBackOutline.Height;
                Invalidate(new Rectangle(428, 70, width, height), false);
            }
        }

        private void SetRecordingTime(string text)
        {
            lbl_RecordingTime.ThreadSafe(x => x.Text = text);
        }

        private void ShowRecordingTime(bool is_show)
        {
            lbl_RecordingTime.ThreadSafe(x => x.Visible = is_show);
        }

        private void ShowRecordingBtn(bool is_show)
        {
            btn_RecordingReady.ThreadSafe(x => x.Visible = is_show);
        }

        private void ReleaseFocus()
        {
            this.ThreadSafe(x => x.ActiveControl = pbx_Stage);
        }

        private void SetWindowFormTheme(int idx)
        {
            if (idx == 0) {
                BackgroundImage = Properties.Resources.WindowFormblue;
            } else if (idx == 1) {
                BackgroundImage = Properties.Resources.WindowFormYellow;
            } else if (idx == 2) {
                BackgroundImage = Properties.Resources.WindowFormBlack;
            } else if (idx == 3) {
                BackgroundImage = Properties.Resources.WindowFormRed;
            }
        }
    }

    internal static class Ex
    {
        public static void Invoke(this Control control, Action action) => control.Invoke((Delegate)action);

        public static T1 ThreadSafe<T, T1>(this T control, Func<T, T1> selector) where T : Control
        {
            T1 ret = default(T1);

            if (control.InvokeRequired) {
                control.Invoke(() => ret = selector.Invoke(control));
            } else {
                ret = selector.Invoke(control);
            }

            return ret;
        }
    }
}