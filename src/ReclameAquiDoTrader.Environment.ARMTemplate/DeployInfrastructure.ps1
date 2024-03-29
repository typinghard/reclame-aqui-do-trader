param
(
	[string] $appPrefix,
	[string] $environment,
	[string] $templatesLocation,
	[string] $azureStorageConnectionString,
	[string] $ravenDbConnectionConfigsCertificateDownloadPath,
	[string] $ravenSettingsCertFilePath,
	[string] $ravenSettingsCertPassword,
	[string] $ravenSettingsDatabaseName,
	[string] $ravenSettingsUrl
)

$stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
$timing = ""
$timing = -join($timing, "1. Deployment started: ", $stopwatch.Elapsed.TotalSeconds, "`n")
Write-Host "1. Deployment started: "$stopwatch.Elapsed.TotalSeconds

$subscriptionId = "061440c6-b8b2-41ab-98dc-8863642471ff"
$location = "Brazil South"
$serviceAPIName = "$appPrefix-$environment"
$webhostingName = "app-plan-th-windows"
$serverFarmResourceGroup = "rg-radt-$environment"
$alwaysOn = false
$currentStack = "dotnetcore"
$phpVersion = "OFF"
$timing = -join($timing, "2. Variables created: ", $stopwatch.Elapsed.TotalSeconds, "`n");


if ($(az group exists --name $serverFarmResourceGroup) -eq 'false') 
{
	az group create -l brazilsouth -n $serverFarmResourceGroup

	az deployment group create --resource-group $serverFarmResourceGroup --name $serviceAPIName --template-file "$templatesLocation/WebApp.json" --parameters subscriptionId=$subscriptionId hostingPlanName=$webhostingName location=$location name=$serviceAPIName serverFarmResourceGroup=$serverFarmResourceGroup alwaysOn=$alwaysOn currentStack=$currentStack phpVersion=$phpVersion

	az deployment group wait --name $serviceAPIName --resource-group $serverFarmResourceGroup

	az webapp config appsettings set -g $serverFarmResourceGroup -n $serviceAPIName --output none `
											--settings AzureStorage:ConnectionString=$azureStorageConnectionString `
												   RavebDBConnectionConfigs:Certificate:DownloadPath=$ravenDbConnectionConfigsCertificateDownloadPath `
												   RavenSettings:CertFilePath=$ravenSettingsCertFilePath `
												   RavenSettings:CertPassword=$ravenSettingsCertPassword `
												   RavenSettings:DatabaseName=$ravenSettingsDatabaseName `
												   RavenSettings:Urls:0=$ravenSettingsUrl
	Write-Host "1. Deployment finished: "$stopwatch.Elapsed.TotalSeconds
}
