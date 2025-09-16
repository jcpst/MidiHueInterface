#!/bin/bash

# Build script for MidiHueInterface
# This script builds the application for multiple platforms

set -e

echo "🔧 Building MidiHueInterface for multiple platforms..."

# Create output directory
mkdir -p dist

# Build configurations
declare -A platforms=(
    ["win-x64"]="windows-x64"
    ["osx-x64"]="macos-x64"
    ["osx-arm64"]="macos-arm64"
    ["linux-x64"]="linux-x64"
)

# Clean previous builds
echo "🧹 Cleaning previous builds..."
rm -rf dist/*

# Restore dependencies
echo "📦 Restoring dependencies..."
dotnet restore

# Run tests
echo "🧪 Running tests..."
dotnet test --configuration Release

# Build for each platform
for runtime in "${!platforms[@]}"; do
    platform_name="${platforms[$runtime]}"
    echo "🏗️  Building for $platform_name ($runtime)..."
    
    dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj \
        --configuration Release \
        --runtime $runtime \
        --self-contained true \
        --output "./dist/$platform_name" \
        /p:PublishSingleFile=true \
        /p:PublishTrimmed=true \
        /p:TrimMode=partial \
        /p:DebugType=None \
        /p:DebugSymbols=false
        
    # Create archives
    echo "📦 Creating archive for $platform_name..."
    cd "dist/$platform_name"
    
    if [[ "$runtime" == "win-x64" ]]; then
        zip -r "../MidiHueInterface-$platform_name.zip" .
    else
        tar -czf "../MidiHueInterface-$platform_name.tar.gz" .
    fi
    
    cd ../..
done

echo "✅ Build complete! Check the dist/ directory for release files."
echo ""
echo "📁 Generated files:"
ls -la dist/*.zip dist/*.tar.gz 2>/dev/null || echo "No archive files found"