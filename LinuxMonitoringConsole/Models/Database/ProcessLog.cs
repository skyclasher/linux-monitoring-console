using LiteDB;

namespace LinuxMonitoringConsole.Models.Database
{
	public class ProcessLog
	{
		public ObjectId? Id { get; set; }
		public string? DateRun { get; set; }
		public string? NotifyService { get; set; }
		public bool IsSuccess { get; set; } = false;
		public List<string>? Mounts { get; set; }
	}
}
