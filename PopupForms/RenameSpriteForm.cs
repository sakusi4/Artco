using System;
using System.Windows.Forms;

namespace Artco
{
    public partial class RenameSpriteForm : Form
    {
        public string new_name { get; set; } = string.Empty;

        public RenameSpriteForm(bool is_recording = false)
        {
            InitializeComponent();

            BackgroundImage = (is_recording) ? Properties.Resources.RenameForm_EN : Properties.Resources.RenameForm_CH;
            btn_Submit.Image = DynamicResources.b_msg_box_ok_btn;
        }

        private void RenameSpriteForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
        }

        private void Btn_Submit_Click(object sender, EventArgs e)
        {
            string name = txt_NewName.Text;
            if (string.IsNullOrEmpty(name))
                return;

            if (name.Length < 15) {
                new_name = name;
                CloseForm(DialogResult.OK);
            } else {
                new MsgBoxForm("只有韩文,英语或数字组成的9字以下的名字才可以").ShowDialog();
                return;
            }
        }

        private void Txt_NewName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Btn_Submit_Click(null, null);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            CloseForm(DialogResult.Cancel);
        }

        private void CloseForm(DialogResult dialog_result)
        {
            DialogResult = dialog_result;
            Close();
        }
    }
}