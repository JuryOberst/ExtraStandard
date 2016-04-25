@echo off
set BUILD_CFG=%1
if "%BUILD_CFG%" equ "" set BUILD_CFG=Debug
dotnet restore
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet build src/Dataline.ExtraStandard -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/Dataline.ExtraStandard.Extra*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/Dataline.ExtraStandard.Validation*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/Dataline.ExtraStandard.GkvKomServer*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/Dataline.ExtraStandard.DrvKomServer*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build test/Dataline.*.Tests/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
