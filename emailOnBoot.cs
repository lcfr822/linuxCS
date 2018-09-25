using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;

public static class SendExternIP{
	public static void Main(string[] args){
		MailMessage mail = new MailMessage("boot_bot@gmail.com", "lcfr822@gmail.com");
		SmtpClient client = new SmtpClient();

		client.Port = 587;
		client.DeliveryMethod = SmtpDeliveryMethod.Network;
		client.Host = "smtp.gmail.com";
		client.EnableSsl = true;
		client.UseDefaultCredentials = false;
		client.Credentials = new System.Net.NetworkCredential("lcfr822@gmail.com", "pgkamfbzjennabec");
		mail.Subject = "BOOT-BOT NOTIFICATION";
		
		string[] body = GetBootData().Split('\n');
		string compBody = "\"BEDPUTER\" SERVER BOOTED SUCCESSFULLY\n\nAVAILABLE UPDATES\n";
		compBody += body[0];
		compBody += body[1];
		compBody += "\n\nCURRENT EXTERNAL IP ADDRESS\n";
		compBody += body[2];
		mail.Body = compBody;

		mail.BodyEncoding = UTF8Encoding.UTF8;
		mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
		client.Send(mail);

		Environment.Exit(0);
	}

	private static string GetBootData(){
		string command = "/usr/lib/update-notifier/apt-check --human-readable && dig +short myip.opendns.com @resolver1.opendns.com";
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
