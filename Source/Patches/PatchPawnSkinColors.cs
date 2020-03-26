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
        static float Postfix(float value)
        {
            int v = Mathf.RoundToInt(value * 100);
            IntRange range = WhiteOnly.settings.melaninRange;

            if (v < range.min || v > range.max) value = range.RandomInRange * 0.01f;

            return value;
        }
    }
}
