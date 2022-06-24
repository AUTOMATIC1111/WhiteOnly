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

        static Rect sourceRect = new Rect(0f, 0f, 1f, 1f);

        public void Draw(Rect rect)
        {
            Material mat;

            GUI.color = Color.white;

            // there are other DrawTexture calls with less params but for whatever reason they are using 
            // Color32(128, 128, 128, 128) as color, which is extremely fucked up and shows a half-transparent
            // texture instead of intended normal looking texture, so we're using this.
            mat = headGraphic.MatSouth;
            Graphics.DrawTexture(rect, mat.mainTexture, sourceRect, 0, 0, 0, 0, Color.white, mat, 0);

            mat = hairGraphic.MatSouth;
            Graphics.DrawTexture(rect, mat.mainTexture, sourceRect, 0, 0, 0, 0, Color.white, mat, 0);
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
