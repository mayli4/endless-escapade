using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.IO;

namespace EndlessEscapade.Common.World; 

public sealed class AlgaeSystem : ModSystem {



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

        // Safely get blossom binary tile data from the tag and apply it to the world.
        fixed(AlgaeData* dataPtr = algaeWorldDataRef) {
            byte* bytePtr = (byte*)dataPtr;
            Span<byte> algaeDataWrapper = new(bytePtr, algaeWorldDataRef.Length);
            Span<byte> savedDataWrapper = new(algaeData);

            if(savedDataWrapper.Length == algaeDataWrapper.Length)
                savedDataWrapper.CopyTo(algaeDataWrapper);
        }
    }
}
