using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace WhiteOnly
{
    public class WhiteOnly : Mod
    {
        public static WhiteOnlySettings settings;

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

        public WhiteOnly(ModContentPack pack) :base(pack)
        {
            var harmony = new Harmony("com.github.automatic1111.whiteonly");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            settings = GetSettings<WhiteOnlySettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            settings.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "WhiteOnlyTitle".Translate();
        }
    }
}
