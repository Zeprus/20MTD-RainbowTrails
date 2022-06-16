using BepInEx;
using BepInEx.Logging;

using HarmonyLib;

namespace RainbowTrails
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class RainbowTrails : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        public RainbowTrails()
        {
            Log = base.Logger;
            try
            {
                Harmony.CreateAndPatchAll(typeof(Patch.ProjectileFactory_Patch));
            }
            catch
            {
                Log.LogWarning("Harmony failed to patch RainbowTrails");
            }
        }
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

        }
    }
}
