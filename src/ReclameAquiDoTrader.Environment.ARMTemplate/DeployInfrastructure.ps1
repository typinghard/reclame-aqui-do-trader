param
(
	[string] $appPrefix,
	[string] $environment,
	[string] $templatesLocation
)

$stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
$timing = ""
$timing = -join($timing, "1. Deployment started: ", $stopwatch.Elapsed.TotalSeconds, "`n")
Write-Host "1. Deployment started: "$stopwatch.Elapsed.TotalSeconds

$subscriptionId = "061440c6-b8b2-41ab-98dc-8863642471ff"
$location = "Brazil South"
$serviceAPIName = "$appPrefix-$environment"
$webhostingName = "app-plan-th-windows"
$serverFarmResourceGroup = "rg-$environment"
$alwaysOn = false
$currentStack = "dotnetcore"
$phpVersion = "OFF"
$timing = -join($timing, "2. Variables created: ", $stopwatch.Elapsed.TotalSeconds, "`n");

$whatifResultsJson = az deployment group what-if --no-pretty-print --only-show-errors --resource-group $serverFarmResourceGroup --name $serviceAPIName --template-file "$templatesLocation/WebApp.json" --parameters subscriptionId=$subscriptionId hostingPlanName=$webhostingName location=$location serviceAPIName=$serviceAPIName serverFarmResourceGroup=$serverFarmResourceGroup alwaysOn=$alwaysOn currentStack=$currentStack phpVersion=$phpVersion
$whatifResults = $whatifResultsJson | ConvertFrom-Json 
$ChangeResults12 = $whatifResults.changes