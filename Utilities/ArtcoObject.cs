using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.Json;

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
            string path = Setting.save_path + "/" + name + ".artcoobj";

            if (File.Exists(path)) {
                using MsgBoxForm msg_box = new MsgBoxForm("该文件已存在, 是否覆盖?", true);
                msg_box.ShowDialog();
                if (msg_box.DialogResult == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            try {
                File.WriteAllText(path, JsonSerializeObject(sprite));

                string old_file_path = Setting.save_path + "/" + name + ".ArtcoObject";
                if (File.Exists(old_file_path)) {
                    File.Delete(old_file_path);
                }

            } catch (Exception) {
                return false;
            }
            return true;
        }

        public bool LoadObject(string path)
        {
            if (Path.GetExtension(path) == ".artcoobj") {
                return JsonDeserializeObject(File.ReadAllText(path));
            } else {
                return ByteDeserializeObject(path);
            }
        }

        public string JsonSerializeObject(ActivatedSprite sprite)
        {
            JsonArtcoObject artco_obj = new JsonArtcoObject();

            artco_obj.name = sprite.name;
            artco_obj.point = new Tuple<int, int>(sprite.x, sprite.y);

            artco_obj.code_blocks = new List<List<Tuple<string, List<string>>>>();
            for (int i = 0; i < sprite.code_list.Count; i++) {
                artco_obj.code_blocks.Add(new List<Tuple<string, List<string>>>());
                for (int j = 0; j < sprite.code_list[i].Count; j++) {
                    var block = sprite.code_list[i][j];
                    if (block.block_view.controls != null) {
                        List<string> control = new List<string>();
                        for (int k = 0; k < block.block_view.controls.Count; k++) {
                            string value = block.block_view.controls[k].Text;
                            if (UserVariableManager.user_variables.ContainsKey(value))
                                value += ":" + UserVariableManager.user_variables[value].GetValue().ToString();
                            control.Add(value);
                        }
                        artco_obj.code_blocks[i].Add(new Tuple<string, List<string>>(block.name, control));
                    } else {
                        artco_obj.code_blocks[i].Add(new Tuple<string, List<string>>(block.name, null));
                    }
                }
            }

            artco_obj.bitmap = new List<byte[]>();
            ImageConverter converter = new ImageConverter();
            for (int i = 0; i < sprite.org_img_list.Count; i++) {
                artco_obj.bitmap.Add((byte[])converter.ConvertTo(sprite.org_img_list[i], typeof(byte[])));
            }

            return JsonSerializer.Serialize(artco_obj);
        }

        public bool JsonDeserializeObject(string serialized_obj)
        {
            try {
                JsonArtcoObject artco_obj = new JsonArtcoObject();
                artco_obj = JsonSerializer.Deserialize<JsonArtcoObject>(serialized_obj);

                List<Bitmap> bmp_list = new List<Bitmap>();
                for (int i = 0; i < artco_obj.bitmap.Count; i++) {
                    bmp_list.Add(ImageUtility.ByteArrayToImage(artco_obj.bitmap[i]) as Bitmap);
                }

                Sprite sprite = new Sprite(artco_obj.name, null, false, null);
                sprite.SetTmpImgList(bmp_list);

                MainForm.select_sprite_cb?.Invoke(sprite);

                ActivatedSpriteController.cur_sprite.x = artco_obj.point.Item1;
                ActivatedSpriteController.cur_sprite.y = artco_obj.point.Item2;

                for (int i = 0; i < artco_obj.code_blocks.Count; i++) {
                    for (int j = 0; j < artco_obj.code_blocks[i].Count; j++) {
                        string block_name = artco_obj.code_blocks[i][j].Item1;
                        List<string> block_value = artco_obj.code_blocks[i][j].Item2;

                        Block block = new Block(Block.GetBlockByName(block_name));
                        ActivatedSpriteController.cur_sprite.code_editor.AddCode(block);

                        if (block_value != null) {
                            for (int k = 0; k < block_value.Count; k++) {
                                string[] splits = block_value[k].Split(':');
                                string key = splits[0];
                                block.block_view.controls[k].Text = key;
                                if (splits.Length == 2) {
                                    object val = double.Parse(splits[1]);
                                    UserVariableManager.AddVariable(key, val);
                                }
                            }
                        }
                    }
                }

            } catch (Exception) {
                return false;
            }
            return true;
        }

        private bool ByteDeserializeObject(string path)
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

    internal struct JsonArtcoObject
    {
        public string name { get; set; }
        public Tuple<int, int> point { get; set; }
        public List<List<Tuple<string, List<string>>>> code_blocks { get; set; }
        public List<byte[]> bitmap { get; set; }
    }
}