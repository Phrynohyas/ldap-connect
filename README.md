# ldap-connect

Simple tool to test LDAP server connection and authentication settings. This tool allows to test the LDAP server conenction settings that can be later used as [LDAP connection provider](http://wiki.remobjects.com/wiki/Relativity_Server_Concepts#Login_provider) settings for the [Relativity Server](http://wiki.remobjects.com/wiki/Relativity).

Command line sample:
```
ldap-connect --server=192.168.180.128 --port=10389 --lookup_dn="uid=admin,ou=system" --lookup_password="secret" --user_base_dn="ou=users,dc=example,dc=com" --group_base_dn="ou=groups,dc=example,dc=com" --user=alpha --password=bravo
```

All tool options are described vis the following command:
```
ldap-connect ?
```
|Option              | Required | Default value | Description                                          |
| ------------------ |:--------:|---------------|------------------------------------------------------|
|--user=username     |          |               |username                                              |
|--password=password |          |               |password to check                                     |
|--server=hostname   |          |localhost      |LDAP server hostname                                  |
|--port=server port  |          |389            |LDAP server port                                      |
|--cert=certificate  |          |               |SSL certificate                                       |
|--lookup_dn=login   | +        |               |LDAP server login, f.e. 'uid=ldapuser,ou=internal,dc=myserver,dc=com'|
|--lookup_password=password| +  |               |LDAP server password                                  |
|--user_base_dn=base DN | +     |               |user search base DN, f.e. 'ou=users,dc=myserver,dc=com'|
|--user_group=group  |          |empty string   |LDAP group all users should belong to                 |
|--user_filter=filter|          |(objectClass=inetOrgPerson)|LDAP entities filter used to distinguish user entries|
|--user_name_field=field|       |uid            |LDAP field used as user name                          |
|--group_base_dn=base DN| +     |               |group search base DN, f.e. 'ou=groups,dc=myserver,dc=com'|
|--group_filter=filter|         |(objectClass=groupOfNames)|LDAP entities filter used to distinguish group entries|
|--group_name_field=field|      |cn             |LDAP field used as group name                         |
|--group_member_field=field|    |member         |LDAP field containing links to LDAP users             |
|--group_root=group  |          |root           |LDAP group that has full administrative access        |


* Powered by RemObjects InternetPack for .NET
