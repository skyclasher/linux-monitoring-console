using System.Text.Json.Serialization;

namespace LinuxMonitoringConsole.Models.Discord
{
	public class EmbedFooter
	{
		[JsonPropertyName("text")]
		public string? Text { get; set; }
	}
}
