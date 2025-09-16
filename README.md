# Midi Hue Interface

C# application for controlling Hue lights via MIDI.

## Download

Pre-built releases are available for Windows, macOS, and Linux on the [Releases page](https://github.com/jcpst/MidiHueInterface/releases).

### Supported Platforms
- **Windows**: x64 (MidiHueInterface-windows-x64.zip)
- **macOS**: x64 and ARM64 (MidiHueInterface-macos-x64.tar.gz, MidiHueInterface-macos-arm64.tar.gz)
- **Linux**: x64 (MidiHueInterface-linux-x64.tar.gz)

All releases are self-contained and don't require .NET runtime installation.

## Development

### Prerequisites
- .NET 9.0 SDK

### Building from Source

```bash
# Clone the repository
git clone https://github.com/jcpst/MidiHueInterface.git
cd MidiHueInterface

# Restore dependencies
dotnet restore

# Build the application
dotnet build --configuration Release

# Run tests
dotnet test

# Run the console application
dotnet run --project MidiHueInterface.Console
```

### Creating a Release Build

For detailed information about creating and publishing releases, see [RELEASE.md](RELEASE.md).

```bash
# Build for current platform
dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj \
  --configuration Release \
  --self-contained true \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true

# Build for specific platform (replace runtime as needed)
dotnet publish MidiHueInterface.Console/MidiHueInterface.Console.csproj \
  --configuration Release \
  --runtime win-x64 \
  --self-contained true \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true

# Build for all platforms using the provided script
./build.sh        # Unix/Linux/macOS
build.bat         # Windows
```

## Setup

