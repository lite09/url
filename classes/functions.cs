
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

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
			string[] urls = Regex.Matches(url_txt, ".*$").Cast<Match>().Select(l => l.Value).ToArray();


		}
        catch { return null; }
    }

	public static string get_file_urls()
    {
		try
        {
			string conf = File.ReadAllText("config.txt");
			string file_urls = Regex.Match(conf, "");
        }
        catch { return null; }

		return "";
    }
}
