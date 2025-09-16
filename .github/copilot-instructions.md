# MIDI Hue Interface

MIDI Hue Interface is a C# .NET 9.0 console application that allows controlling Philips Hue lights via MIDI input. The solution includes a console application, application layer, infrastructure layer, and test projects.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Prerequisites and Setup
- Install .NET 9.0 SDK (REQUIRED - project targets .NET 9.0):
  ```bash
  # Download and install .NET 9.0 SDK
  wget https://builds.dotnet.microsoft.com/dotnet/Sdk/9.0.305/dotnet-sdk-9.0.305-linux-x64.tar.gz
  mkdir -p $HOME/dotnet9
  tar -xzf dotnet-sdk-9.0.305-linux-x64.tar.gz -C $HOME/dotnet9
  export PATH="$HOME/dotnet9:$PATH"
  export DOTNET_ROOT="$HOME/dotnet9"
  dotnet --version  # Should show 9.0.305 or higher
  ```

### Build Process
- Always set the .NET 9.0 environment before any dotnet commands:
  ```bash
  export PATH="$HOME/dotnet9:$PATH"
  export DOTNET_ROOT="$HOME/dotnet9"
  ```
- Restore dependencies: `dotnet restore` -- takes 10-20 seconds. NEVER CANCEL. Set timeout to 60+ minutes.
- Build the solution: `dotnet build` -- takes 10-15 seconds for full build, 3-5 seconds for incremental. NEVER CANCEL. Set timeout to 30+ minutes.
- Clean build: `dotnet clean && dotnet build` -- takes 10-15 seconds total. NEVER CANCEL. Set timeout to 30+ minutes.

### Testing
- Run tests: `dotnet test` -- takes 2-5 seconds. NEVER CANCEL. Set timeout to 30+ minutes.
- **NOTE**: Test projects exist but contain no actual test files. Tests will show "No test is available" warnings but this is expected.

### Running the Application
- Run console app: `dotnet run --project MidiHueInterface.Console`
- **IMPORTANT**: The application requires an interactive terminal. It will crash in non-interactive environments with console buffer errors.
- The application displays a menu with options: Setup, Settings, Devices, Bridges, MIDI Interfaces, Test, Exit

## Validation

### Build Validation
- ALWAYS run `dotnet restore` before making any changes
- ALWAYS build with `dotnet build` after making changes
- Expect 5 compiler warnings about nullable reference types - these are NOT errors
- Build succeeds in Debug configuration by default

### Manual Testing Scenarios
Since the application requires interactive terminal access and hardware (MIDI devices, Hue bridges), comprehensive functional testing is limited in CI environments:

1. **Startup Test**: Verify the application starts and displays the main menu
   ```bash
   # This will show the menu but crash due to console limitations
   timeout 5 dotnet run --project MidiHueInterface.Console || echo "Expected: crashes in non-interactive mode"
   ```

2. **Build Artifact Verification**: Check that all expected outputs are produced
   ```bash
   find . -name "MidiHueInterface.*.dll" -path "*/bin/Debug/net9.0/*"
   # Should find: MidiHueInterface.Console.dll, MidiHueInterface.App.dll, MidiHueInterface.Infra.dll, and test DLLs
   ```

### Code Quality
- **No linting tools configured** - the project does not include EditorConfig, StyleCop, or other code formatting tools
- Build warnings about nullable reference types are acceptable and do not prevent successful compilation
- The solution follows standard .NET project structure and naming conventions

## Project Structure

### Solution Overview
- **MidiHueInterface.Console**: Main console application with interactive menu (entry point)
- **MidiHueInterface.App**: Application layer with business logic, handlers, services
- **MidiHueInterface.Infra**: Infrastructure layer with Hue client, MIDI listeners, repositories
- **MidiHueInterface.App.Tests**: Test project for application layer (currently empty)
- **MidiHueInterface.Infra.Tests**: Test project for infrastructure layer (currently empty)

### Key Dependencies
- **Sharprompt 3.0.0**: Interactive console menu system
- **HueApi 1.9.0**: Philips Hue bridge communication
- **Melanchall.DryWetMidi 8.0.2**: MIDI device handling
- **Microsoft.Extensions.Hosting 9.0.8**: Hosted service framework

### Common Issues
- **Console rendering errors**: The Sharprompt library requires interactive terminals and will crash in CI environments
- **MIDI device access**: Application expects MIDI input devices to be available
- **Hue bridge discovery**: Application attempts to discover Philips Hue bridges on the network

## Debugging Tips
- Use `dotnet build --verbosity detailed` for verbose build output
- Check build artifacts in `bin/Debug/net9.0/` directories
- The application uses dependency injection - check Program.cs for service registration
- Console application host is configured in Host.cs with menu navigation logic

## Development Workflow
1. Always ensure .NET 9.0 SDK is in PATH before starting
2. Run `dotnet restore` to ensure dependencies are current
3. Build with `dotnet build` to validate compilation
4. Make minimal code changes
5. Test build again with `dotnet build`
6. Commit changes when build succeeds