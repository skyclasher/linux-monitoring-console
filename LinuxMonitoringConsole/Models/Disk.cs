namespace LinuxMonitoringConsole.Models
{
	public class Disk
	{
		public string? FileSystem { get; set; }
		public string? Size { get; set; }
		public string? Used { get; set; }
		public string? Available { get; set; }
		public string? UsePercent { get; set; }
		public string? MountedOn { get; set; }
	}
}
