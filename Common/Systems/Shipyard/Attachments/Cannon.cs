﻿using Terraria.DataStructures;

namespace EndlessEscapade.Common.Systems.Shipyard.Attachments;

public class Cannon : TileAttachment
{
    public Cannon(int type) : base(type) { }

    public override Point16 Offset => new(31, 25);
}
