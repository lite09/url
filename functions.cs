using System;

public static class Functions
{
	public static string get_arch()
	{
		if (System.Environment.Is64BitOperatingSystem) return "x86-64";
		else return "x86-32";
	}
}
