@echo off
setlocal
cd /d %~dp0

REM Restore
 dotnet restore

REM Publish single-file, self-contained (x64)
 dotnet publish -c Release -r win-x64 ^
  /p:PublishSingleFile=true ^
  /p:IncludeNativeLibrariesForSelfExtract=true

if %errorlevel% neq 0 (
	echo Publish failed.
	exit /b %errorlevel%
)

echo.
echo Self-contained output:
 echo   %cd%\bin\Release\net8.0-windows\win-x64\publish\

echo.
echo Also producing framework-dependent publish...
 dotnet publish -c Release -r win-x64 /p:PublishSingleFile=false

echo.
echo Framework-dependent output:
 echo   %cd%\bin\Release\net8.0-windows\win-x64\
endlocal

