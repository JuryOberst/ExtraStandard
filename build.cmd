@echo off
set BUILD_CFG=%1
if "%BUILD_CFG%" equ "" set BUILD_CFG=Debug
dotnet restore
if %errorlevel% neq 0 exit /b %errorlevel%
dotnet build src/ExtraStandard -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/ExtraStandard.Extra*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/ExtraStandard.Validation*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/ExtraStandard.GkvKomServer*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build src/ExtraStandard.DrvKomServer*/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
dotnet build test/*.Tests/project.json -c %BUILD_CFG%
if %errorlevel% neq  0 exit /b %errorlevel%
