using ArtcoCustomControl;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artco
{
    public partial class Back1StorageForm : Form
    {
        private readonly Action<object> _complete_handler;
        private readonly List<DoubleBufferedFlowPanel> _content_panels = new List<DoubleBufferedFlowPanel>();
        private readonly List<List<BackStorageView>> _miniviews = new List<List<BackStorageView>>();
        private readonly List<Task> _io_tasks = new List<Task>();

        private readonly int _max_tab_num = 7; // +1 index는 검색 탭으로 사용
        private readonly int _user_tab_num = 6;
        private int _cur_tab_num = 6;
        private bool _is_close;

        public Back1StorageForm(Action<object> complete_handler)
        {
            InitializeComponent();
            _complete_handler = complete_handler;
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
                    Location = new System.Drawing.Point(32, 97),
                    Size = new System.Drawing.Size(1838, 845),
                    AutoScroll = true,
                    BackColor = Color.White,
                    Visible = false
                };

                _miniviews.Add(new List<BackStorageView>());
                _content_panels.Add(content_panel);
                Controls.Add(content_panel);
            }

            _content_panels[_cur_tab_num].Visible = true;
        }

        private void CreateContents()
        {
            foreach (var back in Background.backgrounds[0][_cur_tab_num]) {
                if (_miniviews[_cur_tab_num].Exists(item => item.content_name.Equals(back.name)))
                    continue;

                var miniview = CreateStorageMiniView(back, _cur_tab_num);
                _miniviews[_cur_tab_num].Add(miniview);
                _content_panels[_cur_tab_num].Controls.Add(miniview);
            }

            Task task = new Task(() => {
                using WebClient downloader = new WebClient();

                int tab = _cur_tab_num;
                for (int i = 0; i < Background.backgrounds[0][tab].Count; i++) {
                    if (_is_close)
                        return;

                    var back = Background.backgrounds[0][tab][i];
                    var view = _miniviews[tab][i];

                    if (view.content_image != null)
                        continue;

                    if (back.is_user)
                        view.content_image = new Bitmap(back.preview_path);
                    else
                        view.content_image = ImageUtility.GetImageFromPath(back.preview_path, downloader);
                }
            });

            _io_tasks.Add(task);
            task.Start();
        }

        private BackStorageView CreateStorageMiniView(Background back, int tab_num)
        {
            BackStorageView miniview = new BackStorageView { content_name = back.name };

            miniview.MiniViewLClick += (sender, e) => {
                EffectSound.mouse_click_sound.Play();

                _complete_handler?.Invoke(back);
                CloseForm();
            };

            if (tab_num == _user_tab_num) {
                miniview.MiniViewRClick += (sender, e) => {
                    EffectSound.mouse_click_sound.Play();

                    MouseEventArgs args = (MouseEventArgs)e;
                    Utility.ShowContextMenu(sender, args.Location.X + 14, args.Location.Y + 12, new string[] { "删除" }, new List<Action<object, EventArgs>>()
                    {(sender, e) => {

                        var cur_back = MainForm.stage_player.GetBackground();
                        if(cur_back != null && cur_back.background_path.Equals(back.background_path))
                        {
                            new MsgBoxForm("素材正在使用中").ShowDialog();
                            return;
                        }

                        miniview.content_image?.Dispose();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        File.Delete(back.background_path);
                        File.Delete(back.preview_path);

                        int user_tab = Background.backgrounds[0].Count - 1;
                        Background.backgrounds[0][user_tab].Remove(back);
                        _content_panels[_user_tab_num].Controls.Remove(miniview);
                    }});
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

        private void Btn_OpenUserFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { Filter = "mp4 files (*.mp4)|*.mp4" };
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo file_info = new FileInfo(dialog.FileName);

            int cnt = 0;
            string video_path;
            string only_name;
            string preview_path;

            while (true) {
                only_name = "User" + (++cnt);
                video_path = Setting.user_back_path + "/" + only_name + ".mp4";
                if (!File.Exists(video_path))
                    break;                
            }

            preview_path = video_path.Substring(0, video_path.Length - 4) + ".jpg";            

            File.Copy(file_info.FullName, video_path, true);
            VideoCapture capture = new VideoCapture(video_path);
            Mat mat = new Mat();
            capture.Read(mat);
            OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat).Save(preview_path, ImageFormat.Jpeg);
            mat.Dispose();
            capture.Dispose();

            var back = new Background(only_name, 0, 0, video_path, preview_path, true);
            int category = Background.backgrounds[0].Count - 1;
            Background.backgrounds[0][category].Add(back);

            BackStorageView view = CreateStorageMiniView(back, category);
            view.content_image = new Bitmap(preview_path);

            _miniviews[_user_tab_num].Add(view);
            _content_panels[_user_tab_num].Controls.Add(view);
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

        private void Txtbox_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (string.IsNullOrEmpty(txtbox_Search.Text))
                return;

            ChangeTab(_max_tab_num);
            _content_panels[_cur_tab_num].Controls.Clear();

            using WebClient downloader = new WebClient();
            foreach (var backgrounds in Background.backgrounds[0]) {
                foreach (var back in backgrounds) {
                    if (_is_close)
                        break;

                    if (!back.name.Contains(txtbox_Search.Text))
                        continue;

                    var view = CreateStorageMiniView(back, _cur_tab_num);
                    if (back.is_user)
                        view.content_image = new Bitmap(back.preview_path);
                    else
                        view.content_image = ImageUtility.GetImageFromPath(back.preview_path, downloader);

                    _content_panels[_cur_tab_num].Controls.Add(view);
                }
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private async void CloseForm()
        {
            _is_close = true;

            await Task.Run(() => {
                foreach (var task in _io_tasks) {
                    task.Wait();
                }
            });

            Close();
        }
    }
}
