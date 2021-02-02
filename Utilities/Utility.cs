using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Xml;

namespace Artco
{
    internal static class Utility
    {
        public static void ShowContextMenu(object sender, int x, int y, string[] titles, List<Action<object, EventArgs>> actions)
        {
            ContextMenu context_menu = new ContextMenu();
            for (int i = 0; i < titles.Length; i++) {
                var menu_item = new MenuItem() { Text = titles[i] };
                var d = actions[i];
                menu_item.Click += (sender, e) => d.Invoke(sender, e);
                context_menu.MenuItems.Add(menu_item);
            }

            context_menu.Show(sender as Control, new Point(x, y));
        }

        public static MultiMap<string> ReadXMLWithLevel(string level)
        {
            MultiMap<string> multi_map = new MultiMap<string>();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(FileManager.GetStreamFromHTTP(FileManager.http_root_dir + "backgrounds/Practice_Explain/path.xml"));

            XmlNodeList nodes = xdoc.SelectNodes("/Practice/" + level + "/Intro");
            foreach (XmlNode node in nodes)
                multi_map.Add(node.Name, node.InnerText);

            nodes = xdoc.SelectNodes("/Practice/" + level + "/Solution");
            foreach (XmlNode node in nodes)
                multi_map.Add(node.Name, node.InnerText);

            nodes = xdoc.SelectNodes("/Practice/" + level + "/Points");
            foreach (XmlNode node in nodes)
                multi_map.Add(node.Name, node.InnerText);


            return multi_map;
        }

        public static List<(string, string)> ReadSettingDirs()
        {
            List<(string, string)> dirs = new List<(string, string)>();
#if (DEMO)
            string xml_file = "./xml/setting_demo.xml";
#else
            string xml_file = "./xml/setting.xml";
#endif
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(FileManager.GetStreamFromHTTP(FileManager.http_root_dir + xml_file));

            XmlNodeList nodes = xdoc.SelectNodes("/Setting/Dirs");
            foreach (XmlNode node in nodes)
                foreach (XmlNode child in node)
                    dirs.Add((child.Name, child.InnerText));

            return dirs;
        }
    }

    internal static class FontLibrary
    {
        public static PrivateFontCollection private_font;

        static FontLibrary()
        {
            private_font = new PrivateFontCollection();
            private_font.AddFontFile("themes/chinese/CloudYuan.TTF");
        }
    }

    public class LoopStack
    {
        private readonly List<List<(int, int)>> _list = new List<List<(int, int)>>();

        public void Init(int count)
        {
            _list.Clear();
            for (int i = 0; i < count; i++)
                _list.Add(new List<(int, int)>());
        }

        public void Push(int line_number, int pc, int value)
        {
            _list[line_number].Add((pc, value));
        }

        public int Pop(int line_number)
        {
            int list_count = _list[line_number].Count;
            int last_idx = list_count - 1;
            int loop_cnt = _list[line_number][last_idx].Item2 - 1;
            int saved_pc = _list[line_number][last_idx].Item1;

            if (loop_cnt == 0) {
                _list[line_number].RemoveAt(last_idx);
                return 0;
            }

            _list[line_number][last_idx] = (saved_pc, loop_cnt);
            return saved_pc;
        }
    }

    public class MultiMap<V>
    {
        private readonly Dictionary<string, List<V>> _dictionary = new Dictionary<string, List<V>>();

        public void Add(string key, V value)
        {
            // Add a key.
            List<V> list;
            if (this._dictionary.TryGetValue(key, out list)) {
                list.Add(value);
            } else {
                list = new List<V>();
                list.Add(value);
                this._dictionary[key] = list;
            }
        }

        public IEnumerable<string> keys {
            get {
                // Get all keys.
                return this._dictionary.Keys;
            }
        }

        public List<V> this[string key] {
            get {
                // Get list at a key.
                if (!this._dictionary.TryGetValue(key, out List<V> list)) {
                    list = new List<V>();
                    this._dictionary[key] = list;
                }
                return list;
            }
        }
    }
}