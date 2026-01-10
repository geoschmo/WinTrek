# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

```bash
# Build entire solution
dotnet build

# Run tests
dotnet test

# Build specific project
dotnet build src/WinTrek.Core/WinTrek.Core.csproj
```

## Project Overview

WinTrek is a Star Trek-themed space exploration game being modernized from a legacy WinForms application to a clean WPF application with proper separation of concerns.

## Solution Structure

```
WinTrek/
├── WinTrek.sln                    # Main solution file (.NET 8)
├── src/
│   └── WinTrek.Core/              # Game engine library (no UI dependencies)
│       ├── Models/                # Domain entities
│       ├── Services/              # Game logic services (TBD)
│       ├── Events/                # Game events (TBD)
│       ├── Enums/                 # SectorContent, AlertCondition, ShipSystem
│       └── GameData.cs            # Static data (quadrant names, ASCII art)
├── tests/
│   └── WinTrek.Core.Tests/        # xUnit tests for game logic
└── LegacyCode/                    # Original WinForms app (.NET Framework 4.0)
```

## Architecture

### WinTrek.Core Models

| Class | Purpose |
|-------|---------|
| `Position` | X,Y coordinate record with distance calculation |
| `Galaxy` | 8x8 grid of Quadrants, tracks total Klingons/starbases |
| `Quadrant` | Named region containing 8x8 sector grid |
| `Enterprise` | Player ship: energy, shields, torpedoes, damage state |
| `KlingonShip` | Enemy ship with position and shield level |
| `DamageState` | Tracks damage to 7 ship systems |

### Enums

| Enum | Values |
|------|--------|
| `SectorContent` | Empty, Star, Klingon, Enterprise, Starbase |
| `AlertCondition` | Green, Yellow, Red |
| `ShipSystem` | WarpEngines, ShortRangeScanner, LongRangeScanner, ShieldControl, Computer, PhotonTorpedo, Phaser |

### Game Systems (being migrated from LegacyCode/Form1.cs)
- **Navigation**: Warp drive movement between sectors/quadrants
- **Sensors**: Short-range and long-range scanning
- **Weapons**: Phasers and photon torpedoes
- **Shields**: Energy management for defense
- **Damage**: Random equipment malfunctions and repair at starbases
- **Computer**: Status reports and targeting calculators

## Legacy Code Reference

The `LegacyCode/` folder contains the original WinForms implementation:
- `Form1.cs` - All game logic (~1400 lines) being extracted into WinTrek.Core services
- `Form1.Designer.cs` - UI layout reference for WPF implementation
