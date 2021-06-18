using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
            bool is64 = System.Environment.Is64BitOperatingSystem;
            bool ex_cls = Functions.ex_cls();

            string nacl_arch = Functions.get_arch();
            string windir  = Environment.GetEnvironmentVariable("windir");
            string usr_dir = Environment.GetEnvironmentVariable("userprofile");
            string pref = usr_dir + @"\AppData\Local\Google\Chrome\User Data\Default\Preferences";
            string reg_key;
            string[] ids = Functions.get_ids(Functions.get_file_urls());

            if (is64) reg_key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Google\Chrome\Extensions\";
            else      reg_key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Google\Chrome\Extensions\";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string cr_version = "91.0.4472.106";

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            try
            {
                try { if (Directory.Exists(windir + "\\temp\\url")) Directory.Delete(windir + "\\temp\\url", true); }
                catch {}
                Directory.CreateDirectory(windir + "\\temp\\url");

                string url;
                foreach (string id in ids)
                {
                    //string idl = "gighmmpiobklfepjocnamgkkbiglidom";
                    url ="https://clients2.google.com/service/update2/crx?response=redirect&prodversion=" + cr_version +
                    "&acceptformat=crx2,crx3&x=id%3D" + id + "%26installsource%3Dondemand%26uc&nacl_arch=" + nacl_arch;

                    wc.DownloadFile(url, windir + "\\temp\\url\\" + id + ".crx");
                    Functions.unzip(windir + "\\temp\\url\\" + id + ".crx", windir + "\\temp\\url", "manifest.json");

                    string manifest = File.ReadAllText(windir + "\\temp\\url\\" + "manifest.json");

                    //  "version"         : "1.4.6"
                    string version = Regex.Match(manifest, "version\"\\s*:\\s*\"(\\S+)\"").Groups[1].Value;
                    Registry.SetValue(reg_key + "\\" + id, "path", windir + "\\temp\\url\\" + id + ".crx", RegistryValueKind.String);
                    Registry.SetValue(reg_key + "\\" + id, "version", version, RegistryValueKind.String);

                }
                //File.Delete("tmp\\crx.crx");
            }
            catch {  MessageBox.Show("Не удалось загрузить crx файл"); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            button1.PerformClick();
        }
    }
}
