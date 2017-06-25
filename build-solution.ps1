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

Write-Host "YARM.sln built" -ForegroundColor Green

# Builds web app
# Write-Host "Building web app ..." -ForegroundColor Green

# msbuild .\src\Yarm.WebApp\Yarm.WebApp.csproj /t:Rebuild /p:Configuration=$Configuration /p:Platform=$Platform /p:OutputPath=bin /p:DeployOnBuild=true /p:PublishProfile=FolderProfile.pubxml /p:LastUsedBuildConfiguration=$Configuration

# Write-Host "Web app built" -ForegroundColor Green

# Builds function app
Write-Host "Building function app ..." -ForegroundColor Green

msbuild .\src\Yarm.FunctionApp\Yarm.FunctionApp.csproj /t:Rebuild /p:Configuration=$Configuration /p:Platform=$Platform /p:OutputPath=bin /p:DeployOnBuild=true /p:PublishProfile=FolderProfile.pubxml /p:LastUsedBuildConfiguration=$Configuration

Write-Host "Function app built" -ForegroundColor Green
