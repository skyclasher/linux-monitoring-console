using LinuxMonitoringConsole.Helpers;
using LinuxMonitoringConsole.Models.AppSettings;
using LinuxMonitoringConsole.Models.Database;

namespace LinuxMonitoringConsole
{
	internal class CheckAllDiskProcess
	{
		public static async Task Process(AppSetting appSetting)
		{
			if (appSetting.CheckAllDisk != null && appSetting.CheckAllDisk.Enabled)
			{
				string date = DateTime.Now.ToShortDateString();
				string body = ServerManager.GetDiskSpace();

				if (!string.IsNullOrEmpty(body))
				{
					string subject = $"Server Disk Status - {appSetting.ServerName} - {DateTime.Now.ToLongDateString()}";
					if (appSetting.CheckAllDisk.NotifyOnSpecificDate == null ||
						(appSetting.CheckAllDisk.NotifyOnSpecificDate != null && appSetting.CheckAllDisk.NotifyOnSpecificDate.Contains(DateTime.Now.Day)))
					{
						List<ProcessLog> processLogs = LiteDbHelper.GetProcessLogsByDateAndNotify(date, "Email-CheckAllDisk");

						if (appSetting.CheckAllDisk.NotifyEmail &&
							appSetting.EmailSettings != null &&
							processLogs.Count <= 0)
						{
							bool result = EmailHelper.SendEmail(appSetting.EmailSettings, body, subject);

							if (result)
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Email-CheckAllDisk",
									IsSuccess = true
								});
							}
							else
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Email-CheckAllDisk",
									IsSuccess = false
								});
							}
						}

						processLogs = LiteDbHelper.GetProcessLogsByDateAndNotify(date, "Discord-CheckAllDisk");

						if (appSetting.CheckAllDisk.NotifyDiscord &&
							appSetting.DiscordSettings != null &&
							processLogs.Count <= 0)
						{
							bool result = await DiscordWebhook.SendDiscordWebhook(appSetting.DiscordSettings, subject, string.Empty, $"```{body}```", $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}", 47871);
							if (result)
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Discord-CheckAllDisk",
									IsSuccess = true
								});
							}
							else
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Discord-CheckAllDisk",
									IsSuccess = false
								});
							}
						}
					}
				}
			}
		}
	}
}
