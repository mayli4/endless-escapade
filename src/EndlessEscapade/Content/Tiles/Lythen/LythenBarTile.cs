﻿using Terraria.Audio;
using Terraria.Localization;
using Terraria.ObjectData;

namespace EndlessEscapade.Content.Tiles.Lythen;

public class LythenBarTile : ModTile
{
    public static readonly SoundStyle LythenHitSound = new($"{nameof(EndlessEscapade)}/Assets/Sounds/Custom/LythenHit", 3) {
        Pitch = 0.25f,
        PitchVariance = 0.25f
    };

    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        Main.tileSolid[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileFrameImportant[Type] = true;

        Main.tileShine[Type] = 1100;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
        TileObjectData.newTile.StyleHorizontal = true;
        TileObjectData.newTile.LavaDeath = false;
        TileObjectData.addTile(Type);

        AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar"));

        MineResist = 1f;
        MinPick = 30;
        HitSound = LythenHitSound;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) {
        base.NumDust(i, j, fail, ref num);

        num = fail ? 1 : 3;
    }
}
