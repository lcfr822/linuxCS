using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

public static class SendWeather{
	public static void Main(string[] args){
		MailMessage mail = new MailMessage("weather_bot@gmail.com", "lcfr822@gmail.com");
		SmtpClient client = new SmtpClient();

		client.Port = 587;
		client.DeliveryMethod = SmtpDeliveryMethod.Network;
		client.Host = "smtp.gmail.com";
		client.EnableSsl = true;
		client.UseDefaultCredentials = false;
		client.Credentials = new System.Net.NetworkCredential("lcfr822@gmail.com", "pgkamfbzjennabec");
		mail.Subject = "WEATHER-BOT UPDATE";
		mail.Body = GetWeatherData();
		mail.BodyEncoding = UTF8Encoding.UTF8;
		mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
		client.Send(mail);

	}

	private static string GetWeatherData(){
		string command = "weather-util 98502";
		command.Replace("\"","\\\"");
		var process = new Process()
		{
			StartInfo = new ProcessStartInfo{
				FileName = "/bin/bash",
				Arguments = "-c \"" + command + "\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
			}
		};

		process.Start();
		string result = process.StandardOutput.ReadToEnd();
		process.WaitForExit();

		return result;
	}
}
