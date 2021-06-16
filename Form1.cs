using System;
using System.IO;
using System.Net;
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
            string f_urls = Functions.get_file_urls();
            string[] ids = Functions.get_ids(Functions.get_file_urls());
            //string request = https://clients2.google.com/service/update2/crx?response=redirect&prodversion=${version}&acceptformat=crx2,crx3&x=id%3D${currentEXTId}%26uc&nacl_arch=${nacl_arch}

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //url = get_url_in_file(xml);

            string cr_version = "91.0.4472.106";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            try
            {
                string url;
                foreach (string id in ids)
                {
                    url ="https://clients2.google.com/service/update2/crx?response=redirect&prodversion=" + cr_version +
                    "&acceptformat=crx2,crx3&x=id%3D" + id + "%26uc&nacl_arch=" + nacl_arch;
                    wc.DownloadFile(url, "crx");

                }
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить crs файл");


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //button1.PerformClick();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}
