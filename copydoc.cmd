@echo off
pushd %~dp0
xcopy docs\api api /s /y
popd