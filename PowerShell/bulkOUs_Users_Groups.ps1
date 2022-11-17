#assign variable to run import-csv command and the file path.
$ADOU = Import-csv "C:\Users\Administrator\Documents\AmawebOUs.csv"

#this will loop through each item in the list called $ADOU and pull the column value at each cell.
foreach ($ou in $ADOU)
{
	$displayName = $ou.DisplayName
	$name = $ou.Name
	$path = $ou.Path
	$description = $ou.Description
	#this command will run the New-ADOrganizationalUnit command and create a new OU with the following variables passed in from above.
	New-ADOrganizationalUnit `
	-DisplayName $displayName `
	-Name $name `
	-path $path `
	-description $Description `
}

----------------------------------------------------------------------------------------------------------------------------------

#assign variable to run import-csv command and the file path.
$ADUsers = Import-csv "C:\Users\Administrator\Documents\AmawebUsers.csv"

#this will loop through each item in the list called $ADUsers and pull the column value at each cell.
foreach ($User in $ADUsers)
{
    #Read user data from each field in each row and assign the data to a variable as below.
	$path = $User.Path
    $firstName = $User.FirstName
    $lastName = $User.LastName
    $userName = "$FirstName.$LastName"
    $mail = $userName + "@SoftDesign.com"
    $address = $User.Address
    $city = $User.City
    $state = $User.State
    $country = $User.Country
    $zipcode = $User.ZipCode
    $telephone = $User.telephone
    $company = $User.Company  
    $office = $User.Office
    $description = $User.description
    $manager = $User.manager
    $physicaloffice = $User.physicaloffice    
    $domain = "@SoftDesign.local"
    
    Write-Output $firstName 
    Write-Output $lastName 
    
    #Check to see if the user already exists in the Active Directory.
    if (Get-ADUser -F {SamAccountName -eq $userName}) 
    {
         #If user does exist, prompt a warning and wait for user to hit enter key to continue.
         Write-Warning "A user account with username $username already exists in Active Directory."
           pause
    }
    else
    {
        #User does not exist then proceed to create the new user account.
        $userProps = @{
        Path = $path
        SamAccountName = $userName 
        Name = "$firstName $lastName"
        GivenName = $firstName 
        Surname = $lastName
		email = $mail
		streetAddress = $address
        City = $city
		state = $state	   
		postalCode = $zipcode
		officePhone = $telephone
		company = $company
		office = $office
        Department = $office
        Title = $office
        Description = $description 	
		UserPrincipalName = "$username$domain"
        Enabled = $True
        displayName = $firstName + " " + $lastName
        AccountPassword = (ConvertTo-SecureString "Password1" -AsPlainText -Force)
        }
		#Creates a new AD User with the above variables.
        New-ADUser @userProps -Verbose
		
		#Replace the users country. Ex.Canada (c="CA";co="Canada";countrycode=124)
        set-aduser -identity $userName -Replace @{c="CA";co="Canada";countrycode=124}
		
		#Replace the users Manager field with the value from $manager 
        set-aduser -identity $userName -Replace @{Manager=$manager}

    }
  }

----------------------------------------------------------------------------------------------------------------------------------

#assign variable to run import-csv command and the file path.
$ADUsers = Import-csv "C:\Users\Administrator\Documents\AmawebGroups.csv"

#this will loop through each item in the list called $ADGroups and pull the column value at each cell.
foreach ($Group in $ADGroups)
{
    #Read user data from each field in each row and assign the data to a variable as below.
    Path = $Group.path
    Name = $Group.name
    Description = $Group.description 
    GroupScope = $Group.scope
    GroupCategory = $Group.category  
    
	#Creates a new AD group with the above variables
    New-ADGroup @groupProps
}

#Search the OU at location and add each user to the group. Ex. Users in the Executives OU are added to the Executives group. 
#Adds users from the Executives, Finance, Sales and HR OU to their respective groups.
Get-ADUser -SearchBase "OU=Executives,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Executives" -Members $_ }
Get-ADUser -SearchBase "OU=Finance,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Finance" -Members $_ }
Get-ADUser -SearchBase "OU=Sales,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Sales" -Members $_ }
Get-ADUser -SearchBase "OU=HR,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "HR" -Members $_ }

#For HQ Site, Adds users from the App, Network, Server and Programmers OU to their respective groups.
Get-ADUser -SearchBase "OU=App Support,OU=IT,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "App Support - HQ" -Members $_ }
Get-ADUser -SearchBase "OU=Network Support,OU=IT,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Network Support - HQ" -Members $_ }
Get-ADUser -SearchBase "OU=Server Support,OU=IT,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Server Support - HQ" -Members $_ }
Get-ADUser -SearchBase "OU=Programmers,OU=IT,OU=Departments - HQ,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Programmers - HQ" -Members $_ }

#For DC Site, Adds users from the App, Network, Server and Programmers OU to their respective groups.
Get-ADUser -SearchBase "OU=App Support,OU=IT,OU=Departments - DC,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "App Support - DC" -Members $_ }
Get-ADUser -SearchBase "OU=Network Support,OU=IT,OU=Departments - DC,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Network Support - DC" -Members $_ }
Get-ADUser -SearchBase "OU=Server Support,OU=IT,OU=Departments - DC,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Server Support - DC" -Members $_ }
Get-ADUser -SearchBase "OU=Programmers,OU=IT,OU=Departments - DC,DC=Amaweb,DC=local" -Filter * | ForEach-Object {Add-ADGroupMember -Identity "Programmers - DC" -Members $_ }