using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace WhiteOnly
{
    class PatchColorGenerator_SkinColorMelanin
    {
        static bool Prefix(ref Color __result)
        {
            __result = PawnSkinColors.GetSkinColor(WhiteOnly.settings.melaninRange.RandomInRange * 0.01f);
            return false;
        }
    }
}
