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

        public static Size form_size { get; set; }
        public static Area client_area { get; set; }
        public static StagePanel stage_panel { get; set; } = new StagePanel();
        public static StagePlayer stage_player { get; set; }
        public static PracticeMode practice_mode { get; set; }
        public static Action show_practice_result_form { get; set; }
        public static Action<Background> select_back_cb { get; set; }
        public static Action<Sprite> select_sprite_cb { get; set; }
        public static Action<Image> draw_background { get; set; }
        public static Action<bool> show_recording_btn { get; set; }
        public static Action invalidate_stage_form { get; set; }
        public static Action invalidate_editor_panel { get; set; }
        public static Action invalidate_blocks_panel { get; set; }
        public static Action finish_stage_player_cb { get; set; }
        public static Action finish_act_sprites_cb { get; set; }
        public static Action remove_all_sprite { get; set; }
        public static Func<bool> start_project { get; set; }
        public static Func<bool> stop_project { get; set; }
        public static Action release_focus { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
            CodeEditor.parent_panel = pnl_EditorBox;
            SpriteView.sprite_list_panel = pnl_SpriteList;

            SetStagePanelProperties();
            SetWindowResolution(Setting.resolution_list[Properties.Settings.Default.resolution]);
            SetDelegates();
            AttachTooltipRightMenu();
            SetBlocksPosition();
            SetWindowFormTheme(Properties.Settings.Default.Theme);
            SetMouseWheelEvent();            
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

        private void SetBlocksPosition()
        {
            Block.SetBlocksPosition(pnl_Blocks.Width);
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
            stage_panel.Dock = DockStyle.Fill;
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
            } else if ((!StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING)) &&
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

        private void MainForm_Shown(object sender, EventArgs e)
        {

#if (TEST)
            stage_player = new StagePlayer(draw_background, finish_stage_player_cb);
            StagePlayer.SetFlagZero();
            StagePlayer.SetHomeImage();
#else
            string path = "./themes/" + Setting.language + "/Stage_Default";
            string audio_name = path + ".wav";
            string video_name = path + ".mp4";

            StagePlayer.SetFlagZero();
            StagePlayer.SetFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.LOADING, StagePlayer.Flag.NOREPEAT);

            stage_player = new StagePlayer(draw_background, () => {
                stage_player = new StagePlayer(draw_background, finish_stage_player_cb);
                StagePlayer.SetFlagZero();
                StagePlayer.SetHomeImage();

                Music.ReleaseMusic();
            });

            stage_player.SetBackground(new Background(null, -1, 0, video_name, null, true));
            stage_player.Start();

            Music.SetMusic(audio_name);
            Music.PlayBackMusic();
#endif
        }

        private bool StartProject()
        {
            ReleaseFocus();

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) ||
                stage_player.GetBackground() == null) {
                return false;
            }

            if (!CheckError()) {
                new MsgBoxForm("错误的编码编辑").ShowDialog();
                return false;
            }

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.RECORDING)) {
                StartRecording();
            }

            Music.PlayBackMusic();

            stage_player.SaveStartBackground();
            stage_player.Start();

            RuntimeEnv.RunActivatedSprites();

            StagePlayer.SetFlags(StagePlayer.Flag.PLAYING);

            return true;
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

        private void Btn_Run_Click(object sender, EventArgs e)
        {
            if(int.Parse(btn_Run.Tag.ToString()) == 0) {
                if (!StartProject()) 
                    return;

                btn_Run.Tag = 1;
                btn_Run.Image = Properties.Resources.Button_Stop;
            } else {

                if (!StopProject())
                    return;

                btn_Run.Tag = 0;
                btn_Run.Image = Properties.Resources.Button_Play;
            }
        }

        private bool StopProject()
        {
            if (!StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING) ||
                StagePlayer.ORCheckFlags(StagePlayer.Flag.LOADING)) {
                return false;
            }

            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.RECORDING)) {
                StopRecording();
            }

            RuntimeEnv.StopActivatedSprites();
            return true;
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

            using var setting_form = new SettingForm {
                change_theme = SetWindowFormTheme,
                change_resolution = SetWindowResolution
            };

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
                BackgroundImage = Properties.Resources.WindowFormBlue;
            } else if (idx == 1) {
                BackgroundImage = Properties.Resources.WindowFormYellow;
            } else if (idx == 2) {
                BackgroundImage = Properties.Resources.WindowFormBlack;
            } else if (idx == 3) {
                BackgroundImage = Properties.Resources.WindowFormRed;
            }
        }

        private void SetWindowResolution(string value)
        {
            SuspendLayout();
            double[] col_factors = { 0.035, 0.17, 0.54, 0.18, 0.030 };
            double[] row_factors = { 0.03, 0.6, 0.3 };
            double col_padding_factor = 0.012;
            double row_padding_factor = 0.023;

            string[] splits = value.Split('x');
            Width = int.Parse(splits[0]);
            Height = int.Parse(splits[1]) - 40;
            form_size = new Size(Width, Height);            
            client_area = new Area(Location.X, Location.Y + pnl_Topbar.Height, 
                form_size.Width, form_size.Height - pnl_Topbar.Height);


            pnl_BlockTab.Width = (int)(Width * col_factors[0]);
            pnl_Left.Width = (int)(Width * col_factors[1]);
            pnl_Center.Width = (int)(Width * col_factors[2]);
            pnl_Right.Width = (int)(Width * col_factors[3]);
            pnl_Menu.Width = (int)(Width * col_factors[4]);

            pnl_Topbar.Height = (int)(Height * row_factors[0]);
            pnl_Center.Height = (int)(Height * row_factors[1]);
            pnl_EditorBox.Height = (int)(Height * row_factors[2]);

            pnl_col_padding0.Width = (int)(Width * col_padding_factor);
            pnl_col_padding1.Width = (int)(Width * col_padding_factor);
            pnl_col_padding2.Width = (int)(Width * col_padding_factor);

            pnl_row_padding0.Height = (int)(Height * row_padding_factor);
            pnl_row_padding1.Height = (int)(Height * row_padding_factor);
            pnl_row_padding2.Height = (int)(Height * row_padding_factor);

            pnl_stage_top.Height = (int)(Height * 0.02);
            pnl_stage_bottom.Height = (int)(Height * 0.055);
            pnl_stage_left.Width = (int)(Width * 0.01);
            pnl_stage_right.Width = (int)(Width * 0.01);

            btn_CreateNewSprite.Width = (pnl_Right.Width / 2) - 10;
            btn_RemoveAllSprite.Width = (pnl_Right.Width / 2) - 10;

            btn_CreateVariable.Width = (pnl_Left.Width / 2) - 10;
            btn_HideVarList.Width = (pnl_Left.Width / 2) - 10;


            SetStageInfo();
            SetBlocksPosition();
            ActivatedSpriteController.SetSpritesViewSize();
            ResumeLayout();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _right_menu_tooltip?.Dispose();
        }

        private void Pnl_Topbar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y < 50) {
                _is_form_move = true;
                _form_loc.X = e.X;
                _form_loc.Y = e.Y;
            }
        }

        private void Pnl_Topbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _is_form_move) {
                int x = Location.X + (e.X - _form_loc.X);
                int y = Location.Y + (e.Y - _form_loc.Y);
                Location = new Point(x, y);
                client_area.x = x;
                client_area.y = y + pnl_Topbar.Height;
            }
        }

        private void Pnl_Topbar_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_is_form_move)
                return;

            if (Math.Abs(Location.X) < 50 && Math.Abs(Location.Y) < 50)
                Location = new Point(0, 0);

            _is_form_move = false;            
        }      
    }

    public class Area
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public Area(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
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