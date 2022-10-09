using LinuxMonitoringConsole.Helpers;
using LinuxMonitoringConsole.Models;
using LinuxMonitoringConsole.Models.AppSettings;
using LinuxMonitoringConsole.Models.Database;

namespace LinuxMonitoringConsole
{
	public static class CheckDiskUsageProcess
	{
		public static async Task Process(AppSetting appSetting)
		{
			if (appSetting.CheckDiskUsage != null && appSetting.CheckDiskUsage.Enabled)
			{
				string date = DateTime.Now.ToShortDateString();
				string body = string.Empty;
				List<string> mounts = new List<string>();
				List<string> freeMounts = new List<string>();
				foreach (Disk disk in DiskHelper.GetDisksByAppSetting(appSetting.CheckDiskUsage))
				{
					if (disk.UsePercent != null && !string.IsNullOrEmpty(disk.MountedOn))
					{
						int usagePercent = int.Parse(disk.UsePercent.Replace("%", ""));
						if (usagePercent >= appSetting.CheckDiskUsage.TresholdAlert)
						{
							mounts.Add(disk.MountedOn);
							body += $"Disk `{disk.FileSystem}` mounted on `{disk.MountedOn}` have reached treshhold level.{Environment.NewLine}`{disk.Used}` has been used from total of `{disk.Size}`. Currently at `{disk.UsePercent}` usage. {Environment.NewLine}";
						}
						else
						{
							if (!string.IsNullOrEmpty(disk.MountedOn))
							{
								freeMounts.Add(disk.MountedOn);
							}
						}
					}
				}

				List<ProcessLog> processLogs = LiteDbHelper.GetProcessLogsByDateAndMounts(date, freeMounts);
				foreach (ProcessLog processLog in processLogs)
				{
					if (processLog.Id != null)
						LiteDbHelper.DeleteById<ProcessLog>(processLog.Id);
				}

				if (!string.IsNullOrEmpty(body))
				{
					string subject = $"Server Disk Alert - {appSetting.ServerName} - {DateTime.Now.ToLongDateString()}";
					if (appSetting.CheckDiskUsage.NotifyOnSpecificDate == null ||
						(appSetting.CheckDiskUsage.NotifyOnSpecificDate != null && appSetting.CheckDiskUsage.NotifyOnSpecificDate.Contains(DateTime.Now.Day)))
					{
						processLogs = LiteDbHelper.GetProcessLogsByDateNotifyAndMounts(date, "Email-CheckDiskUsage", mounts);

						if (appSetting.CheckDiskUsage.NotifyEmail &&
							appSetting.EmailSettings != null &&
							processLogs.Count <= 0)
						{
							bool result = EmailHelper.SendEmail(appSetting.EmailSettings, body.Replace("`", ""), subject);

							if (result)
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Email-CheckDiskUsage",
									IsSuccess = true,
									Mounts = mounts
								});
							}
							else
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Email-CheckDiskUsage",
									IsSuccess = false,
									Mounts = mounts
								});
							}
						}

						processLogs = LiteDbHelper.GetProcessLogsByDateNotifyAndMounts(date, "Discord-CheckDiskUsage", mounts);

						if (appSetting.CheckDiskUsage.NotifyDiscord &&
							appSetting.DiscordSettings != null &&
							processLogs.Count <= 0)
						{
							bool result = await DiscordWebhook.SendDiscordWebhook(appSetting.DiscordSettings, subject, string.Empty, body, $"{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}", 16711680);
							if (result)
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Discord-CheckDiskUsage",
									IsSuccess = true,
									Mounts = mounts
								});
							}
							else
							{
								LiteDbHelper.Insert(new ProcessLog
								{
									DateRun = date,
									NotifyService = "Discord-CheckDiskUsage",
									IsSuccess = false,
									Mounts = mounts
								});
							}
						}
					}
				}
			}
		}
	}
}
