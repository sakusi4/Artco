using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;

namespace Artco
{
    internal class ArtcoProject
    {
        public bool SaveProject(string path)
        {
            if (File.Exists(path)) {
                using MsgBoxForm msg_box = new MsgBoxForm("该文件已存在, 是否覆盖?", true);
                msg_box.ShowDialog();
                if (msg_box.DialogResult == System.Windows.Forms.DialogResult.No)
                    return false;
            }

            int header_size = 0;
            var cur_back = MainForm.stage_player.GetBackground();
            if (cur_back == null) {
                new MsgBoxForm("you have to choose a background").ShowDialog();
                return false;
            }

            string background_name = MainForm.stage_player.GetBackground().name + "\n";
            string variable_count = UserVariableManager.GetSize().ToString() + "\n";
            string sprite_count = ActivatedSpriteController.sprite_list.Count.ToString() + "\n";

            try {
                List<List<byte[]>> bytes = new List<List<byte[]>>();

                using (StreamWriter wr = new StreamWriter(path)) {
                    wr.Write(background_name);
                    wr.Write(variable_count);
                    wr.Write(sprite_count);
                }

                string header = string.Empty;
                foreach (var key in UserVariableManager.user_variables.Keys) {
                    string val = UserVariableManager.user_variables[key].GetValue().ToString();
                    header += key + ":" + val + "\n";
                }

                for (int i = 0; i < ActivatedSpriteController.sprite_list.Count; i++) {
                    var sprite = ActivatedSpriteController.sprite_list[i];
                    string name = sprite.name;

                    header += name + "\n";
                    header += sprite.x.ToString() + ":" + sprite.y.ToString() + "\n";

                    var codes = sprite.code_list;
                    int code_count = 0;
                    for (int j = 0; j < codes.Count; j++)
                        code_count += codes[j].Count;

                    header += code_count.ToString() + "\n";

                    for (int j = 0; j < codes.Count; j++) {
                        for (int k = 0; k < codes[j].Count; k++) {
                            var code = codes[j][k];
                            if (code.block_view.controls != null) {
                                string values = code.name;
                                for (int l = 0; l < code.block_view.controls.Count; l++)
                                    values += ":" + code.block_view.controls[l].Text;

                                values += "\n";
                                header += values;
                            } else {
                                header += code.name + "\n";
                            }
                        }
                    }

                    int sprite_img_cnt = sprite.org_img_list.Count;
                    header += sprite_img_cnt.ToString() + "\n";

                    bytes.Add(new List<byte[]>());
                    for (int j = 0; j < sprite_img_cnt; j++) {
                        using (MemoryStream ms = new MemoryStream()) {
                            sprite.org_img_list[j].Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            bytes[i].Add(ms.ToArray());
                        }

                        header += bytes[i][j].Length.ToString() + "\n";
                    }
                }

                header_size += Encoding.UTF8.GetBytes(header).Length;
                header_size += Encoding.UTF8.GetBytes(background_name).Length;
                header_size += Encoding.UTF8.GetBytes(variable_count).Length;
                header_size += Encoding.UTF8.GetBytes(sprite_count).Length;
                using (StreamWriter wr = new StreamWriter(path, true)) {
                    wr.Write(header);
                    wr.Write(header_size.ToString() + "\n");
                }

                for (int i = 0; i < bytes.Count; i++) {
                    for (int j = 0; j < bytes[i].Count; j++) {
                        using FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
                        file.Write(bytes[i][j], 0, bytes[i][j].Length);
                    }
                }
            } catch (Exception) {
                return false;
            }

            return true;
        }

        public bool LoadProject(string path)
        {
            try {
                string header_size;
                List<ArtcoObject> objects = new List<ArtcoObject>();
                List<List<string>> values = new List<List<string>>();

                using (StreamReader rdr = new StreamReader(path)) {
                    MainForm.select_back_cb?.Invoke(Background.GetNameToBack(rdr.ReadLine()));
                    int variable_count = int.Parse(rdr.ReadLine());
                    int sprite_count = int.Parse(rdr.ReadLine());

                    for (int i = 0; i < variable_count; i++) {
                        string[] splits = rdr.ReadLine().Split(':');
                        UserVariableManager.AddVariable(splits[0], double.Parse(splits[1]));
                    }

                    for (int i = 0; i < sprite_count; i++) {
                        ArtcoObject artco_object = new ArtcoObject();
                        artco_object.name = rdr.ReadLine();

                        string[] splits = rdr.ReadLine().Split(':');
                        artco_object.x = int.Parse(splits[0]);
                        artco_object.y = int.Parse(splits[1]);

                        values.Add(new List<string>());
                        int code_cnt = int.Parse(rdr.ReadLine());
                        for (int j = 0; j < code_cnt; j++) {
                            string code_name = rdr.ReadLine();
                            var split = code_name.Split(':');

                            Block code;
                            if (split.Length > 1) {
                                code = new Block(Block.GetBlockByName(split[0]));
                                string value = split[1];
                                for (int k = 2; k < split.Length; k++)
                                    value += ":" + split[k];

                                values[i].Add(value);
                            } else {
                                code = new Block(Block.GetBlockByName(code_name));
                                values[i].Add(null);
                            }
                            artco_object.blocks.Add(code);
                        }

                        int sprite_cnt = int.Parse(rdr.ReadLine());
                        for (int j = 0; j < sprite_cnt; j++) {
                            artco_object.img_sizes.Add(int.Parse(rdr.ReadLine()));
                        }

                        objects.Add(artco_object);
                    }

                    header_size = rdr.ReadLine();
                }

                int header_length = header_size.Length + 1;
                int start_point = int.Parse(header_size) + header_length;

                for (int i = 0; i < objects.Count; i++) {
                    for (int j = 0; j < objects[i].img_sizes.Count; j++) {
                        int img_size = objects[i].img_sizes[j];

                        using FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                        file.Seek(start_point, SeekOrigin.Begin);
                        byte[] bytes = new byte[img_size];
                        int read_size = file.Read(bytes, 0, img_size);
                        objects[i].images.Add(ImageUtility.ByteArrayToImage(bytes) as Bitmap);
                        start_point += read_size;
                    }

                    Sprite sprite = new Sprite(objects[i].name, null, false, null);
                    sprite.SetTmpImgList(objects[i].images);

                    MainForm.select_sprite_cb?.Invoke(sprite);

                    ActivatedSpriteController.cur_sprite.x = objects[i].x;
                    ActivatedSpriteController.cur_sprite.y = objects[i].y;

                    for (int j = 0; j < objects[i].blocks.Count; j++) {
                        ActivatedSpriteController.cur_sprite.code_editor.AddCode(objects[i].blocks[j]);
                        if (values[i][j] != null) {
                            string[] value = values[i][j].Split(':');
                            if (value.Length > 1) {
                                for (int k = 0; k < objects[i].blocks[j].block_view.controls.Count; k++)
                                    objects[i].blocks[j].block_view.controls[k].Text = value[k];
                            } else {
                                objects[i].blocks[j].block_view.controls[0].Text = value[0];
                            }
                        }
                    }
                }
            } catch (Exception e) {
                Debug.Print(e.Message);
                return false;
            }

            return true;
        }
    }
}