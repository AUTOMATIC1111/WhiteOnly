using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace WhiteOnly
{
    [StaticConstructorOnStartup]
    static class SkincolorWidgetUtil
    {

        static float pawnSize = 64f;
        static List<PawnGraphicRenderer> pawns = new List<PawnGraphicRenderer>();
        static Texture2D deleteX = ContentFinder<Texture2D>.Get("UI/Buttons/Delete", true);

        static SkincolorWidgetUtil()
        {
            int total = 24;
            for (int i = 0; i < total; i++)
            {
                pawns.Add(new PawnGraphicRenderer(1f * i / (total - 1)));
            }
        }

        public static void SkincolorWidget(this Listing_Standard listing_Standard, ref IntRange range, Rect inRect)
        {
            listing_Standard.IntRange(ref range, 0, 100);

            Rect rect = listing_Standard.GetRect(pawnSize);
            float lineWidth = inRect.width - pawnSize * 0.4f;
            rect.width = rect.height;

            bool allow = false;
            bool allDone = false;
            foreach (PawnGraphicRenderer pawn in pawns)
            {
                float v = pawn.melanin;
                rect.x = -pawnSize * 0.3f + v * lineWidth;
                pawn.Draw(rect);

                if (!allow && !allDone && v * 100 >= range.min) allow = true;
                else if (allow && v * 100 > range.max) { allow = false; allDone = true; };

                if (!allow) Graphics.DrawTexture(rect.ContractedBy(12), deleteX);
            }
        }
    }
}
