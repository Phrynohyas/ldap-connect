using NDesk.Options;

namespace LdapConnect
{
	sealed class LdapOptions : OptionSet, ILdapConnectionSettings
	{
		public LdapOptions()
		{
			this.ShowDetailedHelp = false;

			this.LdapServer = "localhost";
			this.LdapPort = 389;
			this.UserGroup = "";
			this.UserFilter = "(objectClass=inetOrgPerson)";
			this.UserNameField = "uid";

			this.GroupBaseDN = "ou=groups,dc=myserver,dc=com";
			this.GroupFilter = "(objectClass=groupOfNames)";
			this.GroupNameField = "cn";
			this.GroupMemberField = "member";
			this.RootGroup = "root";

			this.Add(@"user=", @"{username} to check (optional)", v => this.UserName = v);
			this.Add(@"password=", @"{password} to check (optional)", v => this.Password = v);

			this.Add(@"server=", @"LDAP server {hostname}. The default value is localhost (optional)", v => this.Password = v);
			this.Add(@"port=", @"LDAP {server port}. The default value is 389 (optional)", v => this.Password = v);
			this.Add(@"cert=", @"SSL {certificate} (optional)", v => this.LdapCertificate = v);

			this.Add(@"lookup_dn=", @"LDAP {server login}, f.e. for 'uid=ldapuser,ou=internal,dc=myserver,dc=com' (required)", v => this.LookupDN = v);
			this.Add(@"lookup_password=", @"LDAP {server password} (required)", v => this.LookupPassword = v);

			this.Add(@"user_base_dn=", @"user search {base DN}, f.e. 'ou=users,dc=myserver,dc=com' (required)", v => this.UserBaseDN = v);
			this.Add(@"user_group=", @"LDAP {group} all users should belong to (optional)", v => this.UserGroup = v);
			this.Add(@"user_filter=", @"LDAP entities {filter} used to distinguish user entries. The default value is '(objectClass=inetOrgPerson)' (optional)", v => this.UserFilter = v);
			this.Add(@"user_name_field=", @"LDAP {field} used as user name. The default value is 'uid' (optional)", v => this.UserNameField = v);

			this.Add(@"group_base_dn=", @"group search {base DN}, f.e. 'ou=groups,dc=myserver,dc=com' (required)", v => this.GroupBaseDN = v);
			this.Add(@"group_filter=", @"LDAP entities {filter} used to distinguish group entries. The default value is '(objectClass=groupOfNames)' (optional)", v => this.GroupFilter = v);
			this.Add(@"group_name_field=", @"LDAP {field} used as group name. The default value is 'cn' (optional)", v => this.GroupNameField = v);
			this.Add(@"group_member_field=", @"LDAP {field} containing links to LDAP users. The default value is 'member' (optional)", v => this.GroupMemberField = v);
			this.Add(@"group_root=", @"LDAP {group} that has full administrative access. The default value is 'root' (optional)", v => this.RootGroup = v);

			this.Add(@"h|?", @"show help", v => this.ShowDetailedHelp = (v != null));
		}

		public string UserName { get; private set; }
		public string Password { get; private set; }

		public string LdapServer { get; private set; }
		public int LdapPort { get; private set; }
		public string LdapCertificate { get; private set; }

		public string LookupDN { get; private set; }
		public string LookupPassword { get; private set; }

		public string UserBaseDN { get; private set; }
		public string UserGroup { get; private set; }
		public string UserFilter { get; private set; }
		public string UserNameField { get; private set; }

		public string GroupBaseDN { get; private set; }
		public string GroupFilter { get; private set; }
		public string GroupNameField { get; private set; }
		public string GroupMemberField { get; private set; }
		public string RootGroup { get; private set; }

		public bool ShowDetailedHelp { get; private set; }

		public bool IsValid()
		{
			if (!(string.IsNullOrEmpty(this.UserName) ^ string.IsNullOrEmpty(this.Password)))
				return false;

			if (string.IsNullOrEmpty(this.LookupDN) || string.IsNullOrEmpty(this.LookupPassword))
				return false;

			if (string.IsNullOrEmpty(this.UserBaseDN))
				return false;

			if (string.IsNullOrEmpty(this.GroupBaseDN))
				return false;

			return true;
		}
	}
}