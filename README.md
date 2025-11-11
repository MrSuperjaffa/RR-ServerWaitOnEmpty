# RR-ServerWaitOnEmpty
Enables Railroader to pause when their are no players connected. The plugin will auto-pause upon map loading until a player connects. When the last player leaves (minus the host), the server will pause.

This plugin also handles disables audio while enables.

The plugin can be disable/enabled at any time.

## Building
Built in Visual Studio 2022

Add `Railroader\Railroader_Data\Managed` to your Reference Paths

### Requires
+ 0Harmony.dll
+ UnityModManager.dll