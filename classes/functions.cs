
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

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
			string url_txt = File.ReadAllText(file_urls);
			string[] urls = Regex.Matches(url_txt, "(.*)\\r\\n").Cast<Match>().Select(l => l.Value.Trim()).ToArray();

            for (int i = 0; i < urls.Length; i++)
				urls[i] = Regex.Match(urls[i], @"detail\/[^\/]+\/(\S+)[\/|\?]").Groups[1].Value;

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
}
