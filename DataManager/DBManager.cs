using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace Artco
{
    internal static class DBManager
    {
        public static string php_root_dir { get; set; } = "http://" + FileManager.server_addr + "/";

        public static bool LoadDatas(Action<string> update_rich_textbox, string script_name, Action<string[]> action)
        {
            update_rich_textbox?.Invoke("Loading " + script_name);
            string url = php_root_dir + script_name;
            string result = string.Empty;

            try {
                using WebClient client = FileManager.GetHttpClient();
                byte[] bytes = client.DownloadData(url);
                result = Encoding.UTF8.GetString(bytes);
            } catch (WebException e) {
                update_rich_textbox?.Invoke("[Failed] : " + e.Message + "\n");
                return false;
            }

            string[] datas = result.Split('\n');
            action?.Invoke(datas);

            update_rich_textbox?.Invoke("[OK]\n");
            return true;
        }

        public static bool LoginCheck(string id, string passwd)
        {
            string url = php_root_dir + "LoginCheck.php";
            string result;
            using (WebClient client = new WebClient()) {
                NameValueCollection post_data = new NameValueCollection(){
                                    { "id", id },  //order: {"parameter name", "parameter value"}
                                    { "passwd", passwd }
                                };
                try {
                    result = Encoding.UTF8.GetString(client.UploadValues(url, post_data));
                } catch (Exception) {
                    return false;
                }
            }
            return result.Equals("true");
        }

        public static bool SignUp(UserInfo user_info)
        {
            string url = php_root_dir + "SignUp.php";
            string result;
            using (WebClient client = new WebClient()) {
                NameValueCollection post_data = new NameValueCollection(){
                                        { "id", user_info.id },
                                        { "passwd", user_info.passwd },
                                        { "license", user_info.license },
                                        { "name", user_info.name },
                                        { "birth", user_info.birth.ToString("yyyy-MM-dd") },
                                        { "email", user_info.email }
                                    };
                try {
                    result = Encoding.UTF8.GetString(client.UploadValues(url, post_data));
                } catch (Exception) {
                    return false;
                }
            }
            if (result.Equals("true")) {
                return true;
            } else if (result.Contains("duplicated_ID")) {
                throw new Exception("用户名已存在");
            } else if (result.Contains("duplicated_LIC")) {
                throw new Exception("电脑已被注册");
            } else {
                return false;
            }
        }
    }
}