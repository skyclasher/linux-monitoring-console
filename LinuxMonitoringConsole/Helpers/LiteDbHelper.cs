using LinuxMonitoringConsole.Models.Database;
using LiteDB;

namespace LinuxMonitoringConsole.Helpers
{
	public static class LiteDbHelper
	{
		private static readonly string DATABASE_NAME = @"Data.db";

		public static List<ProcessLog> GetProcessLogsByDateNotifyAndMounts(string date, string notifyService, List<string> mounts)
		{
			using (var db = new LiteDatabase(DATABASE_NAME))
			{
				var col = db.GetCollection<ProcessLog>();
				return col.Find(x => x.DateRun == date && x.IsSuccess == true && x.NotifyService == notifyService && x.Mounts == mounts).ToList();
			}
		}

		public static List<ProcessLog> GetProcessLogsByDateAndNotify(string date, string notifyService)
		{
			using (var db = new LiteDatabase(DATABASE_NAME))
			{
				var col = db.GetCollection<ProcessLog>();
				return col.Find(x => x.DateRun == date && x.IsSuccess == true && x.NotifyService == notifyService).ToList();
			}
		}

		public static List<ProcessLog> GetProcessLogsByDateAndMounts(string date, List<string> mounts)
		{
			using (var db = new LiteDatabase(DATABASE_NAME))
			{
				var col = db.GetCollection<ProcessLog>();
				List<ProcessLog> processLogs = col.Find(x => x.DateRun == date && x.IsSuccess == true).ToList();
				List<ProcessLog> result = new List<ProcessLog>();
				foreach (string mount in mounts)
				{
					result.AddRange(processLogs.Where(x => x.Mounts != null && x.Mounts.Contains(mount)).ToList());
				}
				return result;
			}
		}

		public static void Insert<T>(T entity) where T : class
		{
			using (var db = new LiteDatabase(DATABASE_NAME))
			{
				var col = db.GetCollection<T>();
				col.Insert(entity);
			}
		}

		public static void DeleteById<T>(ObjectId id) where T : class
		{
			using (var db = new LiteDatabase(DATABASE_NAME))
			{
				var col = db.GetCollection<T>();
				col.Delete(id);
			}
		}
	}
}
