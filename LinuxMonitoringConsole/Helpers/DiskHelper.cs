using LinuxMonitoringConsole.Models;
using LinuxMonitoringConsole.Models.AppSettings;
using System.Text.RegularExpressions;

namespace LinuxMonitoringConsole.Helpers
{
	public class DiskHelper
	{
		public static List<Disk> GetDisksByAppSetting(CheckDiskUsage checkDiskUsage)
		{
			List<Disk> disks = new List<Disk>();
			List<string> lines = new List<string>();
			string diskStr = ServerManager.GetDiskSpace();

			foreach (var line in diskStr.SplitToLines())
			{
				if (checkDiskUsage.FileSystems != null)
				{
					foreach (string fileSystem in checkDiskUsage.FileSystems)
					{
						if (line.Contains(fileSystem))
						{
							lines.Add(line);
							break;
						}
					}
				}
			}

			foreach (string line in lines)
			{
				Console.WriteLine(line);

				RegexOptions options = RegexOptions.None;
				Regex regex = new Regex("[ ]{1,}", options);
				string rep = regex.Replace(line, "|");
				string[] details = rep.Split('|');

				Disk disk = new Disk()
				{
					FileSystem = details[0],
					Size = details[1],
					Used = details[2],
					Available = details[3],
					UsePercent = details[4],
					MountedOn = details[5],
				};
				disks.Add(disk);
			}
			return disks;
		}
	}
}
