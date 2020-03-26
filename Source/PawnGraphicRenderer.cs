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

            headGraphic = GraphicDatabaseHeadRecords.GetHeadRandom(gender, skinColor, crownType);
            hairGraphic = GraphicDatabase.Get<Graphic_Multi>(hairDef.texPath, ShaderDatabase.Cutout, Vector2.one, hairColor);
        }

        public void Draw(Rect rect)
        {
            Material mat;
            
            mat = headGraphic.MatSouth;
            Graphics.DrawTexture(rect, mat.mainTexture, mat, 0);

            mat = hairGraphic.MatSouth;
            Graphics.DrawTexture(rect, mat.mainTexture, mat, 0);
        }
        public static HairDef RandomHairDefFor(Gender gender)
        {
            return (from hair in DefDatabase<HairDef>.AllDefs where GenderMatches(gender, hair.hairGender) select hair).RandomElement();
        }

        public static bool GenderMatches(Gender gender, HairGender hairGender) {
            if (hairGender == HairGender.Any) return true;
            if (gender == Gender.Male && (hairGender == HairGender.Male || hairGender == HairGender.MaleUsually)) return true;
            if (gender == Gender.Female && (hairGender == HairGender.Female || hairGender == HairGender.FemaleUsually)) return true;

            return false;
        }
    }
}
