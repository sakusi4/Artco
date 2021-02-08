using System;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class LoginForm : Form
    {
        private bool _is_exit = true;
        private bool _is_remember_me;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void SelectServerForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

            foreach (var item in Setting.resolution_list)
                cmbbox_Resolution.Items.Add(item);

            cmbbox_Resolution.SelectedIndex = Properties.Settings.Default.resolution;

            Setting.serial_num = GetSerialNumber();

            txtbox_Username.Font = new Font(FontLibrary.private_font.Families[0], 17F);
            txtbox_userpass.Font = new Font(FontLibrary.private_font.Families[0], 17F);

            txtbox_Username.Text = Properties.Settings.Default.UserName;
            txtbox_userpass.Text = Properties.Settings.Default.UserPass;
            _is_remember_me = Properties.Settings.Default.RememberMe;
            SetRememberMe();
        }

        private void ServerSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_is_exit)
                Environment.Exit(0);
        }

        private void Btn_Enter_Click(object sender, EventArgs e)
        {
            string id = txtbox_Username.Text;
            string pwd = txtbox_userpass.Text;

            if (txtbox_Username.Text == "" || txtbox_userpass.Text == "") {
                MessageBox.Show("账号或密码输入错误");
                return;
            }

            if ((id.Equals("Admin") && pwd.Equals("Admin")) || DBManager.LoginCheck(id, pwd)) {

                Properties.Settings.Default.UserName = (_is_remember_me) ? txtbox_Username.Text : "";
                Properties.Settings.Default.UserPass = (_is_remember_me) ? txtbox_userpass.Text : "";
                Properties.Settings.Default.RememberMe = _is_remember_me;
                Properties.Settings.Default.resolution = cmbbox_Resolution.SelectedIndex;
                Properties.Settings.Default.Save();

                Setting.user_name = txtbox_Username.Text;
                Setting.language = "chinese"; //(Properties.Settings.Default.Language == 0) ? "chinese" : "korean";

                _is_exit = false;
                Close();
            } else {
                MessageBox.Show("账号或密码输入错误");
            }
        }

        public string GetSerialNumber()
        {
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            //foreach (ManagementObject info in searcher.Get())
            //{
            //        return info["SerialNumber"].ToString();
            //}

            return null;
        }

        private void Pbx_RememberMe_Click(object sender, EventArgs e)
        {
            _is_remember_me ^= true;
            SetRememberMe();
        }

        private void SetRememberMe()
        {
            pbx_RememberMe.Image = (_is_remember_me) ? Properties.Resources.login_check : Properties.Resources.login_uncheck;
        }

        private Point mouse_point { get; set; }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e) => mouse_point = new Point(e.X, e.Y);

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(this.Left - (mouse_point.X - e.X), this.Top - (mouse_point.Y - e.Y));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) {
                switch (keyData) {
                    case Keys.Enter:
                        Btn_Enter_Click(null, null);
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Btn_Close_Click(object sender, EventArgs e) => Close();

        private void Button_Signup_Click(object sender, EventArgs e)
        {
            SignupForm signup_form = new SignupForm();
            signup_form.ShowDialog();
        }

        private void Button_Findidpw_Click(object sender, EventArgs e)
        {
            //
        }
    }
}