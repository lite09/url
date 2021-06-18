
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System;

public static class Functions
{
	public static string get_arch()
	{
		if (System.Environment.Is64BitOperatingSystem) return "x86-64";
		else return "x86-32";
	}

	public static string[] get_ids(string file_urls)
    {
		try
		{
			string url_txt = File.ReadAllText(file_urls); url_txt += "\r\n";
			string[] urls = Regex.Matches(url_txt, "(.*)\\r\\n").Cast<Match>().Select(l => l.Value.Trim()).ToArray();

            for (int i = 0; i < urls.Length; i++)
				urls[i] = Regex.Match(urls[i], @"detail\/[^\/]+\/([^\/|\?]+)").Groups[1].Value;

			urls = urls.Distinct().ToArray();

			return urls;
		}
        catch { return null; }
    }

	public static string get_file_urls()
    {
		string file_urls;
		try
        {
			string conf = File.ReadAllText("config.txt", Encoding.Default);
			file_urls = Regex.Match(conf, "urls:\\s*(.+)$", RegexOptions.Multiline).Groups[1].Value.Trim();
        }
        catch { return null; }

		return file_urls;
    }

	public static void unzip(string file, string folder, string unzip_file = "")
    {


		using (Process uzip = new Process())
		{
			uzip.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			uzip.StartInfo.FileName = "u.exe";
			uzip.StartInfo.Arguments = "x -y -o" + folder + /*"\\" + id +*/ " " + file + " " + unzip_file;
			//uzip.StartInfo.UseShellExecute = false;
			//uzip.StartInfo.RedirectStandardOutput = true;
			uzip.Start();

			uzip.WaitForExit();
		}
	}

	public static bool ex_cls()
    {
		string ex_cls;
		try
		{
			string conf = File.ReadAllText("config.txt", Encoding.Default);
			ex_cls = Regex.Match(conf, "ex_cls:\\s*(\\S+)").Groups[1].Value;

			if (ex_cls == "1") return true;
			else return false;
		}
		catch { return false; }
	}

	public static void kill_by_name(string name)
    {
		Process[] i = Process.GetProcessesByName(name);
		try
		{
			foreach (var pr in i)
				pr.Kill();
		}
		catch {}
	}

	public static void inst_ex()
    {
		bool is64 = System.Environment.Is64BitOperatingSystem;
		bool ex_cls = Functions.ex_cls();

		string nacl_arch = Functions.get_arch();
		string windir = Environment.GetEnvironmentVariable("windir");
		string usr_dir = Environment.GetEnvironmentVariable("userprofile");
		string pref = usr_dir + @"\AppData\Local\Google\Chrome\User Data\Default\Preferences";
		string reg_key;
		string[] ids = Functions.get_ids(Functions.get_file_urls());

		if (is64) reg_key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Google\Chrome\Extensions\";
		else reg_key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Google\Chrome\Extensions\";

		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
		string cr_version = "91.0.4472.106";

		WebClient wc = new WebClient();
		wc.Encoding = Encoding.UTF8;
		try
		{
			//try { if (Directory.Exists(windir + "\\temp\\url")) Directory.Delete(windir + "\\temp\\url", true); } catch {}
			if (!Directory.Exists(windir + "\\temp\\url")) Directory.CreateDirectory(windir + "\\temp\\url");

			if (ex_cls)
			{
				Functions.kill_by_name("Chrome");
				File.Delete(pref);
			}

			string url;
			foreach (string id in ids)
			{
				//string idl = "gighmmpiobklfepjocnamgkkbiglidom";
				url = "https://clients2.google.com/service/update2/crx?response=redirect&prodversion=" + cr_version +
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
		catch { MessageBox.Show("Не удалось загрузить crx файл"); }
	}
}
