using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace Artco
{
    public static class FileManager
    {
        private static readonly string _id = "sangsang";
        private static readonly string _pwd = "sangsang1024";
        private static readonly Dictionary<string, string> _remote_dirs = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _local_dirs = new Dictionary<string, string>();
        private static readonly MultiMap<string> _requeset_files = new MultiMap<string>();

#if (TEST)
        public static string server_addr { get; } = "192.168.56.128";
#else
        public static string server_addr { get; } = "182.151.21.32";
#endif
        public static string http_root_dir { get; } = "http://" + server_addr + "/artco/";
        public static string ftp_root_dir { get; } = "ftp://" + server_addr + "/artco/";

        public delegate void DSafeUpdateRichTextbox(string message);

        public static List<string> SetDirectories(List<(string, string)> dirs)
        {
            List<string> check_list = new List<string>();
            foreach (var dir in dirs) {
                string remote_dir = http_root_dir + dir.Item2;
                string local_dir = "./" + dir.Item2;
#if (FREE)
                local_dir = local_dir.Replace("resource/free/", "");
#else
                local_dir = local_dir.Replace("resource/pay/", "");
#endif
                _remote_dirs[dir.Item1] = remote_dir;
                _local_dirs[dir.Item1] = local_dir;
                check_list.Add(dir.Item2);
            }

            return check_list;
        }

        public static bool CheckResourceByPath(DSafeUpdateRichTextbox update_rich_textbox, string path,
                MultiMap<(string, long)> remote_files, string key, WebClient web_client = null)
        {
            string result = string.Empty;
            string url = DBManager.php_root_dir + "CheckRsourcesByPath.php";

            try {
                if (web_client != null) {
                    NameValueCollection post_data = new NameValueCollection() { { "path", path }, };
                    result = Encoding.UTF8.GetString(web_client.UploadValues(url, post_data));
                } else {
                    using WebClient client = GetHttpClient();
                    NameValueCollection post_data = new NameValueCollection() {
                                                        { "path", path },  //order: {"parameter name", "parameter value"}
                                                };

                    result = Encoding.UTF8.GetString(client.UploadValues(url, post_data));
                }
            } catch (WebException e) {
                update_rich_textbox?.Invoke("Failed CheckResourceByPath Function, " + e.Message + "\n");
                return false;
            }

            string[] datas = result.Split('\n');
            for (int i = 0; i < datas.Length - 2; i++) {
                string[] splits = datas[i].Split(':');
                string name = splits[0];
                long size = long.Parse(splits[1]);
                remote_files[key].Add((name, size));
            }

            return true;
        }

        public static bool CheckChangedResources(DSafeUpdateRichTextbox update_rich_textbox, List<string> check_list)
        {
            MultiMap<(string, long)> remote_files = new MultiMap<(string, long)>();
            MultiMap<(string, long, bool)> local_files = new MultiMap<(string, long, bool)>();

            int cnt = 0;
            WebClient web_client = GetHttpClient();
            foreach (var key in _remote_dirs.Keys) {
                update_rich_textbox?.Invoke("Start checking " + key);
                if (!CheckResourceByPath(update_rich_textbox, check_list[cnt], remote_files, key, web_client)) {
                    update_rich_textbox?.Invoke(" [Failed]\n");
                    Thread.Sleep(3000);

                    Process.GetCurrentProcess().Kill();
                }

                update_rich_textbox?.Invoke(" [Complete]\n");
                cnt++;
            }

            web_client.Dispose();

            foreach (var key in _local_dirs.Keys) {
                DirectoryInfo di = new DirectoryInfo(_local_dirs[key]);
                if (!di.Exists)
                    di.Create();

                foreach (FileInfo file in di.GetFiles()) {
                    local_files[key].Add((file.Name, file.Length, false));
                }
            }

            bool is_update = false;
            foreach (var key in remote_files.keys) {
                foreach (var remote_file in remote_files[key]) {
                    bool is_download = true;
                    for (int i = 0; i < local_files[key].Count; i++) {
                        if (remote_file.Item1.Equals(local_files[key][i].Item1)) {
                            // 한 번이라도 검색 된 파일은 삭제할 필요가 없음
                            local_files[key][i] = (local_files[key][i].Item1, local_files[key][i].Item2, true);

                            if (remote_file.Item2 == local_files[key][i].Item2) {
                                is_download = false;
                                break;
                            } else {
                                is_download = true;
                                break;
                            }
                        }
                    }
                    if (is_download) {
                        _requeset_files[key].Add(remote_file.Item1);
                        is_update = true;
                    }
                }
            }

            foreach (var key in local_files.keys) {
                foreach (var file in local_files[key]) {
                    if (!file.Item3) {
                        string target = _local_dirs[key] + file.Item1;
                        File.Delete(target);
                        update_rich_textbox?.Invoke(target + "......Delete\n");
                    }
                }
            }

            return is_update;
        }

        public static void DownloadChangedResources(DSafeUpdateRichTextbox update_rich_textbox)
        {
            List<string> local_paths = new List<string>();
            List<string> remote_paths = new List<string>();

            foreach (var key in _requeset_files.keys) {
                foreach (var file_name in _requeset_files[key]) {
                    local_paths.Add(_local_dirs[key] + file_name);
                    remote_paths.Add(_remote_dirs[key] + file_name);
                }
            }

            DownloadFileListFromHTTP(update_rich_textbox, local_paths, remote_paths);
        }
        public static void DownloadExecutableFile(DSafeUpdateRichTextbox update_rich_textbox)
        {
            RemoveOldVersion();

            File.Move("./Artco.exe", "./tmp");
#if (FREE)
            string server_exe_path = http_root_dir + "/bin/free/Artco.exe";
#else
            string server_exe_path = http_root_dir + "/bin/pay/Artco.exe";
#endif
            if (!DownloadFileFromHTTP(server_exe_path, "./Artco.exe")) {
                update_rich_textbox?.Invoke("Failed DownloadExecutableFile Function\n");

                if (File.Exists("./Artco.exe"))
                    File.Delete("./Artco.exe");

                File.Move("./tmp", "./Artco.exe");

                update_rich_textbox?.Invoke("Executable Recovery Completedn\n");
                Thread.Sleep(2000);
                Process.GetCurrentProcess().Kill();
            }
        }

        public static string GetExecutableVersion()
        {
            string local_version_path = "version.txt";
            if (!File.Exists(local_version_path))
                File.WriteAllText(local_version_path, "0.0");

            double local_ver = double.Parse(File.ReadAllText(local_version_path));
            double remote_ver = double.Parse(new StreamReader(GetStreamFromHTTP(http_root_dir + "version.txt")).ReadLine());
            return (local_ver < remote_ver) ? remote_ver.ToString() : null;
        }

        public static void DownloadCustomControlDll(DSafeUpdateRichTextbox update_rich_textbox)
        {
            if (!DownloadFileFromHTTP(http_root_dir + "ArtcoCustomControl.dll", "./ArtcoCustomControl.dll")) {
                update_rich_textbox?.Invoke("Failed DownloadCustomControlDll Function\n");

                Thread.Sleep(2000);
                Process.GetCurrentProcess().Kill();
            }
        }

        public static void UpdateVersionInfo(string new_ver)
        {
            File.WriteAllText("version.txt", new_ver);
        }

        public static bool DownloadFileFromHTTP(string src_path, string dst_path)
        {
            try {
                using (WebClient web_client = GetHttpClient()) {
                    web_client.DownloadFile(src_path, dst_path);
                }

                return true;
            } catch (Exception) {
                return false;
            }
        }

        public static void DownloadFileListFromHTTP(DSafeUpdateRichTextbox update_rich_textbox, List<string> local_paths, List<string> remote_paths)
        {
            try {
                using WebClient web_client = GetHttpClient();
                for (int i = 0; i < remote_paths.Count; i++) {
                    web_client.DownloadFile(remote_paths[i], local_paths[i]);
                    update_rich_textbox?.Invoke(local_paths[i] + " ...Download Succeeded\n");
                }
            } catch (Exception e) {
                update_rich_textbox?.Invoke("Resources downloa failed : " + e.Message + "\n");
            }
        }

        //public static void UploadSaveFile(string filePath, string fileName)
        //{
        //    string path = _ftpRootDir + " saves/" + Setting._userName + "/" + fileName;
        //    Stream fileStream = File.OpenRead(filePath);

        //    FtpWebRequest request = GetFtpRequest(path);
        //    request.Method = WebRequestMethods.Ftp.UploadFile;

        //    int length = 4096;
        //    byte[] buffer = new byte[length];

        //    using (var uploadStream = request.GetRequestStream())
        //    {
        //        int bytesRead = fileStream.Read(buffer, 0, length);

        //        while (bytesRead != 0)
        //        {
        //            uploadStream.Write(buffer, 0, bytesRead);
        //            bytesRead = fileStream.Read(buffer, 0, length);
        //        }
        //    }

        //    request.Abort();
        //}

        public static void DeleteFileFromFTP(string path)
        {
            string remote_path = "ftp" + path.Substring(4);
            try {
                FtpWebRequest request = GetFtpRequest(remote_path);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.GetResponse();
            } catch (WebException e) {
                new MsgBoxForm("Failed Delete : " + e.Message).ShowDialog();
            }
        }

        public static void MakeFTPDirectory(string user_name)
        {
            string path = ftp_root_dir + "sprites/" + user_name;
            try {
                FtpWebRequest request = GetFtpRequest(path);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.GetResponse();
            } catch (InvalidOperationException e) {
                Debug.Print(e.Message);
            }
        }

        public static string[] GetFtpFolderItems(string ftp_url)
        {
            try {
                FtpWebRequest request = GetFtpRequest(ftp_url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream response_stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(response_stream);

                return reader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            } catch (Exception) {
                return new string[] { };
            }
        }

        public static string[] GetLocalFolderItems(string path)
        {
            try {
                List<string> file_name = new List<string>();
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                foreach (var item in di.GetFiles()) {
                    file_name.Add(item.Name);
                }
                return file_name.ToArray();
            } catch (DirectoryNotFoundException) {
                return new string[] { };
            }
        }

        public static Stream GetStreamFromHTTP(string path)
        {
            string remote_path = path;
            var client = GetHttpClient();
            return client.OpenRead(remote_path);
        }

        public static WebClient GetHttpClient()
        {
            // 인증 추가가 필요한 경우, 여기에 추가
            return new WebClient();
        }

        private static FtpWebRequest GetFtpRequest(string path)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Credentials = new NetworkCredential(_id, _pwd);
            return request;
        }

        public static void RemoveOldVersion()
        {
            string temp = @".\tmp";
            if (File.Exists(temp)) {
                try {
                    File.Delete(temp);
                } catch (Exception) { }
            }
        }
    }
}