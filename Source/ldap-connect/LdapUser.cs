using System.Text;

namespace LdapConnect
{
	sealed class LdapUser
	{
		public LdapUser(string login)
		{
			this.Login = login;
			this.SecurityRoles = new string[0];
		}

		public string Login { get; set; }
		public string UserName { get; set; }
		public string UserDN { get; set; }
		public string[] SecurityRoles { get; set; }
		public bool IsAccessAllowed { get; set; }
		public bool IsRootAccessAllowed { get; set; }

		public override string ToString()
		{
			StringBuilder result = new StringBuilder(512);

			result.AppendLine(string.Format("Login: {0}", this.Login));
			if (!string.IsNullOrEmpty(UserDN))
			{
				result.AppendLine(string.Format("Name: {0}", this.UserName));
				result.AppendLine(string.Format("DN: {0}", this.UserDN));
				result.AppendLine(string.Format("Access allowed: {0}", this.IsAccessAllowed));
				result.AppendLine(string.Format("Advanced access allowed: {0}", this.IsRootAccessAllowed));

				if ((this.SecurityRoles != null) && (this.SecurityRoles.Length > 0))
				{
					result.AppendLine("Security roles:");
					foreach (string role in this.SecurityRoles)
					{
						result.AppendLine(string.Format("\t\t{0}", role));
					}
				}
				else
				{
					result.AppendLine("Security roles: Not Set");
				}
			}
			else
			{
				result.AppendLine("Authentication attempt failed");
			}

			return result.ToString();
		}
	}
}
