using System;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class SpeakInputForm : Form
    {
        private readonly TextBox _text_box;

        public SpeakInputForm(TextBox text_box)
        {
            InitializeComponent();

            BackgroundImage = DynamicResources.b_speak_text_form;
            btn_Submit.Image = DynamicResources.b_msg_box_ok_btn;
            btn_Close.Image = DynamicResources.b_msg_box_cancel_btn;

            _text_box = text_box;
            txtbox_SpeakText.Text = _text_box.Text;

            foreach (var key in UserVariableManager.user_variables.Keys) {
                listbox_VarList.Items.Add(key.ToString());
            }
        }

        private void SpeakInputForm_Load(object sender, System.EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
        }

        private void Txtbox_SpeakText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Btn_Submit_Click(null, null);
            }
        }

        private void Btn_Close_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void Btn_Submit_Click(object sender, System.EventArgs e)
        {
            _text_box.Text = txtbox_SpeakText.Text;
            Close();
        }

        private void Listbox_Varlist_Click(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            int selected_idx = listbox_VarList.IndexFromPoint(point);

            if (selected_idx != -1)
            {
                string selected_item = listbox_VarList.Items[selected_idx] as string;
                txtbox_SpeakText.Text += "{" + selected_item + "}";
            }
        }
    }
}