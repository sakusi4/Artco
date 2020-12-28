using System.Windows.Forms;

namespace Artco
{
    public partial class MakeConditionForm : Form
    {
        bool _left_focus = true;
        int _op_num = 0; // 1:>= , 2:> , 3:= , 4:< , 5:<=
        readonly string[] _oplist = { null, ">=", ">", "==", "<", "<=" };
        readonly TextBox _txtbox;

        public MakeConditionForm(TextBox txtbox)
        {
            InitializeComponent();
            this._txtbox = txtbox;
            Cursor = DynamicResources.cursor;

            foreach (var key in UserVariableManager.user_variables.Keys) {
                listbox_VarList.Items.Add(key.ToString());
            }
        }

        private void Btn_OK_Click(object sender, System.EventArgs e)
        {
            string l_value = txtbox_LValue.Text;
            string r_value = txtbox_RValue.Text;

            if (_op_num == 0 || string.IsNullOrEmpty(l_value) || string.IsNullOrEmpty(r_value))
                return;

            _txtbox.Text = l_value + " " + _oplist[_op_num] + " " + r_value;
            Close();
        }

        private void Btn_LGreaterEqual_Click(object sender, System.EventArgs e)
        {
            btn_CurOperator.Image = btn_LGreaterEqual.Image;
            _op_num = 1;
        }

        private void Btn_LGreater_Click(object sender, System.EventArgs e)
        {
            btn_CurOperator.Image = btn_LGreater.Image;
            _op_num = 2;
        }

        private void Btn_Equal_Click(object sender, System.EventArgs e)
        {
            btn_CurOperator.Image = btn_Equal.Image;
            _op_num = 3;
        }

        private void Btn_RGreater_Click(object sender, System.EventArgs e)
        {
            btn_CurOperator.Image = btn_RGreater.Image;
            _op_num = 4;
        }

        private void Btn_RGreaterEqual_Click(object sender, System.EventArgs e)
        {
            btn_CurOperator.Image = btn_RGreaterEqual.Image;
            _op_num = 5;
        }

        private void Btn_Cancle_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void Listbox_VarList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_left_focus) {
                txtbox_LValue.Text = listbox_VarList.Items[listbox_VarList.SelectedIndex].ToString();
            } else {
                txtbox_RValue.Text = listbox_VarList.Items[listbox_VarList.SelectedIndex].ToString();
            }
        }

        private void Txtbox_LValue_Click(object sender, System.EventArgs e)
        {
            _left_focus = true;
        }

        private void Txtbox_RValue_Click(object sender, System.EventArgs e)
        {
            _left_focus = false;
        }
    }
}
