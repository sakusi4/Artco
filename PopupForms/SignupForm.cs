using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artco
{
    public partial class SignupForm : Form
    {
        private UserInfo _user_info;
        private bool[] _personalinfo_chk;
        private Bunifu.Framework.UI.BunifuImageButton[] _personalinfo_chk_btn;
        private bool[] _userinfo_condition;

        public SignupForm()
        {
            InitializeComponent();

            _user_info = new UserInfo();
            _user_info.birth = new string[3];
            _personalinfo_chk = new bool[5];
            _personalinfo_chk_btn = new Bunifu.Framework.UI.BunifuImageButton[5];
            _personalinfo_chk_btn[0] = this.button_Personalinfo1;
            _personalinfo_chk_btn[1] = this.button_Personalinfo2;
            _personalinfo_chk_btn[2] = this.button_Personalinfo3;
            _personalinfo_chk_btn[3] = this.button_Personalinfo4;
            _personalinfo_chk_btn[4] = this.button_Personalinfo5;
            _userinfo_condition = new bool[8];
        }

        private void SignupForm_Load(object sender, EventArgs e)
        {
            this.panel_Personalinfo.Visible = true;
            this.panel_Signup.Visible = false;

            this.label_IDC.Visible = false;
            this.label_PWC.Visible = false;
            this.label_PWrC.Visible = false;
            this.label_NameC.Visible = false;
            this.label_BirthC.Visible = false;
            this.label_EmailC.Visible = false;
        }

        private void Button_Personalinfo_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Personalinfo_Ok_Click(object sender, EventArgs e)
        {
            if (_personalinfo_chk.Contains(false)) {
                MessageBox.Show("请先阅读并同意服务协议", "个人信息收集与保护");
            } else {
                this.panel_Personalinfo.Visible = false;
                this.panel_Signup.Visible = true;
            }
        }

        private void Button_Personalinfo1_Click(object sender, EventArgs e)
        {
            PersonalinfoChk(0);
        }

        private void Button_Personalinfo2_Click(object sender, EventArgs e)
        {
            PersonalinfoChk(1);
        }

        private void Button_Personalinfo3_Click(object sender, EventArgs e)
        {
            PersonalinfoChk(2);
        }

        private void Button_Personalinfo4_Click(object sender, EventArgs e)
        {
            PersonalinfoChk(3);
        }

        private void Button_Personalinfo5_CLick(object sender, EventArgs e)
        {
            PersonalinfoChk(4);
        }

        private void PersonalinfoChk(int index)
        {
            _personalinfo_chk[index] = _personalinfo_chk[index] ? false : true;
            this._personalinfo_chk_btn[index].Image = _personalinfo_chk[index] ?
                global::Artco.Properties.Resources.PersonalInfoChk : global::Artco.Properties.Resources.PersonalInfoUnChk;
        }

        private void Button_Signup_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Signup_Ok_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox_id.Text) || string.IsNullOrEmpty(textBox_pw.Text) || 
                string.IsNullOrEmpty(textBox_pwr.Text) || string.IsNullOrEmpty(textBox_name.Text) ||
                string.IsNullOrEmpty(textBox_year.Text) || string.IsNullOrEmpty(textBox_month.Text) ||
                string.IsNullOrEmpty(textBox_day.Text) || string.IsNullOrEmpty(textBox_email.Text)) {
                return;
            } else {
                //need server work
                this.Close();
            }
        }

        private void TextBox_ID_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-z0-9_\-]{5,20}$");

            if (regex.IsMatch(textBox_id.Text)) {
                this._user_info.id = textBox_id.Text;
                this.label_IDC.Visible = false;
            } else {
                this.label_IDC.Visible = true;
            }
        }

        private void TextBox_PW_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9_\-]{8,16}$");

            if (regex.IsMatch(textBox_pw.Text)) {
                this._user_info.pw = textBox_pw.Text;
                this.label_PWC.Visible = false;
            } else {
                this.label_PWC.Visible = true;
            }
        }

        private void TextBox_PWR_Leav(object sender, EventArgs e)
        {
            if(this._user_info.pw == textBox_pwr.Text) {
                this.label_PWrC.Visible = false;
            } else {
                this.label_PWrC.Visible = true;
            }
        }

        private void TextBox_Name_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_name.Text)) {
                this._user_info.name = textBox_name.Text;
                this.label_NameC.Visible = false;
            } else {
                this.label_NameC.Visible = true;
            }
        }

        private void TextBox_Year_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9_\-]{4,4}$");

            if (regex.IsMatch(textBox_year.Text)) {
                this._user_info.birth[0] = textBox_year.Text;
                if(!string.IsNullOrEmpty(textBox_month.Text) && !string.IsNullOrEmpty(textBox_day.Text)) {
                    this.label_BirthC.Visible = false;
                } else {
                    this.label_BirthC.Visible = true;
                }
            } else {
                this.label_BirthC.Visible = true;
            }
        }

        private void TextBox_Month_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9_\-]{1,2}$");

            if (regex.IsMatch(textBox_month.Text)) {
                this._user_info.birth[1] = textBox_month.Text;
                if (!string.IsNullOrEmpty(textBox_year.Text) && !string.IsNullOrEmpty(textBox_day.Text)) {
                    this.label_BirthC.Visible = false;
                } else {
                    this.label_BirthC.Visible = true;
                }
            } else {
                this.label_BirthC.Visible = true;
            }
        }

        private void TextBox_Day_Leave(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9_\-]{1,2}$");

            if (regex.IsMatch(textBox_day.Text)) {
                this._user_info.birth[2] = textBox_day.Text;
                if (!string.IsNullOrEmpty(textBox_year.Text) && !string.IsNullOrEmpty(textBox_month.Text)) {
                    this.label_BirthC.Visible = false;
                } else {
                    this.label_BirthC.Visible = true;
                }
            } else {
                this.label_BirthC.Visible = true;
            }
        }

        private void TextBox_Email_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox_email.Text)) {
                this._user_info.email = textBox_email.Text;
                this.label_EmailC.Visible = false;
            } else {
                this.label_EmailC.Visible = true;
            }
        }
    }

    public struct UserInfo
    {
        public string id { get; set; }
        public string pw { get; set; }
        public string name { get; set; }
        public string[] birth { get; set; }
        public string email { get; set; }
    }
}
