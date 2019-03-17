using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using Verse;

namespace WhiteOnly
{
    [HarmonyPatch(typeof(PawnSkinColors), "RandomMelanin", new Type[] { typeof(Faction) }), StaticConstructorOnStartup]
    class PatchPawnSkinColors
    {
        static FloatRange range = new FloatRange(0f, 0.5f);

        static bool Prefix(ref float __result)
        {
            __result = range.RandomInRange;
            return false;
        }
    }
}
