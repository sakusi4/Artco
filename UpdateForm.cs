using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artco
{
    public partial class UpdateForm : Form
    {
        private bool _is_restart;
        private bool _is_details;

        public UpdateForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            btn_Submit.Enabled = false;
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
            UpdateResources();
        }

        private async void UpdateResources()
        {
            await Task.Run(new Action(() => {
                var check_list = FileManager.SetDirectories(Utility.ReadSettingDirs());

                SafeUpdateStatusLabel("Checking for changed resources.......  ");
                bool is_resources = FileManager.CheckChangedResources(SafeUpdateRichTextbox, check_list);
                if (is_resources) {
                    SafeUpdateStatusLabel("Checking for changed resources....... [Change Detected]");
                    Thread.Sleep(500);

                    SafeUpdateStatusLabel("Downloading changed resources....... ");
                    FileManager.DownloadChangedResources(SafeUpdateRichTextbox);
                    SafeUpdateStatusLabel("Downloading changed resources....... [OK]");
                } else {
                    SafeUpdateStatusLabel("Checking for changed resources....... [OK]");
                }

                Thread.Sleep(1000);

                SafeUpdateStatusLabel("Checking for changed version.......  ");
                string new_ver = FileManager.GetExecutableVersion();
                if (new_ver != null) {
                    SafeUpdateStatusLabel("Downloading new version....... ");

                    FileManager.DownloadExecutableFile(SafeUpdateRichTextbox);
                    FileManager.DownloadCustomControlDll(SafeUpdateRichTextbox);
                    FileManager.UpdateVersionInfo(new_ver);

                    SafeUpdateStatusLabel("Downloading new version....... [OK]");

                    Thread.Sleep(500);
                    SafeUpdateStatusLabel("Please restart");
                    _is_restart = true;
                } else {
                    SafeUpdateStatusLabel("Checking for changed version....... [OK]");

                    Thread.Sleep(500);
                    SafeUpdateStatusLabel("Press the OK button");
                }
            }));

            btn_Submit.Enabled = true;
            prgbar_Update.Style = ProgressBarStyle.Blocks;
            prgbar_Update.Value = 100;
            prgbar_Update.Update();            
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            if (_is_restart) {
                Process.Start(Application.StartupPath + "\\Artco.exe");
                Environment.Exit(0);
            } else {
                Close();
            }
        }

        public delegate void DSafeUpdateRichTextbox(string message);

        public void SafeUpdateRichTextbox(string message)
        {
            if (richtxtbox_Update.InvokeRequired) {
                var d = new DSafeUpdateRichTextbox(SafeUpdateRichTextbox);
                richtxtbox_Update.Invoke(d, message);
            } else {
                richtxtbox_Update.AppendText(message);
                richtxtbox_Update.Refresh();
            }
        }

        public delegate void DSafeUpdateStatusLabel(string message);

        private void SafeUpdateStatusLabel(string message)
        {
            if (lbl_Status.InvokeRequired) {
                var d = new DSafeUpdateStatusLabel(SafeUpdateStatusLabel);
                lbl_Status.Invoke(d, message);
            } else {
                lbl_Status.Text = message;
                lbl_Status.Refresh();
            }
        }

        private void Lbl_Details_Click(object sender, EventArgs e)
        {
            if (!_is_details) {
                lbl_Details.Text = "Hide details";
                Height = 400;
            } else {
                lbl_Details.Text = "Show details";
                Height = 161;
            }

            _is_details ^= true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) {
                switch (keyData) {
                    case Keys.Enter:
                        if (btn_Submit.Enabled)
                            Btn_Submit_Click(null, null);
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}