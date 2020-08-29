using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using EEMod.Projectiles.Enemy;
using Microsoft.Xna.Framework.Graphics;

namespace EEMod.NPCs.CoralReefs.GlisteningReefs
{
    public class BlueringOctopus : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bluering Octopus");
            //.npcFrameCount[npc.type] = 6;
        }

        /*public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter == 6)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0;
            }
            if (npc.frame.Y >= frameHeight * 6)
            {
                npc.frame.Y = 0;
                return;
            }
        }*/

        public override void SetDefaults()
        {
            npc.aiStyle = -1;

            npc.HitSound = SoundID.NPCHit25;
            npc.DeathSound = SoundID.NPCDeath28;

            npc.alpha = 0;

            npc.lifeMax = 550;
            npc.defense = 10;

            npc.width = 174;
            npc.height = 98;

            npc.noGravity = true;

            npc.spriteDirection = -1;

            npc.lavaImmune = false;
            npc.noTileCollide = false;
            //bannerItem = ModContent.ItemType<Items.Banners.GiantSquidBanner>();
        }

        public override void AI()
        {
            npc.TargetClosest();
            Player target = Main.player[npc.target];
            npc.ai[1]++;
            if(npc.ai[1] >= 60)
            {
                npc.ai[2]++;
                if (npc.ai[2] >= 2)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Normalize(target.Center - npc.Center) * 6, ModContent.ProjectileType<BlueRing>(), npc.damage, 0f);
                    npc.ai[2] = 0;
                }

                if(target.position.Y <= npc.position.Y)
                {
                    npc.velocity.Y -= 16;
                    npc.velocity.X += Helpers.Clamp((target.Center.X - npc.Center.X) / 10, -8, 8);
                }

                npc.ai[1] = 0;
            }
            npc.velocity *= 0.98f;
            npc.velocity.Y += 0.1f;

            npc.rotation = npc.velocity.X / 18;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(mod.GetTexture("NPCs/CoralReefs/GlisteningReefs/BlueringOctopusGlow"), npc.Center - Main.screenPosition + new Vector2(0, -6), npc.frame, Color.White, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[0] == 2)
            {
                AfterImage.DrawAfterimage(spriteBatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(drawColor.R, drawColor.G, drawColor.B, 150));
            }
            return true;
        }*/
    }
}