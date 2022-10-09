namespace LinuxMonitoringConsole.Models.AppSettings
{
	public class EmailSetting
	{
		public string? Host { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public int Port { get; set; }
		public string? Encryption { get; set; }
		public string? FromEmail { get; set; }
		public string? FromName { get; set; }
		public List<string>? ToEmail { get; set; }
		public List<string>? CcEmail { get; set; }
	}
}
