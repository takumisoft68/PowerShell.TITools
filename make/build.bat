@echo off
set module=TITools
cd /d %~dp0
cd ../

dotnet build .\src\module\TIToolsDll\TIToolsDll.csproj -o .\output\%module%\bin
xcopy .\src\manifest\ output\%module%\ /F /R /Y
powershell -NoProfile -ExecutionPolicy RemoteSigned .\make\MakeHelp.ps1
