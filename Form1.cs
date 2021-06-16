using System;
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
            string[] ids;
            //string request = https://clients2.google.com/service/update2/crx?response=redirect&prodversion=${version}&acceptformat=crx2,crx3&x=id%3D${currentEXTId}%26uc&nacl_arch=${nacl_arch}



            /*IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://chrome.google.com/webstore/detail/line/ophjlpahpchlmihnnnihgmmeilfjmjjc?hl=ru&");
            //driver.Navigate().GoToUrl("https://chrome.google.com/webstore/detail/view-image/jpcmhcelnjdmblfmjabdeclccemkghjk?hl=ru&");
            //Assert.IsTrue(driver.Url.Contains("habr.com"), "Что-то не так =(");

            Thread.Sleep(1800);
            //var element = driver.FindElement(By.XPath(@"/html/body/div[5]/div[2]/div/div/div[2]/div[2]/div/div/div/div"));
            var element = driver.FindElement(By.XPath(@"/html/body/div[5]/div[2]/div/div/div[2]/div[2]/div/div/div/div"));
            element.Click();

            driver.Quit();*/

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //url = get_url_in_file(xml);

            //var s = System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture;
            WebClient wc = new WebClient();
            bool x64 = System.Environment.Is64BitOperatingSystem;
            wc.Encoding = Encoding.UTF8;
            try
            {
                //file_xml_data = new StringReader(wc.DownloadString(url));
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
