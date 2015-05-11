using System;
using System.Collections.Generic;
using System.Linq;
using RemObjects.InternetPack.Ldap;

namespace LdapConnect
{
	sealed class LdapConnection : IDisposable
	{
		private readonly LdapUserLookup _ldap;
		private readonly string _userGroupName;
		private readonly string _rootGroupName;

		public LdapConnection(ILdapConnectionSettings settings)
		{
			this._ldap = new LdapUserLookup();

			this._ldap.Hostname = settings.LdapServer;
			this._ldap.Port = settings.LdapPort;

			this._ldap.LookupDN = settings.LookupDN;
			this._ldap.LookupPassword = settings.LookupPassword;

			this._ldap.UserSearchBase = settings.UserBaseDN;
			this._ldap.UserFilter = settings.UserFilter;
			this._ldap.UserNameField = settings.UserNameField;

			this._ldap.StripGroupBaseDN = true;

			this._ldap.GroupSearchBase = settings.GroupBaseDN;
			this._ldap.GroupFilter = settings.GroupFilter;
			this._ldap.GroupNameField = settings.GroupNameField;
			this._ldap.GroupMemberField = settings.GroupMemberField;

			if (!string.IsNullOrEmpty(settings.LdapCertificate))
			{
				this._ldap.SslOptions.Enabled = true;
				this._ldap.SslOptions.CertificateFileName = settings.LdapCertificate;
			}

			this._userGroupName = settings.UserGroup;
			this._rootGroupName = settings.RootGroup;
		}

		~LdapConnection()
		{
			this.Dispose();
		}

		public void Dispose()
		{
			if (this._ldap == null)
				return;

			this._ldap.Dispose();

			GC.SuppressFinalize(this);
		}

		public void TryConnect()
		{
			using (var client = new LdapClient())
			{
				client.SslOptions = this._ldap.SslOptions;
				client.UseStartTLS = this._ldap.UseStartTLS;
				client.HostName = this._ldap.Hostname;
				client.Port = this._ldap.Port;

				client.Open();
				client.Close();
			}
		}

		public LdapUser Authenticate(string username, string password)
		{
			var result = new LdapUser(username);

			var lookup = this._ldap.Login(username, password);

			if (lookup == null)
			{
				return result;
			}

			result.UserName = lookup.Username;
			result.UserDN = lookup.DN;
			result.IsAccessAllowed = LdapConnection.CheckGroupMembership(lookup.GroupMembership, this._userGroupName, true);
			result.IsRootAccessAllowed = result.IsAccessAllowed && LdapConnection.CheckGroupMembership(lookup.GroupMembership, this._rootGroupName, false);
			result.SecurityRoles = lookup.GroupMembership.ToArray();

			return result;
		}

		private static bool CheckGroupMembership(IEnumerable<string> groups, string group, bool defaultValue)
		{
			if (string.IsNullOrEmpty(group))
				return defaultValue;

			return groups.Any(g => string.Equals(g, group, StringComparison.OrdinalIgnoreCase));
		}
	}
}