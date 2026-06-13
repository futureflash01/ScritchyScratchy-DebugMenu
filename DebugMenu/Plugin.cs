using BepInEx;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using Il2CppInterop.Runtime.Injection; 

namespace ScritchyScratchyDebugMenu
{
    [BepInPlugin("futureflash.scritchyscratchy.debugmenu", "ScritchyScratchy Debug Menu", "1.0.0")]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            Log.LogInfo("ScritchyScratchy Debug Menu loaded - initializing...");

            // Register custom class (IL2CPP)
            ClassInjector.RegisterTypeInIl2Cpp<ToggleBehaviour>();

            // Create the GameObject
            var pluginObject = new GameObject("ScritchyScratchyDebugToggle_Object");
            Object.DontDestroyOnLoad(pluginObject);
            pluginObject.hideFlags = HideFlags.HideAndDontSave;

            // Attach the script
            pluginObject.AddComponent<ToggleBehaviour>();

            Log.LogInfo("ToggleBehaviour successfully attached!");
        }
    }

    public class ToggleBehaviour : MonoBehaviour
    {
        // Cache the object to only search for it once
        private GameObject _cachedDebugMenu = null;

        public ToggleBehaviour(System.IntPtr handle) : base(handle) { }

        private void Update()
        {
            bool f9Pressed = false;

            // New Input System
            try 
            { 
                if (UnityEngine.InputSystem.Keyboard.current != null && 
                    UnityEngine.InputSystem.Keyboard.current.f9Key.wasPressedThisFrame) 
                { f9Pressed = true; }
            } catch { }

            // Old Input System
            try 
            { 
                if (UnityEngine.Input.GetKeyDown(KeyCode.F9)) f9Pressed = true; 
            } catch { }

            if (f9Pressed)
            {
                try
                {
                    // If menu is not found, try to find it manually
                    if (_cachedDebugMenu == null)
                    {
                        System.Console.WriteLine("[ScritchyMod] F9 Pressed: Searching for menu without string arguments...");
                        _cachedDebugMenu = FindDebugMenuManually();
                    }

                    // If menu is found or already cached, toggle it
                    if (_cachedDebugMenu != null)
                    {
                        bool newState = !_cachedDebugMenu.activeSelf;
                        _cachedDebugMenu.SetActive(newState);
                        System.Console.WriteLine($"[ScritchyMod] SUCCESS! Menu toggled to: {newState}");
                    }
                    else
                    {
                        System.Console.WriteLine("[ScritchyMod] ERROR: Could not find the Debug Buttons Group in the scene.");
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"[ScritchyMod] ERROR: {ex.Message}");
                }
            }
        }

        private GameObject FindDebugMenuManually()
        {
            // Grab EVERY object in the game. I know it sounds excessive but that's the only way I found to be working. Read below:
			// It turns out, the Debug Menu is a single GameObject but it's made up of individual labels, buttons, and values rather than one object that can be turned on/off
            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject debugToolsRoot = null;

            foreach (var obj in allObjects)
            {
                if (obj.name == "Debug Tools")
                {
                    debugToolsRoot = obj;
                    break;
                }
            }

            if (debugToolsRoot != null)
            {
                var rootTransform = debugToolsRoot.transform;

                // Loop through children to find 'Canvas'. This is the parent of the main 'Debug Tools' GameObject
                for (int i = 0; i < rootTransform.childCount; i++)
                {
                    var child = rootTransform.GetChild(i);
                    if (child.name == "Canvas")
                    {
                        // Loop through Canvas children to find the Debug Buttons, labels, and values
                        for (int j = 0; j < child.childCount; j++)
                        {
                            var grandchild = child.GetChild(j);
                            if (grandchild.name == "Debug Buttons Group")
                            {
                                return grandchild.gameObject;
                            }
                        }
                    }
                }
            }

            // Return null if menu couldn't be found
			return null;
        }
    }
}