using HarmonyLib;
using System;
using System.Reflection;
using Verse;

namespace WhiteOnly
{
    [StaticConstructorOnStartup]
    public class WhiteOnly
    {
        public static FloatRange colorRange = new FloatRange(0f, 0.5f);

        static WhiteOnly()
        {
            var harmony = new Harmony("com.github.automatic1111.whiteonly");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Type ColorGenerator_SkinColorMelanin = GenTypes.GetTypeInAnyAssembly("AlienRace.ColorGenerator_SkinColorMelanin");
            try
            {
                if (ColorGenerator_SkinColorMelanin != null)
                    harmony.Patch(AccessTools.Method(ColorGenerator_SkinColorMelanin, "NewRandomizedColor"), new HarmonyMethod(typeof(PatchColorGenerator_SkinColorMelanin), "Prefix"));
            }
            catch (Exception e)
            {
                Log.Error("Failed to patch AlienRace.ColorGenerator_SkinColorMelanin: " + e.ToString());
            }
        }
    }
}
