Param (
  [Parameter(Mandatory=$true)]
  [string]$Version
)

$BaseDir = (Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)
$NuGetExe = Join-Path $BaseDir "packages\NuGet.CommandLine.2.8.6\tools\NuGet.exe"

$PackagePath = Join-Path $BaseDir "Linq.Extras\bin\Release\Linq.Extras.$Version.nupkg"
Invoke-Expression "$NuGetExe push $PackagePath"

