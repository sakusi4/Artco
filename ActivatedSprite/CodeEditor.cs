using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Artco
{
    public class CodeEditor : IDisposable
    {
        private readonly ActivatedSprite _sprite;

        public static TransparentPanel parent_panel { get; set; }
        public DoubleBufferedFlowPanel code_panel { get; set; }
        public Block selected_code { get; set; }
        public int range_cnt { get; set; }
        public int cur_line_num { get; set; } = -1;

        public CodeEditor(ActivatedSprite sprite)
        {
            this._sprite = sprite;

            int width = parent_panel.Width - 90;
            int height = parent_panel.Height;

            code_panel = new DoubleBufferedFlowPanel {
                Location = new Point(39, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                //Size = new Size(958, (MainForm.is_full_editor) ? 700 : 300),
                Size = new Size(width, height),
                FlowDirection = FlowDirection.LeftToRight,
                BorderStyle = BorderStyle.None,
                BackColor = Color.Transparent,
                AutoScrollPosition = new Point(0, 0)
            };

            code_panel.VerticalScroll.Maximum = 1000;
            code_panel.MouseDown += ReleaseSelectedCode;

            AddCodePanel(code_panel);
        }

        public void AddCodePanel(DoubleBufferedFlowPanel code_panel)
        {
            if (parent_panel.InvokeRequired)
                parent_panel.Invoke(new Action<DoubleBufferedFlowPanel>(AddCodePanel), code_panel);
            else
                parent_panel.Controls.Add(code_panel);
        }

        public void AddCode(Block code)
        {
            EffectSound.block_link_sound.Play();

            if (cur_line_num == -1 && code.category != 0)
                AddCode(new Block(Block.blocks[0][0]));

            // event type block
            if (code.category == 0)
                cur_line_num = _sprite.code_list.Count;

            if (_sprite.code_list.Count <= cur_line_num)
                AddNewCodeLine();

            AddNewCode(code);
            code.block_view.BlockViewLClick += BlockView_BlockViewLClick;
            code.block_view.BlockViewRClick += BlockView_BlockViewRClick;

            MainForm.invalidate_editor_panel?.Invoke();
        }

        public void AddNewCodeLine()
        {
            int width = parent_panel.Width - 90;
                        
            DoubleBufferedFlowPanel line_panel = new DoubleBufferedFlowPanel {
                FlowDirection = FlowDirection.LeftToRight,
                Size = new Size(width, 1),
                BorderStyle = BorderStyle.None,
                BackColor = Color.Transparent,
            };

            line_panel.MouseDown += ReleaseSelectedCode;

            code_panel.Controls.Add(line_panel);
            _sprite.code_list.Add(new List<Block>());
        }

        public void AddNewCode(Block code)
        {
            var codes = _sprite.code_list[cur_line_num];
            if (codes.Count > 1) {
                // 무한 동작 블럭이 앞에 있다면 블럭을 추가할 수 없음.
                var prev_block = codes[codes.Count - 1];
                if (prev_block.name.Contains("Action") && prev_block.block_type == 0) {
                    new MsgBoxForm("因为动作积木默认为无限循环，所以动作积木后不能放置其他积木").ShowDialog();
                    return;
                }
            }

            codes.Add(code);
            var line_panel = code_panel.Controls[cur_line_num];
            line_panel.Controls.Add(code.block_view);

            if (code.block_view.Location.X < code.block_view.Width)
                line_panel.Height += 105;

            selected_code = code;

            if (code.block_view.controls == null)
                return;

            foreach (var control in code.block_view.controls) {
                if (control is ComboBox) {
                    UserVariableManager.SetComboBoxVarName(control as ComboBox);
                }
            }
        }

        private void BlockView_BlockViewLClick(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            EffectSound.mouse_click_sound.Play();

            BlockView block_view = (BlockView)sender;
            var line_panel = (DoubleBufferedFlowPanel)block_view.Parent;

            int line_num = code_panel.Controls.IndexOf(line_panel);
            int code_idx = line_panel.Controls.IndexOf(block_view);

            selected_code = _sprite.code_list[line_num][code_idx];
            cur_line_num = line_num;
            range_cnt = code_idx;

            MainForm.invalidate_editor_panel?.Invoke();
        }

        public int GetRangeCount(int line, int start)
        {
            int cnt = 0;
            for (int i = start; i < _sprite.code_list[line].Count; i++) {
                string name = _sprite.code_list[line][i].name;
                if (name.Equals("ControlLoop") || name.Equals("ControlLoopN") ||
                    name.Equals("ControlCondition")) {
                    cnt++;
                } else if (name.Equals("ControlFlag")) {
                    cnt--;
                    if (cnt == 0)
                        return i;
                }
            }

            return 0;
        }

        private void BlockView_BlockViewRClick(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            BlockView_BlockViewLClick(sender, e);
            MouseEventArgs args = (MouseEventArgs)e;

            string[] titles = { "复印积木", "删除" };
            var actions = new List<Action<object, EventArgs>>() { CopyAndPasteCode, ClearCode };
            Utility.ShowContextMenu(sender, args.X, args.Y, titles, actions);
        }

        public void ClearCode(object sender = null, EventArgs e = null)
        {
            if (selected_code == null) {
                using var msgbox = new MsgBoxForm("你想要删除所有积木吗?", true);
                if (msgbox.ShowDialog() != DialogResult.Yes)
                    return;

                var codes = _sprite.code_list;
                for (int i = 0; i < codes.Count; i++)
                    for (int j = 0; j < codes[i].Count; j++)
                        codes[i][j].Dispose();

                for (int i = 0; i < codes.Count; i++)
                    codes[i].Clear();

                codes.Clear();
                code_panel.Controls.Clear();
                cur_line_num = -1;
            } else {
                RemoveCode();
            }

            MainForm.invalidate_editor_panel?.Invoke();
        }

        public void RemoveCode()
        {
            var line_panel = code_panel.Controls[cur_line_num];
            var codes = _sprite.code_list[cur_line_num];
            int next_idx = codes.IndexOf(selected_code) - 1;

            if (selected_code.category == 0) {
                for (int i = 0; i < codes.Count; i++)
                    codes[i].Dispose();

                _sprite.code_list.Remove(codes);
                code_panel.Controls.Remove(line_panel);
                cur_line_num = (_sprite.code_list.Count > 0) ? 0 : -1;
                selected_code = null;
            } else {
                codes.Remove(selected_code);
                line_panel.Controls.Remove(selected_code.block_view);

                selected_code?.Dispose();
                selected_code = (next_idx >= 0) ? codes[next_idx] : null;

                if (line_panel.Controls.Count != 0 && line_panel.Controls.Count % 9 == 0)
                    line_panel.Height -= 105;
            }
        }

        private void ReleaseSelectedCode(object sender, EventArgs e)
        {
            if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                return;
            }

            selected_code = null;
            MainForm.release_focus?.Invoke();
            MainForm.invalidate_editor_panel?.Invoke();
        }

        public void CopyAndPasteCode(object sender, EventArgs e)
        {
            if (selected_code == null)
                return;

            Block code = new Block(selected_code);
            var controls = code.block_view.controls;

            if (controls != null) {
                for (int i = 0; i < controls.Count; i++)
                    controls[i].Text = selected_code.block_view.controls[i].Text;
            }

            AddCode(code);
            selected_code = null;
        }

        public void MoveCode(bool is_left)
        {
            if (selected_code == null || selected_code.category == 0)
                return;

            var codes = _sprite.code_list;
            int line = cur_line_num;
            int idx = codes[line].IndexOf(selected_code);

            if (is_left) {
                if (idx - 1 > 0) {
                    var temp = codes[line][idx - 1];
                    codes[line][idx - 1] = selected_code;
                    codes[line][idx] = temp;
                }
            } else {
                if (idx + 1 < codes[line].Count) {
                    var temp = codes[line][idx + 1];
                    codes[line][idx + 1] = selected_code;
                    codes[line][idx] = temp;
                }
            }

            var line_panel = code_panel.Controls[line];

            line_panel.Controls.Clear();
            for (int i = 0; i < codes[line].Count; i++)
                line_panel.Controls.Add(codes[line][i].block_view);

            parent_panel.Invalidate();
        }

        public void Show()
        {
            if (code_panel.InvokeRequired)
                code_panel.Invoke(new Action(Show));
            else
                code_panel.Show();
        }

        public void Hide()
        {
            if (code_panel.InvokeRequired)
                code_panel.Invoke(new Action(Hide));
            else
                code_panel.Hide();
        }

        #region IDisposable Support
        private bool _disposed_value = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed_value) {
                if (disposing) {
                    code_panel.Controls.Clear();
                    code_panel.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed_value = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SpriteEditor()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
