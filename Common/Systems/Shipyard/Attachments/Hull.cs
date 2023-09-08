﻿using Terraria.DataStructures;

namespace EndlessEscapade.Common.Systems.Shipyard.Attachments;

public class Hull : StructureAttachment
{
    public Hull(string path) : base(path) { }

    public override Point16 Offset => new Point16(0, 17);
}
