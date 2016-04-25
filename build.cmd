@echo off
set BUILD_CFG=%1
if "%BUILD_CFG%" equ "" set BUILD_CFG=Debug
dotnet restore
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet build src/Dataline.ExtraStandard*/project.json -c %BUILD_CFG% --version-suffix "%2"
