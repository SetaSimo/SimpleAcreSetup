using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleAcreSetup
{
	class Program
	{
		private static readonly List<string> _defaultFolders = new() {
			Path.Combine("Program Files (x86)","Steam"),
			"SteamLibrary"
		};

		static void Main(string[] args)
		{
			var installedHardDrives = DriveInfo.GetDrives();
			var armaDirectory = string.Empty;

			foreach (var drive in installedHardDrives)
			{
				foreach (var folder in _defaultFolders)
				{
					try
					{
						armaDirectory = Directory.GetFiles(Path.Combine(drive.Name, folder), "arma3.exe", SearchOption.AllDirectories).FirstOrDefault();
						if (!string.IsNullOrEmpty(armaDirectory))
							break;
					}
					catch (DirectoryNotFoundException)
					{
					}
				}

			}

			armaDirectory = Path.GetDirectoryName(armaDirectory);
			var directory_name = Path.Combine(armaDirectory, "!Workshop", "@ACRE2", "plugin");

			string acre_file1 = Path.Combine(directory_name, "acre2_win32.dll");
			string acre_file2 = Path.Combine(directory_name, "acre2_win64.dll");

			File.Copy(acre_file1, Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "AppData", "Roaming", "TS3Client", "plugins", "acre2_win32.dll"), true);
			File.Copy(acre_file2, Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "AppData", "Roaming", "TS3Client", "plugins", "acre2_win64.dll"), true);

			Console.WriteLine("Plugin installed");
		}
	}
}