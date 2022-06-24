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
    public class PawnGraphicRenderer
    {
        public float melanin;

        Graphic headGraphic;
        Graphic hairGraphic;

        public PawnGraphicRenderer(float melanin)
        {
            Color skinColor = PawnSkinColors.GetSkinColor(this.melanin = melanin);
            Gender gender = (Rand.Value < 0.5f) ? Gender.Male : Gender.Female;
            CrownType crownType = (Rand.Value < 0.5f) ? CrownType.Average : CrownType.Narrow;
            HairDef hairDef = RandomHairDefFor(gender);
            Color hairColor = PawnHairColors.RandomHairColor(skinColor, 18);
            headGraphic = GraphicDatabaseHeadRecords.GetHeadRandom(gender, skinColor, crownType, false);
            hairGraphic = GraphicDatabase.Get<Graphic_Multi>(hairDef.texPath, ShaderDatabase.Cutout, Vector2.one, hairColor);
        }

        public void Draw(Rect rect)
        {
            Material mat;

            GUI.color = Color.white;

            mat = headGraphic.MatSouth;
            Graphics.DrawTexture(rect, mat.mainTexture, mat, 0);

            mat = hairGraphic.MatSouth;
            Graphics.DrawTexture(rect, mat.mainTexture, mat, 0);
        }
        public static HairDef RandomHairDefFor(Gender gender)
        {
            return (from hair in DefDatabase<HairDef>.AllDefs where GenderMatches(gender, hair.styleGender) select hair).RandomElement();
        }

        public static bool GenderMatches(Gender gender, StyleGender styleGender) {
            if (styleGender == StyleGender.Any) return true;
            if (gender == Gender.Male && (styleGender == StyleGender.Male || styleGender == StyleGender.MaleUsually)) return true;
            if (gender == Gender.Female && (styleGender == StyleGender.Female || styleGender == StyleGender.FemaleUsually)) return true;

            return false;
        }
    }
}
