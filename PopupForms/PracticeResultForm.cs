using System;
using System.Windows.Forms;

namespace Artco
{
    public partial class PracticeResultForm : Form
    {
        public int exit_code { get; set; } = -1;

        public PracticeResultForm()
        {
            InitializeComponent();
        }

        private void Btn_NextStep_Click(object sender, EventArgs e)
        {
            exit_code = 1;
            Close();
        }

        private void Btn_Restart_Click(object sender, EventArgs e)
        {
            exit_code = 0;
            Close();
        }

        private void Btn_Finish_Click(object sender, EventArgs e)
        {
            exit_code = -1;
            Close();
        }
    }
}
