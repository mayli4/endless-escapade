﻿using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace EndlessEscapade.Common.Config;

public class AudioConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool EnableLowPassFiltering { get; set; } = true;
}
