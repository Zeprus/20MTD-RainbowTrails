using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;

using HarmonyLib;

namespace RainbowTrails
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class RainbowTrails : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        public static ConfigEntry<float> trailLength;

        private void Awake()
        {
            // Plugin startup logic

            Log = base.Logger;

            //Bind Config
            trailLength = Config.Bind("General", "Trail length", 0.1f, "Changes the length of the projectile trail. Game default = 0.1");

            //Apply method patches through Harmony
            try
            {
                Harmony.CreateAndPatchAll(typeof(Patch.ProjectileFactory_Patch));
            }
            catch
            {
                Logger.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods.");
            }

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void OnDestory()
        {
            Harmony.UnpatchAll();
        }
    }
}
