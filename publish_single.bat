@echo off
setlocal
cd /d %~dp0

REM Single-file, self-contained publish for win-x64
 dotnet restore
 dotnet publish -c Release -r win-x64 ^
  /p:SelfContained=true ^
  /p:PublishSingleFile=true ^
  /p:IncludeNativeLibrariesForSelfExtract=true

if %errorlevel% neq 0 (
	echo Publish failed.
	exit /b %errorlevel%
)

echo.
echo Output directory:
 echo   %cd%\bin\Release\net8.0-windows\win-x64\publish\

echo Done.
endlocal

