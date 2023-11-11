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