﻿using Terraria.Localization;
using Terraria.ModLoader;

namespace EndlessEscapade.Utilities.Extensions;

public static class ModExtensions
{
    public static string GetTextValue(this Mod mod, string key) {
        return Language.GetTextValue($"Mods.{mod.Name}.{key}");
    }
}
