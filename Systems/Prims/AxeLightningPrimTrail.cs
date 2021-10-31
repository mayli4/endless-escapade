﻿using EEMod.Autoloading;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using EEMod.Extensions;
using System.Linq;
using System;
using EEMod.Effects;
using EEMod.Items.Weapons.Mage;
using static Terraria.ModLoader.ModContent;
using System.Reflection;
using EEMod.NPCs.CoralReefs;

namespace EEMod.Prim
{
    class AxeLightningPrimTrail : Primitive
    {
        public AxeLightningPrimTrail(Projectile projectile, float width = 1, float turnoff = 0.9f) : base(projectile)
        {
            BindableEntity = projectile;
            _width = width;
            _decayRate = turnoff;
        }

        float _decayRate;

        public override void SetDefaults()
        {
            Alpha = 0.7f;
            _cap = 80;

            behindTiles = false;
            ManualDraw = false;
        }

        public override void PrimStructure(SpriteBatch spriteBatch)
        {
            /*if (_noOfPoints <= 1) return; //for easier, but less customizable, drawing
            float colorSin = (float)Math.Sin(_counter / 3f);
            Color c1 = Color.Lerp(Color.White, Color.Cyan, colorSin);
            float widthVar = (float)Math.Sqrt(_points.Count) * _width;
            DrawBasicTrail(c1, widthVar);*/

            if (_noOfPoints <= 1) return;
            float widthVar;

            float colorSin = (float)Math.Sin(_counter / 3f);
            {
                widthVar = (float)Math.Sqrt(_points.Count) * _width;
                Color c1 = Color.Lerp(Color.White, Color.Gold, colorSin);

                Vector2 normalAhead = CurveNormal(_points, 1);
                Vector2 secondUp = _points[1] - normalAhead * widthVar;
                Vector2 secondDown = _points[1] + normalAhead * widthVar;
                Vector2 v = new Vector2((float)Math.Sin(_counter / 20f));

                AddVertex(_points[0], c1 * Alpha, v);
                AddVertex(secondUp, c1 * Alpha, v);
                AddVertex(secondDown, c1 * Alpha, v);
            }

            for (int i = 1; i < _points.Count - 1; i++)
            {
                widthVar = (float)Math.Sqrt(_points.Count - i) * _width;

                Color base1 = new Color(7, 86, 122);
                Color base2 = new Color(255, 244, 173);
                Color c = Color.Lerp(Color.White, Color.Gold, colorSin);
                Color CBT = Color.Lerp(Color.White, Color.Gold, colorSin);

                Vector2 normal = CurveNormal(_points, i);
                Vector2 normalAhead = CurveNormal(_points, i + 1);

                float j = (_cap + ((float)(Math.Sin(_counter / 10f)) * 1) - i * 0.1f) / _cap;
                widthVar *= j;

                Vector2 firstUp = _points[i] - normal * widthVar;
                Vector2 firstDown = _points[i] + normal * widthVar;
                Vector2 secondUp = _points[i + 1] - normalAhead * widthVar;
                Vector2 secondDown = _points[i + 1] + normalAhead * widthVar;

                AddVertex(firstDown, c * Alpha, new Vector2((i / _cap), 1));
                AddVertex(firstUp, c * Alpha, new Vector2((i / _cap), 0));
                AddVertex(secondDown, CBT * Alpha, new Vector2((i + 1) / _cap, 1));

                AddVertex(secondUp, CBT * Alpha, new Vector2((i + 1) / _cap, 0));
                AddVertex(secondDown, CBT * Alpha, new Vector2((i + 1) / _cap, 1));
                AddVertex(firstUp, c * Alpha, new Vector2((i / _cap), 0));
            }
        }


        public override void SetShaders()
        {
            int width = _device.Viewport.Width;
            int height = _device.Viewport.Height;

            Vector2 zoom = Main.GameViewMatrix.Zoom;
            Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(width / 2, height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi) * Matrix.CreateScale(zoom.X, zoom.Y, 1f);
            Matrix projection = Matrix.CreateOrthographic(width, height, 0, 1000);

            //EEMod.lightningShader.View = view;
            //EEMod.lightningShader.Projection = projection;

            //PrepareShader(EEMod.lightningShader);

            Main.spriteBatch.End(); Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);

            EEMod.LightningShader.Parameters["maskTexture"].SetValue(ModContent.GetInstance<EEMod>().Assets.Request<Texture2D>("Textures/GlowingWeb").Value);
            EEMod.LightningShader.Parameters["newColor"].SetValue(new Vector4(Color.Gold.R, Color.Gold.G, Color.Gold.B, Color.Gold.A) / 255f);
            EEMod.LightningShader.CurrentTechnique.Passes[0].Apply();
        }

        public override void OnUpdate()
        {
            _counter++;
            _noOfPoints = _points.Count() * 6;
            if (_cap < _noOfPoints / 6)
            {
                _points.RemoveAt(0);
            }
            if ((!BindableEntity.active && BindableEntity != null) || _destroyed)
            {
                OnDestroy();
            }
            else
            {
                _points.Add(BindableEntity.Center);
            }
        }

        public override void OnDestroy()
        {
            _destroyed = true;
            _width *= _decayRate;
            if (_width < 0.05f)
            {
                Dispose();
            }
        }

        public override void PostDraw()
        {
            Main.spriteBatch.End(); Main.spriteBatch.Begin();
        }
    }
}