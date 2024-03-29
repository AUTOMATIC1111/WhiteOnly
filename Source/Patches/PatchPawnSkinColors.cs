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

    [HarmonyPatch(typeof(PawnSkinColors), "GetSkinColorGene", new Type[] { typeof(float) })]
    class PatchPawnSkinColorsGetSkinColorGene
    {
        static public bool skip = false;

        static bool Prefix(ref float melanin)
        {
            if (skip) {
                return true;
            }

            int v = Mathf.RoundToInt(melanin * 100);
            IntRange range = WhiteOnly.settings.melaninRange;

            if (v < range.min || v > range.max)
            {
                melanin = range.RandomInRange * 0.01f;
            }

            return true;
        }
    }
}
