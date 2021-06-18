
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;

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
			file_urls = Regex.Match(conf, "urls:\\s*(\\S+)$").Groups[1].Value;
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
			ex_cls = Regex.Match(conf, "ex_cls:\\s*(\\S+)$").Groups[1].Value;

			if (ex_cls == "1") return true;
			else return false;
		}
		catch { return false; }
	}

}
