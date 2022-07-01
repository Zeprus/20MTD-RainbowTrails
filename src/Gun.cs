using HarmonyLib;
using flanne;
using UnityEngine;

namespace RainbowTrails.Patch
{
    internal static class GunPatch
    {
        static Color startColor = new Color(148, 0, 211);
        static Color endColor = new Color(255, 0, 0);
        static GradientColorKey[] colorKeys = new GradientColorKey[7] {
            new GradientColorKey(new Color(148, 0, 211), 0.0f),
            new GradientColorKey(new Color(75, 0, 130), 1/6f),
            new GradientColorKey(new Color(0, 0, 255), 2/6f),
            new GradientColorKey(new Color(0, 255, 0), 3/6f),
            new GradientColorKey(new Color(255, 255, 0), 4/6f),
            new GradientColorKey(new Color(255, 127, 0), 5/6f),
            new GradientColorKey(new Color(255, 0 , 0), 1.0f),
        };
        static GradientAlphaKey[] alphaKeys = new GradientAlphaKey[7]{
            new GradientAlphaKey(1.0f, 0f),
            new GradientAlphaKey(1.0f, 1/6f),
            new GradientAlphaKey(1.0f, 2/6f),
            new GradientAlphaKey(1.0f, 3/6f),
            new GradientAlphaKey(1.0f, 4/6f),
            new GradientAlphaKey(1.0f, 5/6f),
            new GradientAlphaKey(1.0f, 1f),
        };

        [HarmonyPatch(typeof(Gun), "LoadGun")]
        [HarmonyPrefix]
        static bool LoadGunPrefix(GunData gunToLoad)
        {
            TrailRenderer trailRenderer = gunToLoad.bullet.GetComponent<TrailRenderer>();

            if (trailRenderer != null) //flame cannon has no trailRenderer
            {
                //Patch TrailRenderer
                trailRenderer.startColor = GunPatch.startColor;
                trailRenderer.endColor = GunPatch.endColor;
                trailRenderer.time = RainbowTrails.trailLength.Value;
                Gradient gradient = new Gradient();
                gradient.SetKeys(GunPatch.colorKeys, GunPatch.alphaKeys);
                gradient.mode = GradientMode.Fixed;
                trailRenderer.colorGradient = gradient;
            }
            return true;
        }
    }
}
