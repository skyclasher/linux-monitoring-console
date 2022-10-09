using LinuxMonitoringConsole.Models.AppSettings;
using Microsoft.Extensions.Configuration;

namespace LinuxMonitoringConsole
{
	internal class Program
	{
		private static AppSetting _appSetting = new AppSetting();

		static async Task Main(string[] args)
		{
			InitApplication();
			await CheckDiskUsageProcess.Process(_appSetting);
			await CheckAllDiskProcess.Process(_appSetting);
		}

		private static void InitApplication()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("AppSettings.json");

			var config = builder.Build();

			_appSetting = config.GetSection("AppSettings").Get<AppSetting>();
		}
	}
}