using System;
using System.Windows.Forms;

namespace Artco
{
    public partial class MsgBoxForm : Form
    {
        public MsgBoxForm(string text, bool is_yes_or_no = false)
        {
            InitializeComponent();

            txtbox_Text.Text = text;
            txtbox_Text.BackColor = txtbox_Text.BackColor;
            txtbox_Text.Font = DynamicResources.font;

            if (is_yes_or_no) {
                btn_Yes.Visible = true;
                btn_No.Visible = true;

                btn_Yes.Image = DynamicResources.b_msg_box_yes_btn;
                btn_No.Image = DynamicResources.b_msg_box_no_btn;
            } else {
                btn_Ok.Visible = true;
                btn_Ok.Image = DynamicResources.b_msg_box_ok_btn;
            }
        }

        private void MsgBoxForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
            pnl_Back.BackgroundImage = DynamicResources.b_msg_box_form;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            CloseBtn(DialogResult.OK);
        }

        private void Btn_Yes_Click(object sender, EventArgs e)
        {
            CloseBtn(DialogResult.Yes);
        }

        private void Btn_No_Click(object sender, EventArgs e)
        {
            CloseBtn(DialogResult.No);
        }

        private void CloseBtn(DialogResult dialog_result)
        {
            DialogResult = dialog_result;
            Close();
        }
    }
}