using System;
using System.Net;
using System.IO;

public class HelloWorld{
	static public void Main(string[] args){
		if(args == null || args.Length == 0){
			throw new ApplicationException("Specify the URI of the resource to retrieve.");
		}

		Console.WriteLine("Hello MONO World!");
		Console.WriteLine("WebClient Check Starting...");

		WebClient client = new WebClient();
		Stream data = client.OpenRead(new Uri("https://" + args[0]));
		StreamReader reader = new StreamReader(data);
		client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
		
		try{
			string result = reader.ReadToEnd();
			Console.WriteLine(result);
			Console.WriteLine("\n\nWebClient Test Succeeded");
		}
		catch(Exception e){
			Console.WriteLine(e.Message);
			Console.WriteLine("\n\nWebClient Test Failed");
		}
		finally{
			data.Close();
			reader.Close();
		}
	}
}
