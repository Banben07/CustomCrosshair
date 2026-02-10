# CrossfireCrosshair

Windows desktop crosshair overlay for FPS games that do not offer custom crosshair settings.

## Features

- Transparent top-most crosshair overlay (click-through, no focus steal)
- Profile system (add, duplicate, delete, reset defaults)
- Presets inspired by common `CS` and `VALORANT` style setups
- Real-time preview and live update
- Extensive customization:
  - line length, line thickness, gap
  - center dot and dot size
  - T-style
  - outline and outline thickness
  - opacity
  - screen offsets
  - dynamic spread value
  - hex color + color picker
- Global hotkeys:
  - toggle overlay
  - cycle profile
- JSON settings persistence at:
  - `%APPDATA%\CrossfireCrosshair\settings.json`

## Build

Requirements:

- Windows 10/11
- .NET 8 SDK

Build and run:

```powershell
dotnet build
dotnet run
```

## Security and Compliance Notes

- This app only draws a desktop overlay and listens for global hotkeys.
- It does **not** inject into game processes.
- It does **not** read game memory.
- It does **not** install kernel drivers.
- It does **not** require administrator privileges.

No tool can guarantee anti-cheat acceptance in every game. Always follow the game's terms and anti-cheat policy.

## Reducing False Positives

- Build from source yourself.
- Keep dependencies minimal.
- Avoid obfuscators/packers.
- Sign release binaries with a trusted code-signing certificate.
- Publish checksums and release notes for each build.
