using ArtcoCustomControl;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Artco
{
    internal class UserVariable
    {
        private readonly VariableMiniView _var_mini_view;
        private object _value;

        public UserVariable(string name, object value)
        {
            this._value = value;

            _var_mini_view = new VariableMiniView {
                varName = name,
                varValue = value.ToString()
            };
        }

        public void SetValue(object value)
        {
            lock (this) {
                this._value = value;
                _var_mini_view.ThreadSafe(x => x.varValue = this._value.ToString());
            }
        }

        public void ChangeValue(object value)
        {
            lock (this) {
                this._value = (double)this._value + (double)value;
                _var_mini_view.ThreadSafe(x => x.varValue = this._value.ToString());
            }
        }

        public object GetValue()
        {
            lock (this) {
                return _value;
            }
        }

        public VariableMiniView GetVariableMiniView()
        {
            return _var_mini_view;
        }
    }

    internal static class UserVariableManager
    {
        public static Dictionary<string, UserVariable> user_variables = new Dictionary<string, UserVariable>();

        public static int GetSize()
        {
            return user_variables.Count;
        }

        public static void AddVariable(string var_name, object value)
        {
            if (user_variables.ContainsKey(var_name))
                return;

            UserVariable user_variable = new UserVariable(var_name, value);
            user_variable.GetVariableMiniView().miniViewClose += UserVariableManager_miniViewClose;
            user_variables.Add(var_name, user_variable);

            AddComboBoxVarName(var_name);
            SetVarMiniViewPosition();
        }

        public static void RemoveVariable(string var_name)
        {
            user_variables.Remove(var_name);
        }

        public static void InitializeVariables()
        {
            foreach (var key in user_variables.Keys)
                user_variables[key].SetValue(0.0);
        }

        public static void ClearVariables()
        {
            foreach (var key in user_variables.Keys)
                MainForm.stage_panel.Controls.Remove(user_variables[key].GetVariableMiniView());

            user_variables.Clear();
        }

        private static void UserVariableManager_miniViewClose(object sender, System.EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            VariableMiniView miniview = (VariableMiniView)sender;

            RemoveComboBoxVarName(miniview.varName);
            RemoveVariable(miniview.varName);
            SetVarMiniViewPosition();
        }

        public static void SetComboBoxVarName(ComboBox cmbbox)
        {
            foreach (var key in user_variables.Keys)
                cmbbox.Items.Add(key);
        }

        public static void AddComboBoxSpriteName(string sprite_name)
        {
            var sprites = ActivatedSpriteController.sprite_list;
            for (int i = 0; i < sprites.Count; i++) {
                var sprite = sprites[i];
                if (sprite.name.Equals(sprite_name))
                    continue;

                var codes = sprite.code_list;
                for (int j = 0; j < codes.Count; j++) {
                    for (int k = 0; k < codes[j].Count; k++) {
                        var code = codes[j][k];
                        if (code.event_type != 3)
                            continue;

                        if (!(code.block_view.controls[0] is ComboBox control))
                            continue;

                        control.Items.Add(sprite_name);
                    }
                }
            }
        }

        private static void AddComboBoxVarName(string var_name)
        {
            var sprites = ActivatedSpriteController.sprite_list;
            for (int i = 0; i < sprites.Count; i++) {
                var sprite = sprites[i];
                var codes = sprite.code_list;
                for (int j = 0; j < codes.Count; j++) {
                    for (int k = 0; k < codes[j].Count; k++) {
                        var code = codes[j][k];
                        if (code.block_view.controls == null)
                            continue;

                        foreach (var control in code.block_view.controls) {
                            if (control is ComboBox) {
                                ((ComboBox)control).Items.Add(var_name);
                            }
                        }
                    }
                }
            }
        }

        private static void RemoveComboBoxVarName(string var_name)
        {
            var sprites = ActivatedSpriteController.sprite_list;
            for (int i = 0; i < sprites.Count; i++) {
                var sprite = sprites[i];
                var codes = sprite.code_list;
                for (int j = 0; j < codes.Count; j++) {
                    for (int k = 0; k < codes[j].Count; k++) {
                        var code = codes[j][k];
                        if (code.block_view.controls == null)
                            continue;

                        foreach (var control in code.block_view.controls) {
                            if (control is ComboBox) {
                                var combo_box = control as ComboBox;
                                combo_box.Items.Remove(var_name);
                                if (combo_box.Text.Equals(var_name))
                                    combo_box.Text = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        private static void SetVarMiniViewPosition()
        {
            MainForm.stage_panel.Controls.Clear();

            int cnt = 0;
            foreach (var key in user_variables.Keys) {
                var miniview = user_variables[key].GetVariableMiniView();
                miniview.Location = new System.Drawing.Point(10, 36 * cnt + 10);
                MainForm.stage_panel.Controls.Add(miniview);

                cnt++;
            }
        }

        public static void ShowMiniViewLists()
        {
            foreach (var key in user_variables.Keys) {
                var miniview = user_variables[key].GetVariableMiniView();
                miniview.Show();
            }
        }

        public static void HideMiniViewLists()
        {
            foreach (var key in user_variables.Keys) {
                var miniview = user_variables[key].GetVariableMiniView();
                miniview.Hide();
            }
        }
    }
}