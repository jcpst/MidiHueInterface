# Release Process

This document explains how to create and publish releases for MidiHueInterface.

## Automated Release via Git Tags

The easiest way to create a release is by pushing a git tag:

```bash
# Create and push a new tag
git tag v1.0.0
git push origin v1.0.0
```

This will automatically:
1. Run all tests
2. Build the application for all supported platforms (Windows x64, macOS x64/ARM64, Linux x64)
3. Create release archives (`.zip` for Windows, `.tar.gz` for Unix-like systems)
4. Create a GitHub release with the archives attached
5. Generate release notes automatically

## Manual Release Trigger

You can also manually trigger a release build from the GitHub Actions tab:

1. Go to the "Actions" tab in the GitHub repository
2. Select the "Release" workflow
3. Click "Run workflow"
4. Enter the tag name (e.g., `v1.0.0`)
5. Click "Run workflow"

## Supported Platforms

The release pipeline builds for the following platforms:

- **Windows x64**: Self-contained executable (`.exe`)
- **macOS x64**: Self-contained executable (Intel Macs)
- **macOS ARM64**: Self-contained executable (Apple Silicon Macs)
- **Linux x64**: Self-contained executable

All builds are:
- Self-contained (no .NET runtime required)
- Single-file executables
- Trimmed to reduce size
- Optimized for release

## Build Settings

The release builds use the following optimizations:
- `PublishSingleFile=true`: Creates a single executable file
- `PublishTrimmed=true`: Removes unused code to reduce size
- `TrimMode=partial`: Balances size reduction with compatibility
- `DebugType=None` and `DebugSymbols=false`: Excludes debug information

## Local Development Builds

For local development and testing, you can use the provided build scripts:

### Unix/Linux/macOS
```bash
./build.sh
```

### Windows
```cmd
build.bat
```

These scripts will:
1. Run tests
2. Build for all platforms
3. Create archives in the `dist/` directory

## CI/CD Pipeline

The repository includes three workflows:

1. **CI** (`ci.yml`): Runs on every push, performs basic build and test validation
2. **Build** (`build.yml`): Runs on pull requests, includes build verification for all platforms
3. **Release** (`release.yml`): Triggered by tags or manual dispatch, creates and publishes releases

## Version Naming

Use semantic versioning for releases:
- `v1.0.0` - Major release
- `v1.1.0` - Minor release with new features
- `v1.0.1` - Patch release with bug fixes
- `v2.0.0-beta.1` - Pre-release versions