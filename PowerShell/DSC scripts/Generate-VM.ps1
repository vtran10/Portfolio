 param (
    [string]$VMname = "2515-Victor3"
 )

 function Get-VMconfig ($filePath)
{
    $ini = @{}
    switch -regex -file $FilePath
    {
        "^\[(.+)\]" # Section
        {
            $section = $matches[1]
            $ini[$section] = @{}
            $CommentCount = 0
        }
        "^(;.*)$" # Comment
        {
            $value = $matches[1]
            $CommentCount = $CommentCount + 1
            $name = "Comment" + $CommentCount
            $ini[$section][$name] = $value
        }
        "(.+?)\s*=(.*)" # Key
        {
            $name,$value = $matches[1..2]
            $ini[$section][$name] = $value
        }
    }
    return $ini
}

$VMini = “E:\SourceFiles\$($VMname).ini”
$VMtemplate = "E:\VHDs\TMPL2016.vhdx"
$VMdiskpath = "F:\Virtual Disks\"
$VMvmpath = "F:\Virtual Machines\"
$VMunattend = "E:\SourceFiles\unattend.xml"
$VMgen = "1"
[int64]$VMram = 2GB
$VMswitch = "Production"

if ((!(Test-Path "$VMdiskpath$VMname.vhdx")) -And (!(Test-Path "$VMvmpath$VMname")))
{
    if (Test-Path “$VMini”)
    {
        $iniContent = Get-VMconfig “$VMini”
    }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“ram”] ) { [int64]$VMram = 1GB*$($iniContent[“HARDWARE”][“ram”]) } }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“template”] ) { $VMtemplate = "$($iniContent[“HARDWARE”][“template”])" } }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“diskpath”] ) { $VMdiskpath = "$($iniContent[“HARDWARE”][“diskpath”])" } }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“vmpath”] ) { $VMvmpath = "$($iniContent[“HARDWARE”][“vmpath”])" } }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“unattend”] ) { $VMunattend = "$($iniContent[“HARDWARE”][“unattend”])" } }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“gen”] ) { $VMgen = "$($iniContent[“HARDWARE”][“gen”])" } }
    if ( $inicontent ) { if ( $iniContent[“HARDWARE”][“switch”] ) { $VMswitch = "$($iniContent[“HARDWARE”][“switch”])" } }
    $VMini
    $VMtemplate
    $VMdiskpath
    $VMvmpath
    $VMunattend
    $VMgen
    $VMram
    $VMswitch
    Copy-Item -Path "$VMtemplate" -Destination "$VMdiskpath$VMname.vhdx" -Force
    $VHDLetter = (Mount-VHD -Path "$VMdiskpath$VMname.vhdx" -PassThru | Get-Disk | Get-Partition | where-object PartitionNumber -eq 2 | Get-Volume).DriveLetter
    Copy-Item -Path "$VMunattend" -Destination "$($VHDLetter):\Windows\Panther\"
    Dismount-VHD "$VMdiskpath$VMname.vhdx"
    New-VM -Name $VMname -Path "$VMvmpath" -NoVHD -Generation "$VMgen" -MemoryStartupBytes "$VMram" -SwitchName "$VMswitch"
    Add-VMHardDiskDrive -VMName $VMname -Path "$VMdiskpath$VMname.vhdx"
    # Upgrade VM
    [int64]$VMvcpu = $iniContent[“UPGRADES”][“vcpu”]
    [int64]$VMminram = 1GB*$iniContent[“UPGRADES”][“minram”]
    [int64]$VMmaxram = 1GB*$iniContent[“UPGRADES”][“maxram”]
    if ( $inicontent ) { if ( $inicontent[“UPGRADES”][“vcpu”] ) { Set-VM -Name $VMname -ProcessorCount $VMvcpu } }
    if ( $inicontent ) { if (( $inicontent[“UPGRADES”][“minram”] ) -And ( $iniContent[“UPGRADES”][“maxram”] )) { Set-VM -Name $VMname -DynamicMemory -MemoryMinimumBytes $VMminram -MemoryMaximumBytes $VMmaxram } }

    # Post-sysprep processing.
    Write-Host "Starting VM $VMname"
    Start-VM -Name $VMname
    if ($inicontent)
    {
        if ("$($inicontent['POSTCONFIG']['joindomain'])" -eq "yes") { 
        $cleartextpw = "Password1"
        if ( $inicontent ) { if ( $iniContent[“POSTCONFIG”][“localpw”] ) { $cleartextpw = $iniContent[“POSTCONFIG”][“localpw”] } }
        $VMLocalUser = "Administrator"
        $VMLocalPWord = ConvertTo-SecureString -String "$cleartextpw" -AsPlainText -Force
        $VMLocalCredential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $VMLocalUser, $VMLocalPWord
        while ((icm -VMName $VMName -Credential $VMLocalCredential {“Test”} -ea SilentlyContinue) -ne “Test”) {Sleep -Seconds 60}
	Write-Host "Invoking rename to $VMName"
        Invoke-Command -VMName $VMName -Credential $VMLocalCredential -ScriptBlock { param ($VMname); Rename-Computer $VMName -Restart } -ArgumentList $VMname
	Write-Host "Done renaming"
        do {Sleep -Seconds 120} while ((icm -VMName $VMName -Credential $VMLocalCredential {“Test”} -ea SilentlyContinue) -ne “Test”)
        $VMLocalUser = "$VMName\Administrator"
        $VMLocalCredential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $VMLocalUser, $VMLocalPWord
        $VMdomainuser = "Gundam.net\Administrator"
        $VMdomainpw = ("Password1" | ConvertTo-SecureString -asPlainText -Force)
        $VMdomaincred = New-Object System.Management.Automation.PSCredential($VMdomainuser,$VMdomainpw)
	Invoke-Command -VMName $VMName -Credential $VMLocalCredential -ScriptBlock { Write-Host "Releasing IP"; ipconfig /release }
	Invoke-Command -VMName $VMName -Credential $VMLocalCredential -ScriptBlock { Write-Host "Setting DNS"; Get-NetAdapter | Set-DnsClientServerAddress -ServerAddresses "10.0.0.11" }
	Invoke-Command -VMName $VMName -Credential $VMLocalCredential -ScriptBlock { Write-Host "Renewing IP"; ipconfig /renew }
	Write-Host "Invoking domain join with local credentials"
        Invoke-Command -VMName $VMName -Credential $VMLocalCredential -ScriptBlock { param ($VMdomaincred); Write-Host "Invoking domain join with domain credentials"; Add-Computer -DomainName Gundam.net -Credential $VMdomaincred -Restart } -ArgumentList $VMdomaincred
	Write-Host "Done with domain-join local credentials"
        do {Sleep -Seconds 60} while ((icm -VMName $VMName -Credential $VMLocalCredential {“Test”} -ea SilentlyContinue) -ne “Test”) 
        if ( $inicontent ) { if ( $iniContent[“POSTCONFIG”][“sw”] ) { $sw = $iniContent[“POSTCONFIG”][“sw”] } }
        if ( $inicontent ) { if ( $iniContent[“POSTCONFIG”][“sw”] ) { Invoke-Command -VMName $VMName -Credential $VMLocalCredential -ScriptBlock { Install-WindowsFeature NET-Framework-Features,NET-Framework-45-ASPNET,Web-Server,Web-App-Dev,Web-Mgmt-Tools -Restart } } }
        while ((icm -VMName $VMName -Credential $VMLocalCredential {“Test”} -ea SilentlyContinue) -ne “Test”) {Sleep -Seconds 60}
        } 
     }
}
else
{
    Write-Host "Source files exist that conflict with this request. Exiting..."
}

