About
-----
This is a Dimension Data Cloud plugin for Citrix's Machine Creation API.

Latest Build: 
[![Build status](https://ci.appveyor.com/api/projects/status/f207bx7mipmb7qfu?svg=true)](https://ci.appveyor.com/project/tonybaloney/citrixprovisioningplugin)


Deploy
------

On the delivery controller:-

1. Open file explorer to C:\Program Files\Common Files\Citrix\HCLPlugins\CitrixMachineCreation\v1.0.0.0

2. Create a new folder within that directory, DimensionData

3. Copy the contents of the build into that directory

4. Open a powershell window.
```powershell
 cd "C:\Program Files\Common Files\Citrix\HCLPlugins\CitrixMachineCreation\v1.0.0.0"
 .\RegisterPlugins.exe -PluginsRoot "C:\Program Files\Common Files\Citrix\HCLPlugins\CitrixMachineCreation\v1.0.0.0"
 Restart-Service -Name "Citrix Host Service"
 Add-PSSnapin Citrix.Host.Admin.v2
 Get-HypHypervisorPlugin
 Restart-Service -Name "Citrix Broker Service"
 Restart-Service -Name "Citrix Machine Creation Service"
 ```
          
The plugin is now deployed.

Update
------

To drop in a new build, just stop the three services (Broker, Host, Machine Creation), copy in the new DLL, and
restart the services. No other steps are necessary.
