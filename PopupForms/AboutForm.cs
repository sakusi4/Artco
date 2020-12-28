using System;
using System.Windows.Forms;

namespace Artco
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());
        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
