using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Artco
{
    public partial class MusicStorageForm : Form
    {
        private readonly List<DoubleBufferedFlowPanel> _content_panels = new List<DoubleBufferedFlowPanel>();
        private readonly List<List<SoundStorageView>> _miniviews = new List<List<SoundStorageView>>();

        private readonly int _max_tab_num = 6; // +1 index는 검색 탭으로 사용
        private readonly int _user_tab_num = 5;
        private int _cur_tab_num = 0;        

        public MusicStorageForm()
        {
            InitializeComponent();

            Size = new Size(MainForm.form_size.Width, MainForm.form_size.Height);
#if (DEMO)
            btn_OpenUserFile.Enabled = false;
#else
            btn_OpenUserFile.Enabled = true;
#endif
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
            for (int idx = 0; idx < Music.musics[_cur_tab_num].Count; idx++) {
                var music = Music.musics[_cur_tab_num][idx];

                if (_miniviews[_cur_tab_num].Exists(item => item.content_name.Equals(music.name)))
                    continue;

                var miniview = CreateStorageMiniView(music, _cur_tab_num);
                _miniviews[_cur_tab_num].Add(miniview);
                _content_panels[_cur_tab_num].Controls.Add(miniview);
            }
        }

        public SoundPlayer preview_player;
        private SoundStorageView CreateStorageMiniView(Music music, int tab_num)
        {
            SoundStorageView miniview = new SoundStorageView { content_name = music.name };

            miniview.MiniViewLClick += (sender, e) => {
                Music.SetMusic(music.local_path);
                CloseForm();
            };

            miniview.MiniViewHover += (sender, e) => {
                preview_player = new SoundPlayer(music.local_path);
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
                                Music.musics[_user_tab_num].Remove(music);
                                _content_panels[_user_tab_num].Controls.Remove(miniview);

                                if (File.Exists(music.local_path))
                                {
                                    File.Delete(music.local_path);
                                }
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
            foreach (var musics in Music.musics) {
                foreach (var music in musics) {
                    if (!music.name.Contains(txtbox_Search.Text))
                        continue;

                    var mini_view = CreateStorageMiniView(music, _cur_tab_num);
                    _content_panels[_cur_tab_num].Controls.Add(mini_view);
                }
            }
        }

        private void Btn_OpenUserFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "wav files (*.wav)|*.wav" };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo file_info = new FileInfo(dialog.FileName);
            string music_path = Setting.user_music_path + "/" + file_info.Name;

            if (File.Exists(music_path)) {
                new MsgBoxForm("This file already exists.").ShowDialog();
                return;
            }

            File.Copy(file_info.FullName, music_path, true);

            Music.musics[_user_tab_num].Clear();
            _miniviews[_user_tab_num].Clear();
            _content_panels[_user_tab_num].Controls.Clear();

            DirectoryInfo di = new DirectoryInfo(Setting.user_music_path);
            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(".wav")) {
                    string only_name = file.Name.Substring(0, file.Name.Length - 4);
                    string local_path = file.FullName;
                    Music.musics[_user_tab_num].Add(new Music(only_name, local_path));
                }
            }

            CreateContents();
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
