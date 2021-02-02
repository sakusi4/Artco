using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Artco
{
    public partial class ProjectStorageForm : Form
    {
        private readonly Action<object> _complete_handler;
        private readonly List<DoubleBufferedFlowPanel> _content_panels = new List<DoubleBufferedFlowPanel>();
        private readonly List<List<ProjectStorageView>> _miniviews = new List<List<ProjectStorageView>>();

        private readonly int _max_tab_num = 2; // +1 index는 검색 탭으로 사용        
        private int _cur_tab_num = 0;

        public ProjectStorageForm(Action<object> complete_handler)
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

                _miniviews.Add(new List<ProjectStorageView>());
                _content_panels.Add(content_panel);
                pnl_Contents.Controls.Add(content_panel);
            }

            _content_panels[_cur_tab_num].Visible = true;
        }

        private void CreateContents()
        {
            while (_cur_tab_num >= _miniviews.Count)
                _miniviews.Add(new List<ProjectStorageView>());

            string filter = (_cur_tab_num == 0) ? ".artcoproj" : ".artcoobj";
            string filter_old = (_cur_tab_num == 0) ? ".ArtcoProject" : ".ArtcoObject";

            DirectoryInfo di = new DirectoryInfo(Setting.save_path);
            foreach (var file in di.GetFiles()) {
                if (file.Extension.Equals(filter) || file.Extension.Equals(filter_old)) {
                    string name = file.Name.Split('.')[0];
                    if (_miniviews[_cur_tab_num].Exists(item => item.content_name.Equals(name)))
                        continue;

                    ProjectStorageView miniview = new ProjectStorageView { content_name = name };
                    miniview.SetBackImage(_cur_tab_num);

                    miniview.MiniViewLClick += (sender, e) => {
                        _complete_handler?.Invoke(file.FullName);
                        CloseForm();
                    };

                    miniview.MiniViewDelete += (sender, e) => {
                        string path = file.FullName;
                        if (File.Exists(path)) {
                            File.Delete(path);
                            _content_panels[_cur_tab_num].Controls.Remove(miniview);
                        }
                    };

                    _miniviews[_cur_tab_num].Add(miniview);
                    _content_panels[_cur_tab_num].Controls.Add(miniview);
                }
            }
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
