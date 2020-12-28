using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Artco
{
    internal class ArtcoObject
    {
        public List<Block> blocks { get; set; } = new List<Block>();
        public List<Bitmap> images { get; set; } = new List<Bitmap>();
        public List<int> img_sizes { get; set; } = new List<int>();
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public bool SaveObject(ActivatedSprite sprite, string name)
        {
            var codes = sprite.code_list;
            string path = Setting.save_path + "/" + name + ".ArtcoObject";

            if (File.Exists(path)) {
                using MsgBoxForm msg_box = new MsgBoxForm("该文件已存在, 是否覆盖?", true);
                msg_box.ShowDialog();
                if (msg_box.DialogResult == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            string header = sprite.name + "\n";
            header += sprite.x.ToString() + ":" + sprite.y.ToString() + "\n";

            int code_count = 0;
            for (int i = 0; i < codes.Count; i++)
                code_count += codes[i].Count;

            header += code_count.ToString() + "\n";

            for (int i = 0; i < codes.Count; i++) {
                for (int j = 0; j < codes[i].Count; j++) {
                    var code = codes[i][j];
                    if (code.block_view.controls != null) {
                        string values = code.name;
                        for (int k = 0; k < code.block_view.controls.Count; k++)
                            values += ":" + code.block_view.controls[k].Text;

                        values += "\n";
                        header += values;
                    } else {
                        header += code.name + "\n";
                    }
                }
            }

            int sprite_img_cnt = sprite.org_img_list.Count;
            header += sprite_img_cnt.ToString() + "\n";

            List<byte[]> bytes = new List<byte[]>();

            for (int i = 0; i < sprite_img_cnt; i++) {
                using (MemoryStream ms = new MemoryStream()) {
                    sprite.org_img_list[i].Save(ms, ImageFormat.Png);
                    bytes.Add(ms.ToArray());
                }

                header += bytes[i].Length.ToString() + "\n";
            }

            int header_size = Encoding.UTF8.GetBytes(header).Length;

            try {
                using (StreamWriter wr = new StreamWriter(path)) {
                    wr.Write(header);
                    wr.Write(header_size.ToString() + "\n");
                }

                for (int i = 0; i < sprite_img_cnt; i++) {
                    using FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
                    file.Write(bytes[i], 0, bytes[i].Length);
                }
            } catch (Exception) {
                return false;
            }

            return true;
        }

        public bool LoadObject(string path)
        {
            try {
                using StreamReader rdr = new StreamReader(path);

                string name = rdr.ReadLine();
                string[] splits = rdr.ReadLine().Split(':');
                int x = int.Parse(splits[0]);
                int y = int.Parse(splits[1]);

                List<Block> codes = new List<Block>();
                List<string> values = new List<string>();

                int code_cnt = int.Parse(rdr.ReadLine());
                for (int i = 0; i < code_cnt; i++) {
                    string code_name = rdr.ReadLine();
                    var split = code_name.Split(':');

                    Block code;
                    if (split.Length > 1) {
                        code = new Block(Block.GetBlockByName(split[0]));
                        string value = split[1];
                        for (int j = 2; j < split.Length; j++)
                            value += ":" + split[j];

                        values.Add(value);
                    } else {
                        code = new Block(Block.GetBlockByName(split[0]));
                        values.Add(null);
                    }

                    codes.Add(code);
                }

                int sprite_cnt = int.Parse(rdr.ReadLine());
                List<int> sprite_sizes = new List<int>();
                for (int i = 0; i < sprite_cnt; i++) {
                    sprite_sizes.Add(int.Parse(rdr.ReadLine()));
                }

                string header = rdr.ReadLine();
                int header_length = header.Length + 1;
                int start_point = int.Parse(header) + header_length;

                List<Bitmap> bmp_list = new List<Bitmap>();
                for (int i = 0; i < sprite_cnt; i++) {
                    using FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                    file.Seek(start_point, SeekOrigin.Begin);
                    byte[] bytes = new byte[sprite_sizes[i]];
                    int read_size = file.Read(bytes, 0, sprite_sizes[i]);
                    bmp_list.Add(ImageUtility.ByteArrayToImage(bytes) as Bitmap);
                    start_point += read_size;
                }

                Sprite sprite = new Sprite(name, null, false, null);
                sprite.SetTmpImgList(bmp_list);

                MainForm.select_sprite_cb?.Invoke(sprite);

                ActivatedSpriteController.cur_sprite.x = x;
                ActivatedSpriteController.cur_sprite.y = y;

                for (int i = 0; i < codes.Count; i++) {
                    ActivatedSpriteController.cur_sprite.code_editor.AddCode(codes[i]);
                    if (values[i] != null) {
                        string[] value = values[i].Split(':');
                        if (value.Length > 1) {
                            for (int j = 0; j < codes[i].block_view.controls.Count; j++)
                                codes[i].block_view.controls[j].Text = value[j];

                            UserVariableManager.AddVariable(value[0], double.Parse(value[1]));
                        } else {
                            codes[i].block_view.controls[0].Text = value[0];
                        }
                    }
                }
            } catch (Exception) {
                return false;
            }

            return true;
        }
    }
}