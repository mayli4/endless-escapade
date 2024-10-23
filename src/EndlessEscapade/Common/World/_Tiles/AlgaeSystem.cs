using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace EndlessEscapade.Common.World; 

public sealed class AlgaeSystem : ModSystem {

    public override void PostSetupContent() {
        LoadWhitelist();
    }

    public override unsafe void SaveWorldData(TagCompound tag) {
        AlgaeData[] algaeData = Main.tile.GetData<AlgaeData>();
        byte[] data = new byte[algaeData.Length];

        fixed (AlgaeData* dataPtr = algaeData) {
            byte* bytePtr = (byte*)dataPtr;
            Span<byte> algaeDataWrapper = new(bytePtr, algaeData.Length);
            Span<byte> dataCopyWrapper = new(data);

            algaeDataWrapper.CopyTo(dataCopyWrapper);
        }

        tag.Add("algaeTileData", data);
    }

    public override unsafe void LoadWorldData(TagCompound tag) {
        AlgaeData[] algaeWorldDataRef = Main.tile.GetData<AlgaeData>();
        byte[] algaeData = tag.GetByteArray("algaeTileData");

        fixed(AlgaeData* dataPtr = algaeWorldDataRef) {
            byte* bytePtr = (byte*)dataPtr;
            Span<byte> algaeDataWrapper = new(bytePtr, algaeWorldDataRef.Length);
            Span<byte> savedDataWrapper = new(algaeData);

            if(savedDataWrapper.Length == algaeDataWrapper.Length)
                savedDataWrapper.CopyTo(algaeDataWrapper);
        }
    }

    /// <summary>
    /// Anything not in this list cannot have algae grow on it.
    /// </summary>
    public static List<int> WhitelistedTiles = [];

    public void AddToAlgaeWhitelist(params int[] tileTypes) {
        for(int i = 0; i < tileTypes.Length; i++) {
            WhitelistedTiles.Add(i);
        }
    }

    public void AddToAlgaeWhitelist(bool[] set) {
        for(int i = 0; i < set.Length; i++) {
            if(set[i]) {
                AddToAlgaeWhitelist(i);
            }
        }
    }

    public void LoadWhitelist() {
        AddToAlgaeWhitelist(TileID.Sets.Stone);

        AddToAlgaeWhitelist(
            TileID.WoodBlock,
            TileID.AshWood,
            TileID.Shadewood,
            TileID.Pearlwood,
            TileID.BorealWood,
            TileID.LivingWood,
            TileID.DynastyWood,
            TileID.Ebonwood,
            TileID.SpookyWood
        );

        AddToAlgaeWhitelist(
            TileID.Sand,
            TileID.ShellPile
        );
    }
}

public sealed class TileOverlayTesting : ModPlayer {
    public override void PreUpdate() {
        if(Main.keyState.IsKeyDown(Keys.Q)) {
            var tilePos = Main.MouseWorld.ToTileCoordinates();

            var tile = Main.tile[tilePos.X, tilePos.Y];

            ref AlgaeData algaeData = ref tile.Get<AlgaeData>();

            algaeData.HasAlgae = (byte)1;
        }

        if(Main.keyState.IsKeyDown(Keys.F)) {
            var tilePos = Main.MouseWorld.ToTileCoordinates();

            var tile = Main.tile[tilePos.X, tilePos.Y];

            ref AlgaeData algaeData = ref tile.Get<AlgaeData>();

            algaeData.HasAlgae = (byte)0;
        }
    }
}
