# This script runs solution build.
Param(
	[string] [Parameter(Mandatory=$true)] $TenantId,
	[string] [Parameter(Mandatory=$true)] $SubscriptionId,
	[string] [Parameter(Mandatory=$true)] $ClientId,
	[string] [Parameter(Mandatory=$true)] $ClientSecret,
	[string] [Parameter(Mandatory=$true)] $Environment,
	[string] [Parameter(Mandatory=$true)] $Location,
	[string] [Parameter(Mandatory=$true)] $LogStorageContainerSasToken,
	[string] [Parameter(Mandatory=$true)] $FunctionAppKey
)

# Login to Azure
Write-Host "Logging into Azure ..." -ForegroundColor Green

$password = ConvertTo-SecureString $ClientSecret -AsPlainText -Force
$credential = New-Object System.Management.Automation.PSCredential ($ClientId, $password)

Login-AzureRmAccount `
    -ServicePrincipal `
    -Tenant $TenantId `
    -SubscriptionId $SubscriptionId `
    -Credential $credential

Write-Host "Azure logged in" -ForegroundColor Green

# Deploy Azure resources
Write-Host "Deploying Azure resources ..." -ForegroundColor Green

New-AzureRmResourceGroupDeployment `
    -Name AppDeployment `
    -ResourceGroupName $("yarm-rg-" + $Environment + "-" + $Location) `
    -TemplateFile .\src\Yarm.ResourceGroup\app-deploy.json `
    -TemplateParameterFile .\src\Yarm.ResourceGroup\app-deploy.parameters.json `
    -environment $Environment `
    -location $Location `
    -logStorageContainerSasToken $LogStorageContainerSasToken `
    -remoteDebuggingEnabled $true `
    -functionAppKey $FunctionAppKey `
    -applicationInsightsEnabled $true

Write-Host "Azure resources deployed" -ForegroundColor Green

Remove-Variable password
Remove-Variable credential
