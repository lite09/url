using OpenQA.Selenium.Chrome;
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
            // &x=id%3D${currentEXTId}%26uc
            // &x=id%3D${currentEXTId}%26installsource%3Dondemand%26uc
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //url = get_url_in_file(xml);
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

                    wc.DownloadFile(url, "tmp\\crx.zip");


                }
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить crx файл");


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ZipFile.ExtractToDirectory("tmp\\crx.zip", "tmp\\" + "id");
            ZipFile.ExtractToDirectory("tmp\\crx.zip", "tmp\\" + "id");
            //button1.PerformClick();
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
