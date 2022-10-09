namespace LinuxMonitoringConsole.Models.AppSettings
{
	public class CheckDiskUsage
	{
		public bool Enabled { get; set; } = false;
		public List<string>? FileSystems { get; set; }
		public int TresholdAlert { get; set; } = 90;
		public bool NotifyEmail { get; set; } = false;
		public bool NotifyDiscord { get; set; } = false;
		public List<int>? NotifyOnSpecificDate { get; set; }
	}
}
