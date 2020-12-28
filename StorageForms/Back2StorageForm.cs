using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artco
{
    public partial class Back2StorageForm : Form
    {
        private readonly Action<object> _complete_handler;
        private readonly List<DoubleBufferedFlowPanel> _content_panels = new List<DoubleBufferedFlowPanel>();
        private readonly List<List<BackStorageView>> _miniviews = new List<List<BackStorageView>>();
        private readonly List<Task> _io_tasks = new List<Task>();

        private readonly int _max_tab_num = 5; // +1 index는 검색 탭으로 사용        
        private int _cur_tab_num = 0;
        private bool _is_close;

        public Back2StorageForm(Action<object> complete_handler)
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
                    Location = new Point(32, 97),
                    Size = new Size(1838, 845),
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
            foreach (var back in Background.backgrounds[1][_cur_tab_num]) {
                if (_miniviews[_cur_tab_num].Exists(item => item.content_name.Equals(back.name)))
                    continue;

                var miniview = CreateStorageMiniView(back, _cur_tab_num);
                _miniviews[_cur_tab_num].Add(miniview);
                _content_panels[_cur_tab_num].Controls.Add(miniview);
            }

            Task task = new Task(() => {
                using WebClient downloader = new WebClient();

                int tab = _cur_tab_num;
                for (int i = 0; i < Background.backgrounds[1][tab].Count; i++) {
                    if (_is_close)
                        return;

                    var back = Background.backgrounds[1][tab][i];
                    var view = _miniviews[tab][i];

                    if (view.content_image != null)
                        continue;

                    view.content_image = ImageUtility.GetImageFromURL(back.preview_path, downloader);
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
            foreach (var backgrounds in Background.backgrounds[1]) {
                foreach (var back in backgrounds) {
                    if (_is_close)
                        break;

                    if (!back.name.Contains(txtbox_Search.Text))
                        continue;

                    var view = CreateStorageMiniView(back, _cur_tab_num);
                    view.content_image = ImageUtility.GetImageFromURL(back.preview_path, downloader);

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
