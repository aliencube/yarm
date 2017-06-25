# This script runs solution build.
Param(
	[string] [Parameter(Mandatory=$false)] $Configuration = "Debug",
	[string] [Parameter(Mandatory=$false)] $Platform = "Any CPU"
)

# Restores NuGet packages
Write-Host "Restoring NuGet packages ..." -ForegroundColor Green

nuget restore .\YARM.sln

Write-Host "NuGet packages restored" -ForegroundColor Green

# Builds solution
Write-Host "Building YARM.sln ..." -ForegroundColor Green

msbuild .\YARM.sln /t:Rebuild /p:Configuration=$Configuration /p:Platform=$Platform

# Builds Function App
$publishUrl = $("bin\" + $Configuration + "\PublishOutput")

msbuild .\src\Yarm.FunctionApp\Yarm.FunctionApp.csproj /t:Rebuild /p:Configuration=$Configuration /p:Platform=$Platform /p:OutputPath=bin /p:DeployOnBuild=true /p:PublishProfile=FolderProfile.pubxml /p:LastUsedBuildConfiguration=$Configuration /p:PublishUrl=$publishUrl

Write-Host "YARM.sln built" -ForegroundColor Green
