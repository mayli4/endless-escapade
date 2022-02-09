using EEMod.Items.Materials;
using EEMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using EEMod.Items.Placeables.Furniture;
using EEMod.EEWorld;
using EEMod.UI.States;
using EEMod.Systems.Subworlds.EESubworlds;
using EEMod.Items.Shipyard.Figureheads;

namespace EEMod.Tiles.Furniture.Shipyard
{
    public class FigureheadTile : EETile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileSolidTop[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);

            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 10;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = false;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, 4, 0);
            //TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide, 10, 0);
            TileObjectData.newTile.AnchorBottom = AnchorData.Empty;

            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Figurehead");
            AddMapEntry(new Color(255, 168, 28), name);
            DustType = DustID.Silver;
            DisableSmartCursor = true;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //if(Main.LocalPlayer.GetModPlayer<EEPlayer>().FigureheadType == ModContent.ItemType<WoodenFigurehead>())
                Framing.GetTileSafely(i, j).TileFrameY = (short)(0 + (short)(Framing.GetTileSafely(i, j).TileFrameY % 90));
            //if (Main.LocalPlayer.GetModPlayer<EEPlayer>().FigureheadType == ModContent.ItemType<TreasureFigurehead>())
                //Framing.GetTileSafely(i, j).TileFrameY = (short)(90 + (short)(Framing.GetTileSafely(i, j).TileFrameY % 90));

            Texture2D figurehead = ModContent.Request<Texture2D>("EEMod/Tiles/Furniture/Shipyard/FigureheadTile").Value;

            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            spriteBatch.Draw(figurehead, new Vector2(i * 16, j * 16) - Main.screenPosition + zero,
                new Rectangle(Framing.GetTileSafely(i, j).TileFrameX, Framing.GetTileSafely(i, j).TileFrameY + 18, 16, 16),
                Lighting.GetColor(i, j));

            return false;
        }

        public override void KillMultiTile(int i, int j, int TileFrameX, int TileFrameY)
        {
            Item.NewItem(i * 16, j * 16, 64, 180, TileID.Dirt);
        }
    }

    public class FigureheadRender : GlobalTile
    {
        public override bool PreDraw(int i, int j, int type, SpriteBatch spriteBatch)
        {
            if (Framing.GetTileSafely(i, j + 1).TileType == ModContent.TileType<FigureheadTile>() && type != ModContent.TileType<FigureheadTile>())
            {
                switch (Framing.GetTileSafely(i, j + 1).TileFrameY)
                {
                    case 0:
                        Texture2D figurehead = ModContent.Request<Texture2D>("EEMod/Tiles/Furniture/Shipyard/FigureheadTile").Value;

                        Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                        if (Main.drawToScreen)
                        {
                            zero = Vector2.Zero;
                        }

                        spriteBatch.Draw(figurehead, new Vector2(i * 16, j * 16) - Main.screenPosition + zero,
                            new Rectangle(Framing.GetTileSafely(i, j + 1).TileFrameX, Framing.GetTileSafely(i, j + 1).TileFrameY, 16, 16),
                            Lighting.GetColor(i, j));

                        return false;

                        break;
                    default:
                        return true;

                        break;
                }
            }
            else
            {
                return base.PreDraw(i, j, type, spriteBatch);
            }
        }
    }
}