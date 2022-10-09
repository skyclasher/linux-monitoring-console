using LiteDB;

namespace LinuxMonitoringConsole.Models.Database
{
	public class ErrorLog
	{
		public ObjectId? Id { get; set; }
		public string? DateTime { get; set; }
		public string? Message { get; set; }
		public string? StackTrace { get; set; }
	}
}
