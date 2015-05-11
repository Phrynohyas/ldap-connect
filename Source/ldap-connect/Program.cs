using System;

namespace LdapConnect
{
	static class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("LDAP settings tester");
			Console.WriteLine("");
			Console.WriteLine("Powered by");
			Console.WriteLine("\tRemObjects InternetPack for .NET https://github.com/remobjects/internetpack");
			Console.WriteLine("\tNDesk.Options");

			var options = new LdapOptions();
			options.Parse(args);
			if (options.ShowDetailedHelp || !options.IsValid())
			{
				options.WriteOptionDescriptions(Console.Out);
				return;
			}

			var connection = new LdapConnection(options);

			// 1. Probe connection
			Console.WriteLine("Connecting to LDAP server...");
			connection.TryConnect();
			Console.WriteLine("Connecton successful. Hostname, port and SSL options are set correctly");

			Console.WriteLine();

			Console.WriteLine("Performing authentication attempt...");
			LdapUser user;
			if (string.IsNullOrEmpty(options.UserName))
			{
				Console.WriteLine("User name not set. Fake user name and password are used");
				user = connection.Authenticate(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
			}
			else
			{
				user = connection.Authenticate(options.UserName, options.Password);
			}

			Console.WriteLine(user.ToString());

			Console.WriteLine("Test completed");
		}
	}
}
