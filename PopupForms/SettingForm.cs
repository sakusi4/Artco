using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Artco
{
    public partial class SettingForm : Form
    {
        public delegate void ChangeTheme(int idx);

        public ChangeTheme change_theme { get; set; }

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

            txtbox_SaveFolder.Text = Setting.save_path;
            txtbox_VideoPath.Text = Setting.video_path;
            txtbox_AudioPath.Text = Setting.user_sound_path;

            lbl_UserName.Text = Setting.user_name;
            lbl_SerialCode.Text = Setting.serial_num;

            cmbbox_Language.SelectedIndex = Properties.Settings.Default.Language;
            cmbbox_Theme.SelectedIndex = Properties.Settings.Default.Theme;
            cmbbox_Channels.SelectedIndex = 0;
            cmbbox_SampleRate.SelectedIndex = 0;
            cmbbox_AudioFormat.SelectedIndex = 0;

            LoadAudioDevices();
        }

        private void LoadAudioDevices()
        {
            var devices = Setting.devices;
            cmbbox_InputDevices.DataSource = devices;
            cmbbox_InputDevices.DisplayMember = "ProductName";
        }

        private void Cmbbox_InputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setting.device_number = cmbbox_InputDevices.SelectedIndex - 1;
        }

        private void Cmbbox_Theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            change_theme?.Invoke(cmbbox_Theme.SelectedIndex);
        }

        private void Btn_OpenSaveFolder_Click(object sender, EventArgs e)
        {
            Process.Start(Setting.save_path);
        }

        private void Btn_ChangeVideoPath_Click(object sender, EventArgs e)
        {
            Process.Start(Setting.video_path);
        }

        private void Btn_ChangeAudioPath_Click(object sender, EventArgs e)
        {
            Process.Start(Setting.user_sound_path);
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Language = cmbbox_Language.SelectedIndex;
            Properties.Settings.Default.Theme = cmbbox_Theme.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}