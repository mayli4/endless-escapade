using EndlessEscapade.Utilities;
using ReLogic.Content;
using Terraria.DataStructures;

namespace EndlessEscapade.Common.World;

public sealed class AlgaeGlobalTile : GlobalTile {

    private Asset<Texture2D>[]? _algaeTextures;

    public override void Load() {
        _algaeTextures = [
            ModContent.Request<Texture2D>("EndlessEscapade/Assets/Textures/Extras/AlgaeOverlay"),
            ModContent.Request<Texture2D>("EndlessEscapade/Assets/Textures/Extras/AlgaeOverlay_Sloped")
        ];
    }

    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch) {

        var tile = Main.tile[i, j];
        ref AlgaeData algaeData = ref tile.Get<AlgaeData>();

        if(algaeData.HasAlgae == 0 || AlgaeSystem.WhitelistedTiles.Contains(tile.TileType))
            return;

        var position = new Point16(((i * 16) - (int)Main.screenPosition.X) + Main.offScreenRange, ((j * 16) - (int)Main.screenPosition.Y) + Main.offScreenRange);
        var drawRect = new Rectangle(position.X, position.Y, 16, 16);

        var blockType = tile.BlockType;
        int currentTextureIndex; //0 = non sloped, 1 = sloped / half

        Rectangle sourceRect;

        if(blockType >= BlockType.HalfBlock) {  
            currentTextureIndex = 1;
            sourceRect = new Rectangle((int)MathUtils.Modulo((tile.TileFrameX / 18f) * 16f, 48f), ((int)blockType - 1) * 16, 16, 16);
        }
        else {
            currentTextureIndex = 0;
            sourceRect = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        }

        var currentTex = _algaeTextures[currentTextureIndex];

        spriteBatch.Draw(currentTex.Value, drawRect, sourceRect, Lighting.GetColor(i, j));
    }
}