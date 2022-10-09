using System.Diagnostics;

namespace LinuxMonitoringConsole.Helpers
{
	public static class ServerManager
	{
		public static string GetDiskSpace()
		{
			return string.Join(" ", "df -Ph").Bash();
		}

		private static string Bash(this string cmd)
		{
			var escapedArgs = cmd.Replace("\"", "\\\"");

			var process = new Process()
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "/bin/bash",
					Arguments = $"-c \"{escapedArgs}\"",
					RedirectStandardOutput = true,
					UseShellExecute = false,
					CreateNoWindow = true,
				}
			};
			process.Start();
			string result = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			return result;
		}
	}
}
