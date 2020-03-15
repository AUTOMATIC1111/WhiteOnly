using HarmonyLib;
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
    [HarmonyPatch(typeof(PawnSkinColors), "RandomMelanin", new Type[] { typeof(Faction) })]
    class PatchPawnSkinColorsRandomMelanin
    {
        static bool Prefix(ref float __result)
        {
            __result = WhiteOnly.colorRange.RandomInRange;
            return false;
        }
    }
}
