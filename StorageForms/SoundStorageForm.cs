using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Artco
{
    public partial class SoundStorageForm : Form
    {
        private readonly Action<object> _complete_handler;
        private readonly List<DoubleBufferedFlowPanel> _content_panels = new List<DoubleBufferedFlowPanel>();
        private readonly List<List<SoundStorageView>> _miniviews = new List<List<SoundStorageView>>();

        private readonly int _max_tab_num = 6; // +1 index는 검색 탭으로 사용
        private readonly int _user_tab_num = 5;
        private int _cur_tab_num = 0;

        public SoundStorageForm(Action<object> complete_handler = null)
        {
            InitializeComponent();
            _complete_handler = complete_handler;

            Size = new Size(MainForm.form_size.Width, MainForm.form_size.Height);
        }

        private void StorageForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

            CreatePanels();
            CreateContents();
        }
        private void CreatePanels()
        {
            for (int i = 0; i < _max_tab_num + 1; i++) {
                DoubleBufferedFlowPanel content_panel = new DoubleBufferedFlowPanel {
                    Location = new Point(10, 10),
                    Size = new Size(pnl_Contents.Width - 30, pnl_Contents.Height - 50),
                    AutoScroll = true,
                    BackColor = Color.White,
                    Visible = false
                };

                _miniviews.Add(new List<SoundStorageView>());
                _content_panels.Add(content_panel);
                pnl_Contents.Controls.Add(content_panel);
            }

            _content_panels[_cur_tab_num].Visible = true;
        }

        private void CreateContents()
        {
            for (int idx = 0; idx < Sound.sounds[_cur_tab_num].Count; idx++) {
                var sound = Sound.sounds[_cur_tab_num][idx];

                if (_miniviews[_cur_tab_num].Exists(item => item.content_name.Equals(sound.name)))
                    continue;

                var miniview = CreateStorageMiniView(sound, _cur_tab_num);
                _miniviews[_cur_tab_num].Add(miniview);
                _content_panels[_cur_tab_num].Controls.Add(miniview);
            }
        }

        public SoundPlayer preview_player;
        private SoundStorageView CreateStorageMiniView(Sound sound, int tab_num)
        {
            SoundStorageView miniview = new SoundStorageView { content_name = sound.name };

            miniview.MiniViewLClick += (sender, e) => {
                _complete_handler?.Invoke(sound.name);
                CloseForm();
            };

            miniview.MiniViewHover += (sender, e) => {
                preview_player = new SoundPlayer(sound.local_path);
                preview_player?.Play();
            };

            miniview.MiniViewLeave += (sender, e) => {
                preview_player?.Stop();
                preview_player?.Dispose();
            };

            if (tab_num == _user_tab_num) {
                miniview.MiniViewRClick += (sender, e) => {
                    var args = (MouseEventArgs)e;
                    Utility.ShowContextMenu(sender, args.Location.X, args.Location.Y,
                        new string[] { "删除" }, new List<Action<object, EventArgs>>()
                        {
                            (sender, e) =>
                            {
                                Sound.sounds[_user_tab_num].Remove(sound);
                                _content_panels[_user_tab_num].Controls.Remove(miniview);

                                if (File.Exists(sound.local_path))
                                    File.Delete(sound.local_path);
                            }
                        });
                };
            }

            return miniview;
        }

        private void ChangeTab(int new_tab_num)
        {
            if (new_tab_num == _cur_tab_num)
                return;

            _content_panels[_cur_tab_num].Visible = false;
            _content_panels[new_tab_num].Visible = true;
            _cur_tab_num = new_tab_num;

            pnl_Tabs.Invalidate();
        }

        private void Btn_Tab_Click(object sender, EventArgs e)
        {
            int new_tab_num = int.Parse(((Control)sender).Tag.ToString());
            ChangeTab(new_tab_num);
            CreateContents();
        }

        private void Txtbox_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (string.IsNullOrEmpty(txtbox_Search.Text))
                return;

            ChangeTab(_max_tab_num);
            _content_panels[_cur_tab_num].Controls.Clear();
            foreach (var sounds in Sound.sounds) {
                foreach (var sound in sounds) {
                    if (!sound.name.Contains(txtbox_Search.Text))
                        continue;

                    var miniview = CreateStorageMiniView(sound, _cur_tab_num);
                    _content_panels[_cur_tab_num].Controls.Add(miniview);
                }
            }
        }

        private void Btn_OpenRecordingForm_Click(object sender, EventArgs e)
        {
#if (FREE)
            MessageBox.Show("应版权方要求，需购买付费版Artco软件开通ⅤIP权限", "Artco");
#else
            using AudioRecordingForm audio_recording_form = new AudioRecordingForm();
            audio_recording_form.ShowDialog();

            Sound.sounds[_user_tab_num].Clear();
            _miniviews[_user_tab_num].Clear();
            _content_panels[_user_tab_num].Controls.Clear();

            DirectoryInfo di = new DirectoryInfo(Setting.user_sound_path);
            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(".wav")) {
                    string only_name = file.Name.Substring(0, file.Name.Length - 4);
                    string local_path = file.FullName;
                    Sound.sounds[_user_tab_num].Add(new Sound(only_name, local_path));
                }
            }

            CreateContents();
#endif
        }

        private void Btn_OpenUserFile_Click(object sender, EventArgs e)
        {
#if (FREE)
            MessageBox.Show("应版权方要求，需购买付费版Artco软件开通ⅤIP权限", "Artco");
#else
            OpenFileDialog dialog = new OpenFileDialog { Filter = "wav files (*.wav)|*.wav" };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo file_info = new FileInfo(dialog.FileName);
            string sound_path = Setting.user_sound_path + "/" + file_info.Name;

            if (File.Exists(sound_path)) {
                new MsgBoxForm("This file already exists.").ShowDialog();
                return;
            }

            File.Copy(file_info.FullName, sound_path, true);

            Sound.sounds[_user_tab_num].Clear();
            _miniviews[_user_tab_num].Clear();
            _content_panels[_user_tab_num].Controls.Clear();

            DirectoryInfo di = new DirectoryInfo(Setting.user_sound_path);
            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(".wav")) {
                    string only_name = file.Name.Substring(0, file.Name.Length - 4);
                    string local_path = file.FullName;
                    Sound.sounds[_user_tab_num].Add(new Sound(only_name, local_path));
                }
            }

            CreateContents();
#endif
        }

        private void Pnl_Tabs_Paint(object sender, PaintEventArgs e)
        {
            if (_cur_tab_num == _max_tab_num)
                return;

            var control = pnl_Tabs.Controls[_cur_tab_num];

            int x = control.Location.X - 3;
            int y = control.Location.Y - 3;
            int width = control.Width + 6;
            int height = control.Height + 6;

            e.Graphics.DrawImage(Properties.Resources.StorageSelectTab, x, y, width, height);
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            Close();
        }
    }
}
