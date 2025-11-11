# RR-ServerWaitOnEmpty
Enables Railroader to pause when their are no players connected. The plugin will auto-pause upon map loading until a player connects. When the last player leaves (minus the host), the server will pause.

This plugin also disables audio while enabled.

The plugin can be enabled/disabled at any time.

## Installation
Install via Unity Mod Manager.

## Building
Built in Visual Studio 2022 using .NET Framework 4.8

Add `Railroader\Railroader_Data\Managed` to your Reference Paths.

### Requires
+ 0Harmony.dll
+ UnityModManager.dll