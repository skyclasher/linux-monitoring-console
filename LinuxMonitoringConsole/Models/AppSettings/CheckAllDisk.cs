namespace LinuxMonitoringConsole.Models.AppSettings
{
	public class CheckAllDisk
	{
		public bool Enabled { get; set; } = false;
		public bool NotifyEmail { get; set; } = false;
		public bool NotifyDiscord { get; set; } = false;
		public List<int>? NotifyOnSpecificDate { get; set; }
	}
}
