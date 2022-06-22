using HarmonyLib;

using flanne;
using UnityEngine;

namespace RainbowTrails.Patch
{
    class ProjectileFactory_Patch
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



        [HarmonyPatch(typeof(ProjectileFactory), "SpawnProjectile")]
        [HarmonyPostfix]
        static void ProjectileTrailPostfix(ref Projectile __result)
        {
            //get protected property Projectile TrailRenderer with no getter
            TrailRenderer trailRenderer = (TrailRenderer)Traverse.Create(__result).Field("trail").GetValue();

            //Patch TrailRenderer
            trailRenderer.startColor = ProjectileFactory_Patch.startColor;
            trailRenderer.endColor = ProjectileFactory_Patch.endColor;
            trailRenderer.time = RainbowTrails.trailLength.Value;
            Gradient gradient = new Gradient();
            gradient.SetKeys(ProjectileFactory_Patch.colorKeys, ProjectileFactory_Patch.alphaKeys);
            gradient.mode = GradientMode.Fixed;
            trailRenderer.SetColorGradient(gradient);
        }
    }
}