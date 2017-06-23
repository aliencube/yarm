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

msbuild .\YARM.sln /t:Rebuild /p:Configuration=$Configuration /p:Platform=$Platform
