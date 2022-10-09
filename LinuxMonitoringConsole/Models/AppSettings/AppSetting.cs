namespace LinuxMonitoringConsole.Models.AppSettings
{
	public class AppSetting
	{
		public string? ServerName { get; set; }
		public CheckDiskUsage? CheckDiskUsage { get; set; }
		public CheckAllDisk? CheckAllDisk { get; set; }
		public EmailSetting? EmailSettings { get; set; }
		public DiscordSetting? DiscordSettings { get; set; }
	}
}
