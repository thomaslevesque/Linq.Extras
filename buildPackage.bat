@echo off
set OUTDIR=NuGet\bin
mkdir %OUTDIR% 2> nul
nuget pack NuGet\Linq.Extras.nuspec -OutputDirectory %OUTDIR%

