using ArtcoCustomControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Artco
{
    public partial class Block : IDisposable
    {
        public static Dictionary<string, MethodInfo> funcs = new Dictionary<string, MethodInfo>();
        public static List<List<Block>> blocks = new List<List<Block>>();
        public static int selected_category = 1;

        public string name;
        public int category;
        public int vx; // block panel x pos
        public int vy; // block panel y pos
        public int width; // block image size
        public int block_type;
        public int event_type;
        public string[] values;
        public Bitmap block_img;
        public BlockView block_view;

        public Block(string name, int category, int vx, int vy, int block_type, int event_type, Bitmap block_bit, string[] values)
        {
            this.name = name;
            this.category = category;
            this.vx = vx;
            this.vy = vy;
            this.block_type = block_type;
            this.event_type = event_type;
            this.block_img = block_bit;
            this.values = values;
        }

        public Block(Block block, bool is_clone = false) : this(block.name, block.category, block.vx, block.vy, block.block_type, block.event_type, block.block_img, block.values)
        {
            block_img = new Bitmap(block.block_img);

            // 복제 블럭은 실제 컨트롤이 보이지 않으므로 생성하지 않음
            if (!is_clone)
                SetBlockView();
        }

        public Block(BlockView block_view)
        {
            this.block_view = block_view;
        }

        public void SetBlockView()
        {
            if (block_type == 0)
                block_view = new BlockType0();
            else if (block_type == 1)
                block_view = new BlockType1();
            else if (block_type == 2)
                block_view = new BlockType2();
            else if (block_type == 3)
                block_view = new BlockType3();
            else if (block_type == 4)
                block_view = new BlockType4();
            else if (block_type == 5)
                block_view = new BlockType5(ActivatedSpriteController.GetNamesWithoutMe());
            else if (block_type == 6)
                block_view = new BlockType6();
            else if (block_type == 7)
                block_view = new BlockType7();

            block_view.BackgroundImage = new Bitmap(block_img);            
            block_view.Size = new Size(99, 99);

            if (name.Equals("ControlSound")) {
                ((BlockType2)block_view).txtbox.Click += (sender, e) => {
                    if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                        return;
                    }

                    using SoundStorageForm sound_form = new SoundStorageForm((sound_name) => {
                        ((BlockType2)block_view).txtbox.Text = (string)sound_name;
                    });
                    sound_form.ShowDialog();
                };
            } else if (name.Equals("ControlSpeak")) {
                ((BlockType2)block_view).txtbox.Click += (sender, e) => {
                    if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                        return;
                    }

                    ((BlockType2)block_view).txtbox.ReadOnly = true;
                    new SpeakInputForm(sender as TextBox).ShowDialog();
                };
            } else if (name.Equals("ControlChangeBack")) {
                ((BlockType2)block_view).txtbox.Click += (sender, e) => {
                    if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                        return;
                    }

                    Back1StorageForm back1_form = new Back1StorageForm((back) => {
                        TextBox text_box = (TextBox)sender;
                        text_box.Text = ((Background)back).name;
                    });
                    back1_form.Show();
                };
            } else if (name.Equals("ControlCondition")) {
                ((BlockType7)block_view).txtbox.Click += (sender, e) => {
                    if (StagePlayer.ORCheckFlags(StagePlayer.Flag.PLAYING, StagePlayer.Flag.GAME)) {
                        return;
                    }

                    var txtbox = sender as TextBox;
                    new MakeConditionForm(txtbox).ShowDialog();
                    txtbox.Enabled = false;
                    txtbox.Enabled = true;
                };
            }
        }

        public static void AddBlockData(string[] datas)
        {
            const int row_cnt = 7;
            for (int i = 0; i <= datas.Length - row_cnt; i += row_cnt) {
                string name = datas[i + 1];
                int category = int.Parse(datas[i + 2]);
                int block_type = int.Parse(datas[i + 3]);
                Bitmap block_bit = DynamicResources.block_images[name];
                int event_type = (!string.IsNullOrEmpty(datas[i + 6])) ? int.Parse(datas[i + 6]) : -1;

                if (category >= blocks.Count)
                    blocks.Add(new List<Block>());

                blocks[category].Add(new Block(name, category, 0, 0, block_type, event_type, block_bit, null));
            }
            
            SetBlockFunctions();
        }

        public static Block GetBlockByName(string name)
        {
            foreach (var tab in blocks) {
                var ret = tab.Find(item => item.name.Equals(name));
                if (ret != null)
                    return ret;
            }

            return null;
        }

        public static void SetBlocksPosition(int pnl_width)
        {
            int x, y;
            int row, col;
            int width = (pnl_width / 3) - 10;
            int height = width;

            for (int i = 0; i < blocks.Count; i++) {
                row = 0;
                col = 0;
                for (int j = 0; j < blocks[i].Count; j++) {
                    x = (width * col) + (5 * col) + 10;
                    if (col == 3) {
                        x = 10;
                        col = 0;
                        row += 1;
                    }
                    y = (height * row) + (5 * row) + 10;

                    blocks[i][j].vx = x;
                    blocks[i][j].vy = y;
                    blocks[i][j].width = width;

                    col += 1;
                }
            }
        }

        public static Block IsInsideBlock(int x, int y)
        {
            return blocks[selected_category].Find((item) => {
                int right_up_x = item.vx + item.block_img.Width;
                int left_bot_y = item.vy + item.block_img.Height;

                return (item.vx < x && right_up_x > x && item.vy < y && left_bot_y > y);
            });
        }

        public static void SetBlockFunctions()
        {
            for (int i = 0; i < blocks.Count; i++) {
                for (int j = 0; j < blocks[i].Count; j++) {
                    var block = blocks[i][j];
                    if (funcs.ContainsKey(block.name))
                        continue;

                    funcs.Add(block.name, block.GetType().GetMethod(block.name));
                }
            }
        }

        #region IDisposable Support
        private bool _disposed_value = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed_value) {
                if (disposing) {
                    block_img?.Dispose();
                    block_view?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed_value = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Block()
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