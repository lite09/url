using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace url
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nacl_arch = Functions.get_arch();
            //string f_urls = Functions.get_file_urls();
            string[] ids = Functions.get_ids(Functions.get_file_urls());

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string cr_version = "91.0.4472.106";

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            try
            {
                if (Directory.Exists("tmp")) Directory.Delete("tmp");
                Directory.CreateDirectory("tmp");

                string url;
                foreach (string id in ids)
                {
                    //string idl = "gighmmpiobklfepjocnamgkkbiglidom";
                    url ="https://clients2.google.com/service/update2/crx?response=redirect&prodversion=" + cr_version +
                    "&acceptformat=crx2,crx3&x=id%3D" + id + "%26installsource%3Dondemand%26uc&nacl_arch=" + nacl_arch;

                    wc.DownloadFile(url, "tmp\\crx.crx");
                    Functions.unzip("tmp\\crx.crx", "tmp", id);
                }
                File.Delete("tmp\\crx.crx");
            }
            catch {  MessageBox.Show("Не удалось загрузить crx файл"); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

            /*ChromeOptions options = new ChromeOptions();
            options.AddArguments(@"load-extension=C:/Users/и/source/repos/url/bin/Debug/tmp/crx");
            ChromeDriver driver = new ChromeDriver(options);*/

            //button1.PerformClick();
        }
    }
}