$password = ConvertTo-SecureString "Password1" -AsPlainText -Force
$Cred = New-Object System.Management.Automation.PSCredential ("GUNDAM\Administrator", $password)

Invoke-Command -ComputerName "2515-Victor3" -Credential $Cred -ScriptBlock {

Set-ExecutionPolicy Unrestricted -Force
Enable-PsRemoting -Force

Write-Host ""
Write-Host " . . . . . . Starting DSC Setup . . . . . . "

#Setting protocol to TLS 1.2
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$Net = [Net.ServicePointManager]::SecurityProtocol

Write-Host ""
Write-Host " . . . . . . Net Security Protocal is set to . . . . . . "; 
Write-Host "$Net";

Write-Host ""
Write-Host " . . . . . . Installing NuGet . . . . . . "

#Setting up NuGet manager
Install-PackageProvider -Name NuGet -MinimumVersion 2.8.5.201 -Force

Write-Host ""
Write-Host " . . . . . . Setting PSGallery as Trusted . . . . . . "

#Seting PSGallery as trusted 
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Set-PSRepository -Name "PSGallery" -InstallationPolicy Trusted | write-host 'y';

Write-Host ""
Write-Host " . . . . . . Installing Modules . . . . . . "

Write-Host ""
Write-Host " . . . . . . Installing Modules: xSmbShare . . . . . . "
Install-Module -Name xSmbShare -Force

Write-Host ""
Write-Host " . . . . . . Installing Modules: Web-Server . . . . . . "
Install-WindowsFeature Web-Server -IncludeManagementTools

Write-Host ""
Write-Host " . . . . . . Installing Modules: ComputerManagementDsc . . . . . . "
Install-Module xComputerManagement -Force

Write-Host ""
Write-Host " . . . . . . Installing Modules: NetworkingDsc . . . . . . "
Install-Module xNetworking -Force

Write-Host ""
Write-Host " . . . . . . Installing Modules: xWebAdministration . . . . . . "
Install-Module "xWebAdministration" -Force

Write-Host ""
Write-Host " . . . . . . Installing Modules: xSystemSecurity . . . . . . "
Install-Module xSystemSecurity -Force

}#scriptblock ends
#hypver-v host then compiles DSC configuration

cd "E:\SourceFiles"

. .\DSC_Config.ps1 

DSC_Config

$Job  = Start-DscConfiguration .\DSC_Config\ -force | Get-Job
$ID = $job.Id
Wait-Job -id "$ID"

Start-Sleep -s 5

Test-DscConfiguration .\DSC_Config -verbose