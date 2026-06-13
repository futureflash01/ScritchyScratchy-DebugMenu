# Debug Menu Enabler - Scritchy Scratchy
A lightweight BepInEx plugin for *Scritchy Scratchy* that restores the ability to toggle the hidden Debug Menu using a simple hotkey. 

## 🎮 Features
* **Debug Menu (`F9`):** Press `F9` in-game after properly loading your save file to open the Debug Menu, which grants access to a bunch of internal testing tools used by the developers
* **Seamless Integration:** Runs non-destructively in the background and safely waits until you fully load into the game and opening a save file before attaching to the user interface.

## 🛠️ Requirements
* [BepInEx 6.0.0+ (IL2CPP)](https://builds.bepinex.dev/projects/bepinex_be)
* Scritchy Scratchy ([Steam](https://store.steampowered.com/app/3948120/Scritchy_Scratchy/))

## 📥 Installation

**For Players:**
1. Ensure you have BepInEx installed for Scritchy Scratchy.
2. Download the latest `ScritchyScratchy-DebugMenu.zip` from the [Releases](../../releases) tab.
3. Extract the ZIP file directly into your main Scritchy Scratchy game directory. The folders will automatically merge and place the plugin in the correct location. Or you can manually extract the DLL file from the `BepInEx/plugins` folder in the ZIP file and manually place it in the game's `BepInEx/plugins` folder
4. Launch the game, load your save, and press `F9`!

## 💻 Build Instructions

If you would like to compile this plugin from scratch, follow these steps:

1. Download and install the [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).
2. Create a new project folder on your machine and open a Terminal or PowerShell window inside that directory
3. Install the BepInEx templates by running:
   `dotnet new install BepInEx.Templates@2.0.0-be.4 --nuget-source https://nuget.bepinex.dev/v3/index.json`
4. Generate the base plugin structure by running:
   `dotnet new bep6plugin_unity_il2cpp -n DebugMenu`
5. Navigate into the newly created `DebugMenu` folder. Open the `DebugMenu.csproj` file in a text editor and replace its entire contents with the `.csproj` file provided in this repository
6. **Important:** Edit the `<GameDir>` element within the `.csproj` file to match the exact installation path of your game
7. Replace the generated `Plugin.cs` file with the version from this repository
8. Clean the build environment by running:
   `dotnet clean`
9. Compile the plugin by running:
   `dotnet build`
   *(Note: The initial build will take some time as it interops the IL2CPP game assemblies).*
10. Once finished, navigate to `bin/Debug/net6.0/` inside your project folder to find your compiled `DebugMenu.dll`
11. Copy this `.dll` into your game's `BepInEx/plugins/` directory. You have successfully built the plugin from scratch!

## 📄 License
This project is open-source and available under the MIT License.
