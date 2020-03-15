using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace WhiteOnly
{
    [StaticConstructorOnStartup]
    public class WhiteOnly
    {
        public static FloatRange colorRange = new FloatRange(0f, 0.5f);

        static void applyOptionalPatch(Harmony harmony, string className, string methodName, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
        {
            Type t = GenTypes.GetTypeInAnyAssembly(className);
            try
            {
                if (t != null)
                    harmony.Patch(AccessTools.Method(t, methodName), prefix, postfix);
            }
            catch (Exception e)
            {
                Log.Error("Failed to patch " + className + "." + methodName + ": " + e.ToString());
            }
        }

        static WhiteOnly()
        {
            var harmony = new Harmony("com.github.automatic1111.whiteonly");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            if (GenTypes.GetTypeInAnyAssembly("AlienRace.HarmonyPatches") != null)
            {
                applyOptionalPatch(harmony, "AlienRace.ColorGenerator_SkinColorMelanin", "NewRandomizedColor", new HarmonyMethod(typeof(PatchColorGenerator_SkinColorMelanin), "Prefix"));
                applyOptionalPatch(harmony, "Verse.PawnGraphicSet", "ResolveApparelGraphics", new HarmonyMethod(typeof(PatchPawnGraphicSetResolveApparelGraphics), "Prefix"));
            }
        }
    }
}
