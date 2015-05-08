namespace LdapConnect
{
	interface ILdapConnectionSettings
	{
		string LdapServer { get; }
		int LdapPort { get; }
		string LdapCertificate { get; }

		string LookupDN { get; }
		string LookupPassword { get; }

		string UserBaseDN { get; }
		string UserGroup { get; }
		string UserFilter { get; }
		string UserNameField { get; }

		string GroupBaseDN { get; }
		string GroupFilter { get; }
		string GroupNameField { get; }
		string GroupMemberField { get; }
		string RootGroup { get; }
	}
}