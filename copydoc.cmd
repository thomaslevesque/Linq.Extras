@echo off
pushd %~dp0
xcopy ..\Linq.Extras\docs\api api /s /y
popd