@echo off
REM Build script for MidiHueInterface
REM This script builds the application for multiple platforms

echo 🔧 Building MidiHueInterface for multiple platforms...

REM Create output directory
if exist dist rmdir /s /q dist
mkdir dist

echo 🧹 Cleaning previous builds...

echo 📦 Restoring dependencies...
dotnet restore
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo 🧪 Running tests...
dotnet test --configuration Release
if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

REM Build for Windows x64
echo 🏗️  Building for Windows x64...
dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj ^
    --configuration Release ^
    --runtime win-x64 ^
    --self-contained true ^
    --output "./dist/windows-x64" ^
    /p:PublishSingleFile=true ^
    /p:PublishTrimmed=true ^
    /p:TrimMode=partial ^
    /p:DebugType=None ^
    /p:DebugSymbols=false

if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo 📦 Creating archive for Windows x64...
cd dist\windows-x64
powershell -Command "Compress-Archive -Path * -DestinationPath '..\MidiHueInterface-windows-x64.zip'"
cd ..\..

REM Build for macOS x64
echo 🏗️  Building for macOS x64...
dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj ^
    --configuration Release ^
    --runtime osx-x64 ^
    --self-contained true ^
    --output "./dist/macos-x64" ^
    /p:PublishSingleFile=true ^
    /p:PublishTrimmed=true ^
    /p:TrimMode=partial ^
    /p:DebugType=None ^
    /p:DebugSymbols=false

if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo 📦 Creating archive for macOS x64...
cd dist\macos-x64
tar -czf "..\MidiHueInterface-macos-x64.tar.gz" *
cd ..\..

REM Build for macOS ARM64
echo 🏗️  Building for macOS ARM64...
dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj ^
    --configuration Release ^
    --runtime osx-arm64 ^
    --self-contained true ^
    --output "./dist/macos-arm64" ^
    /p:PublishSingleFile=true ^
    /p:PublishTrimmed=true ^
    /p:TrimMode=partial ^
    /p:DebugType=None ^
    /p:DebugSymbols=false

if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo 📦 Creating archive for macOS ARM64...
cd dist\macos-arm64
tar -czf "..\MidiHueInterface-macos-arm64.tar.gz" *
cd ..\..

REM Build for Linux x64
echo 🏗️  Building for Linux x64...
dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj ^
    --configuration Release ^
    --runtime linux-x64 ^
    --self-contained true ^
    --output "./dist/linux-x64" ^
    /p:PublishSingleFile=true ^
    /p:PublishTrimmed=true ^
    /p:TrimMode=partial ^
    /p:DebugType=None ^
    /p:DebugSymbols=false

if %ERRORLEVEL% neq 0 exit /b %ERRORLEVEL%

echo 📦 Creating archive for Linux x64...
cd dist\linux-x64
tar -czf "..\MidiHueInterface-linux-x64.tar.gz" *
cd ..\..

echo ✅ Build complete! Check the dist/ directory for release files.
echo.
echo 📁 Generated files:
dir dist\*.zip dist\*.tar.gz

pause