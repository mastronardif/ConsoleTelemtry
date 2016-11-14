
#C:\FxM\Dev\Azure\ConsoleTelemtry\packages\Microsoft.ApplicationInsights.2.1.0\lib\net45\Microsoft.ApplicationInsights.dll

Clear-Host   
Add-Type -Path "C:\FxM\Dev\Azure\ConsoleTelemtry\packages\Microsoft.ApplicationInsights.2.1.0\lib\net45\\Microsoft.ApplicationInsights.dll"  

$keyInst = "757ccff8-b03d-431b-a82d-f3ab8b9c929d"
$client = New-Object Microsoft.ApplicationInsights.TelemetryClient  
$client.InstrumentationKey="757ccff8-b03d-431b-a82d-f3ab8b9c929d"  

$eventName = "Hello Event"  
$client.TrackEvent($eventName, $null)   

$message = "where is it?"
Write-Host $message -ForegroundColor Green


$client.Flush();

 Get-Content z:\logs\myball.exe.log  | Select-String "ERROR "
  Get-Content Z:\logs\afrODSAPPSdev\z:\Logs\ODSToLoanCenterSync.log  | Select-String "ERROR "
  Z:\logs\afrODSAPPSdev
  z:\Logs\ODSToLoanCenterSync.log