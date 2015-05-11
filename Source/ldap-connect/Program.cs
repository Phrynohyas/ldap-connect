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
			Console.WriteLine("    RemObjects InternetPack for .NET https://github.com/remobjects/internetpack");
			Console.WriteLine("    NDesk.Options");

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

			Console.ReadKey();
		}
	}
}
