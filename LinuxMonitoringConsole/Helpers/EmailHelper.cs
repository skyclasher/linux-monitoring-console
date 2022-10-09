using LinuxMonitoringConsole.Models.AppSettings;
using System.Net;
using System.Net.Mail;

namespace LinuxMonitoringConsole.Helpers
{
	public static class EmailHelper
	{
		public static bool SendEmail(EmailSetting emailSetting, string body, string subject)
		{
			try
			{
				if (!string.IsNullOrEmpty(emailSetting.FromEmail) &&
					!string.IsNullOrEmpty(emailSetting.Host) &&
					emailSetting.ToEmail != null)
				{
					MailMessage mail = new MailMessage
					{
						From = new MailAddress(emailSetting.FromEmail, emailSetting.FromName),
						Subject = subject,
						Body = body
					};

					foreach (string email in emailSetting.ToEmail)
					{
						mail.To.Add(email);
					}

					if (emailSetting.CcEmail != null)
					{
						foreach (string email in emailSetting.CcEmail)
						{
							mail.CC.Add(email);
						}
					}

					SmtpClient smtp = new SmtpClient
					{
						Host = emailSetting.Host,
						Port = emailSetting.Port,
						EnableSsl = true,
						Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password),
					};
					System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
					smtp.Send(mail);
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
