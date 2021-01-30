using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;

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

            var cur_back = MainForm.stage_player.GetBackground();
            if (cur_back == null) {
                new MsgBoxForm("you have to choose a background").ShowDialog();
                return false;
            }

            try {
                File.WriteAllText(path, JsonSerializeProject());

                string old_file_path = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + ".ArtcoProject");
                if (File.Exists(old_file_path)) {
                    File.Delete(old_file_path);
                }

            } catch (Exception) {
                return false;
            }
            return true;
        }

        public bool LoadProject(string path)
        {
            if (Path.GetExtension(path) == ".artcoproj") {
                return JsonDeserializeProject(File.ReadAllText(path));
            } else {
                return ByteDeserializeProject(path);
            }
        }

        private string JsonSerializeProject()
        {
            JsonArtcoProject artco_proj = new JsonArtcoProject();

            artco_proj.background_path = MainForm.stage_player.GetBackground().background_path;

            if (Music.cur_music != null) {
                artco_proj.bgm_path = Music.cur_music.SoundLocation;
            }

            artco_proj.user_variables = new List<Tuple<string, string>>();
            foreach (var key in UserVariableManager.user_variables.Keys) {
                Tuple<string, string> user_var = new Tuple<string, string>(key, UserVariableManager.user_variables[key].GetValue().ToString());
                artco_proj.user_variables.Add(user_var);
            }

            artco_proj.serialized_sprites = new List<string>();
            for (int i = 0; i < ActivatedSpriteController.sprite_list.Count; i++) {
                string serialized_sprite = new ArtcoObject().JsonSerializeObject(ActivatedSpriteController.sprite_list[i]);
                artco_proj.serialized_sprites.Add(serialized_sprite);
            }

            return JsonSerializer.Serialize(artco_proj);
        }

        private bool JsonDeserializeProject(string serialized_proj)
        {
            try {
                JsonArtcoProject artco_proj = new JsonArtcoProject();
                artco_proj = JsonSerializer.Deserialize<JsonArtcoProject>(serialized_proj);

                MainForm.select_back_cb?.Invoke(Background.GetPathToBack(artco_proj.background_path));

                if (artco_proj.bgm_path != null) {
                    Music.SetMusic(artco_proj.bgm_path);
                }

                for (int i = 0; i < artco_proj.user_variables.Count; i++) {
                    string key = artco_proj.user_variables[i].Item1;
                    double val = double.Parse(artco_proj.user_variables[i].Item2);
                    UserVariableManager.AddVariable(key, val);
                }

                for (int i = 0; i < artco_proj.serialized_sprites.Count; i++) {
                    if (new ArtcoObject().JsonDeserializeObject(artco_proj.serialized_sprites[i]) == false) {
                        return false;
                    }
                }

            } catch (Exception) {
                return false;
            }
            return true;
        }

        private bool ByteDeserializeProject(string path)
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

    internal struct JsonArtcoProject
    {
        public string background_path { get; set; }
        public string bgm_path { get; set; }
        public List<Tuple<string, string>> user_variables { get; set; }
        public List<string> serialized_sprites { get; set; }
    }
}