using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

public static class SendExternIP{
	public static void Main(string[] args){
		if(GetIPData() == System.IO.File.ReadAllText(@"/home/lcfr822/Documents/externalIP.txt")){
			Console.WriteLine("External IP Still Valid");
			return;
		}
		else{
			System.IO.File.WriteAllText(@"/home/lcfr822/Documents/externalIP.txt", GetIPData());
		}
		
		MailMessage mail = new MailMessage("weather_bot@gmail.com", "lcfr822@gmail.com");
		SmtpClient client = new SmtpClient();

		client.Port = 587;
		client.DeliveryMethod = SmtpDeliveryMethod.Network;
		client.Host = "smtp.gmail.com";
		client.EnableSsl = true;
		client.UseDefaultCredentials = false;
		client.Credentials = new System.Net.NetworkCredential("lcfr822@gmail.com", "pgkamfbzjennabec");
		mail.Subject = "IP-BOT UPDATE";
		mail.Body = GetIPData();
		mail.BodyEncoding = UTF8Encoding.UTF8;
		mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
		client.Send(mail);

	}

	private static string GetIPData(){
		string command = "dig +short myip.opendns.com @resolver1.opendns.com";
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
