using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public partial class CreateVarForm : Form
    {
        public string var_name { get; set; }
        public object var_valaue { get; set; }

        public CreateVarForm()
        {
            InitializeComponent();
        }

        private void CreateVarForm_Load(object sender, System.EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

            txtbox_VarName.Font = new Font(FontLibrary.private_font.Families[0], 17F);
            txtbox_VarInitVal.Font = new Font(FontLibrary.private_font.Families[0], 17F);
        }

        private void Btn_Close_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Btn_Cancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Btn_OK_Click(object sender, System.EventArgs e)
        {
            var_name = txtbox_VarName.Text;
            if (string.IsNullOrEmpty(txtbox_VarInitVal.Text)) {
                var_valaue = 0;
            } else {
                if (double.TryParse(txtbox_VarInitVal.Text, out double n)) {
                    var_valaue = n;
                } else {
                    var_valaue = txtbox_VarInitVal.Text;
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private Point _mouse_point;

        private void PnlTitleMouseDown(object sender, MouseEventArgs e)
        {
            _mouse_point = new Point(e.X, e.Y);
        }

        private void PnlTitleMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                Location = new Point(this.Left - (_mouse_point.X - e.X), this.Top - (_mouse_point.Y - e.Y));
        }
    }
}