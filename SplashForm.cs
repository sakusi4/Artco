using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artco
{
    public partial class SplashForm : Form
    {
        private bool _is_details;
        private StagePlayer _stage_player;

        public SplashForm()
        {
            InitializeComponent();
        }

        private async void SplashForm_Load(object sender, EventArgs e)
        {
            Cursor = new Cursor(Properties.Resources.Cursor.GetHicon());

            string remote_path = "./themes/" + Setting.language + "/SplashVideo.mp4";

            _stage_player = new StagePlayer(SafeDrawBackground, null);
            _stage_player.SetBackground(new Background(null, -1, 0, remote_path, null, false));
            _stage_player.Start();

            await Task.Run(LoadData);
            CloseForm();
        }

        public void SafeDrawBackground(Image image)
        {
            if (pbx_Loading.InvokeRequired) {
                var d = new Action<Image>(SafeDrawBackground);
                pbx_Loading.BeginInvoke(d, image);
            } else {
                pbx_Loading.Image?.Dispose();
                pbx_Loading.Image = image;
            }
        }

        public void CloseForm()
        {
            _stage_player.Stop();
            Close();
        }

        public void LoadData()
        {
            bool ret1 = DBManager.LoadDatas(SafeUpdateRichTextbox, "SelectBlockTable.php", Block.AddBlockData);
            bool ret2 = DBManager.LoadDatas(SafeUpdateRichTextbox, "SelectSpriteTable.php", Sprite.AddSpriteData);
            bool ret3 = DBManager.LoadDatas(SafeUpdateRichTextbox, "SelectEduBackTable.php", Background.AddEduBackgroundData);
            bool ret4 = DBManager.LoadDatas(SafeUpdateRichTextbox, "SelectBackTable.php", Background.AddBackgroundData);
            bool ret5 = DBManager.LoadDatas(SafeUpdateRichTextbox, "SelectSoundTable.php", Sound.AddSoundData);
            bool ret6 = DBManager.LoadDatas(SafeUpdateRichTextbox, "SelectMusicTable.php", Music.AddMusicData);
            if (!ret1 || !ret2 || !ret3 || !ret4 || !ret5) {
                Thread.Sleep(3000);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        public void SafeUpdateRichTextbox(string message)
        {
            if (richbox_Log.InvokeRequired) {
                var d = new Action<string>(SafeUpdateRichTextbox);
                richbox_Log.Invoke(d, message);
            } else {
                richbox_Log.AppendText(message);
                richbox_Log.Refresh();
            }
        }

        private void Lbl_Details_Click(object sender, EventArgs e)
        {
            if (!_is_details) {
                lbl_Details.Text = "Hide details";
                Width = 1500;
            } else {
                lbl_Details.Text = "Show details";
                Width = 1280;
            }

            _is_details ^= true;
        }
    }
}