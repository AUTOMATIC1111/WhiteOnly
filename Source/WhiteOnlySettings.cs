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
    public class WhiteOnlySettings : ModSettings
    {
        public IntRange melaninRange = new IntRange(0, 50);

        override public void ExposeData()
        {
            Scribe_Values.Look(ref melaninRange, "melaninRange");
        }

        public void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            listing_Standard.Label("WhiteOnlyMelaninName".Translate(), -1, "WhiteOnlyMelaninDesc".Translate());
            listing_Standard.SkincolorWidget(ref melaninRange, inRect);
            listing_Standard.End();

        }
    }
}
