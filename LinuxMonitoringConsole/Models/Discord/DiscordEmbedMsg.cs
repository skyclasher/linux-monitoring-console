using System.Text.Json.Serialization;

namespace LinuxMonitoringConsole.Models.Discord
{
	public class DiscordEmbedMsg
	{
		[JsonPropertyName("username")]
		public string? Username { get; set; }

		[JsonPropertyName("avatar_url")]
		public string? AvatarUrl { get; set; }

		[JsonPropertyName("embeds")]
		public List<Embed>? Embeds { get; set; }
	}
}
