using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace WhiteOnly
{
    class PatchPawnGraphicSetResolveApparelGraphics
    {
        static Graphic fix(Graphic graphic)
        { 
            return GraphicDatabase.Get<Graphic_Multi>(graphic.path, ShaderDatabase.CutoutSkin, Vector2.one, graphic.color, graphic.colorTwo);
        }

        static bool Prefix(ref PawnGraphicSet __instance)
        {
            Pawn pawn = __instance.pawn;
            if(pawn.def.defName != "Human") return true;

            __instance.nakedGraphic = fix(__instance.nakedGraphic);
            __instance.rottingGraphic = fix(__instance.rottingGraphic);
            __instance.headGraphic = fix(__instance.headGraphic);
            __instance.headStumpGraphic = fix(__instance.headStumpGraphic);

            return true;
        }
    }
}
