#this enables exchange to see users in all domains
Set-ADServerSettings -ViewEntireForest $true

#get count of all users
(get-user -OrganizationalUnit "ou=Departments - dc,DC=amaweb,dc=local").count #20
(get-user -OrganizationalUnit "ou=Departments - hq,DC=amaweb,dc=local").count #90

(get-user -OrganizationalUnit "ou=Departments,DC=softdesign,dc=local" -DomainController dc03.softdesign.local).count # 80

(get-user -OrganizationalUnit "ou=Departments,dc=learning,DC=softdesign,dc=local" -DomainController dc04.learning.softdesign.local).count #50

#enable mailboxes for all users in departments and sub OUs
get-user -OrganizationalUnit "ou=Departments - dc,DC=amaweb,dc=local" | enable-Mailbox
get-user -OrganizationalUnit "ou=Departments - hq,DC=amaweb,dc=local" | enable-Mailbox
get-user -OrganizationalUnit "ou=Departments,DC=softdesign,dc=local" | enable-Mailbox
get-user -OrganizationalUnit "ou=Departments,dc=learning,DC=softdesign,dc=local" | enable-Mailbox