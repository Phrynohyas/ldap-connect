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
				options.WriteOptionDescriptions(Console.Out);

			Console.ReadKey();
		}
	}
}
