Set-ExecutionPolicy Unrestricted -Force
Enable-PSRemoting -Force

Configuration DSC_Config1 #Part1
{
   # A Configuration block indicates a DSC file. It can have zero or more
   # Node blocks. Normal PowerShell code can also appear here.
   Import-DscResource –ModuleName 'PSDesiredStateConfiguration'
   Import-DscResource -Module xSmbShare 

   # Nodes are the endpoint we wish to configure
   # Each host should have its own Node block
   Node “2515-Victor1”
   {
      # Node blocks specify one or more resource blocks. Resources are simply
      # PowerShell modules that implement the logic of "how" to execute a task.
      File CreateFolder
      {
         DestinationPath = 'C:\Repo'
         Type = 'Directory'
         Ensure = 'Present'
      }
      xSmbShare MySMBShare
      {
          Ensure      = "Present"
          Name        = "MyRepo"
          Path        = "C:\Repo"
          ReadAccess  = "GUNDAM\Domain Users"
          FullAccess  = "GUNDAM\Domain Admins"
          Description = "Proof of concept share"
          DependsOn = '[File]CreateFolder'
      }
   }
}

Configuration DSC_Config2 #Part2
{
   # A Configuration block indicates a DSC file. It can have zero or more
   # Node blocks. Normal PowerShell code can also appear here.
   Import-DscResource –ModuleName 'PSDesiredStateConfiguration'
   Import-DscResource -ModuleName xWebAdministration
   Import-DscResource -ModuleName xWebSite

   # Nodes are the endpoint we wish to configure
   # Each host should have its own Node block
   Node “2515-Victor1”
   {
      # Node blocks specify one or more resource blocks. Resources are simply
      # PowerShell modules that implement the logic of "how" to execute a task.
      WindowsFeature IIS
      {         
         Name           = "Web-Server"
         Ensure         = "Present"
      }
      WindowsFeature ASP.Net45
      {
        Name            = "Web-Asp-Net45"
        Ensure          = "Present"
      }
      xWebSite DefaultSite
      {
        Name            = "Default-Website"
        Ensure          = "Present"
        State           = "Stopped"
        PhysicalPath    = "c:\inetpub\wwwroot"
        DependsOn       = "[WindowsFeature IIS]"
      }
      File WebContent
      {
        Ensure          = "Present"
        SourcePath      = "\\2515-HyperV\MyRepo"
        DestinationPath = "c:\inetpub\studentweb"
        Recurse         = "$true"
        Type            = "Directory"
        DependsOn       = "[WindowsFeature]Asp-Net45"
      }
      xWebsite studentweb
      {
        Ensure          = "Present"
        Name            = "studentweb"
        State           = "Started"
        PhysicalPath    = "c:\inetpub\studentweb"
        DependsOn       = "[File]WebContent"
      }
   }
}
