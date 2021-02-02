using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artco
{
    public partial class SpriteStorageForm : Form
    {
        private readonly Action<object> _complete_handler;
        private readonly List<DoubleBufferedFlowPanel> _content_panels = new List<DoubleBufferedFlowPanel>();
        private readonly List<List<SpriteStorageView>> _miniviews = new List<List<SpriteStorageView>>();
        private readonly List<Task> _io_tasks = new List<Task>();

        private readonly int _max_tab_num = 11; // +1 index는 검색 탭으로 사용
        private readonly int _user_tab_num = 0;
        private int _cur_tab_num = 0;
        private bool _is_close;

        public SpriteStorageForm(Action<object> complete_handler)
        {
            InitializeComponent();
            _complete_handler = complete_handler;

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

                _miniviews.Add(new List<SpriteStorageView>());
                _content_panels.Add(content_panel);
                pnl_Contents.Controls.Add(content_panel);
            }

            _content_panels[_cur_tab_num].Visible = true;
        }

        private void CreateContents()
        {
            foreach (var sprite in Sprite.sprites[_cur_tab_num]) {
                if (_miniviews[_cur_tab_num].Exists(item => item.content_name.Equals(sprite.name)))
                    continue;

                var miniview = CreateStorageMiniView(sprite, _cur_tab_num);
                _miniviews[_cur_tab_num].Add(miniview);
                _content_panels[_cur_tab_num].Controls.Add(miniview);
            }

            Task task = new Task(() => {
                using WebClient downloader = new WebClient();

                int tab = _cur_tab_num;
                for (int i = 0; i < Sprite.sprites[tab].Count; i++) {
                    if (_is_close)
                        return;

                    var sprite = Sprite.sprites[tab][i];
                    var view = _miniviews[tab][i];

                    if (view.content_image != null)
                        continue;

                    if (tab == _user_tab_num)
                        view.content_image = new Bitmap(sprite.user_tab_preview_img);
                    else
                        view.content_image = ImageUtility.GetImageFromPath(sprite.sprite_path, downloader);
                }
            });

            _io_tasks.Add(task);
            task.Start();
        }

        private SpriteStorageView CreateStorageMiniView(Sprite sprite, int tab_num)
        {
            SpriteStorageView miniview = new SpriteStorageView { content_name = sprite.name };

            miniview.MiniViewLClick += (sender, e) => {
                EffectSound.mouse_click_sound.Play();

                _complete_handler?.Invoke(sprite);
                CloseForm();
            };

            if (tab_num == _user_tab_num) {
                miniview.MiniViewRClick += (sender, e) => {
                    EffectSound.mouse_click_sound.Play();

                    MouseEventArgs args = (MouseEventArgs)e;
                    Utility.ShowContextMenu(sender, args.Location.X + 14, args.Location.Y + 12, new string[] { "删除" }, new List<Action<object, EventArgs>>()
                    {(sender, e) => {
                        Sprite.sprites[0].Remove(sprite);
                        FileManager.DeleteFileFromFTP(sprite.sprite_path);
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
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files (*.png)|*.png";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo file_info = new FileInfo(dialog.FileName);
            if (!file_info.Extension.Equals(".png")) {
                MessageBox.Show("png 파일이 아닙니다");
                return;
            }

            Sprite sprite = new Sprite(file_info.Name.Substring(0, file_info.Name.Length - 4), null, false, null);
            sprite.SetTmpImgList(new List<Bitmap>() { new Bitmap(file_info.FullName) });

            _complete_handler?.Invoke(sprite);
            CloseForm();
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
            foreach (var sprites in Sprite.sprites) {
                foreach (var sprite in sprites) {
                    if (_is_close)
                        break;

                    if (!sprite.name.Contains(txtbox_Search.Text))
                        continue;

                    var miniview = CreateStorageMiniView(sprite, _cur_tab_num);
                    miniview.content_image = ImageUtility.GetImageFromPath(sprite.sprite_path, downloader);
                    _content_panels[_cur_tab_num].Controls.Add(miniview);
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
