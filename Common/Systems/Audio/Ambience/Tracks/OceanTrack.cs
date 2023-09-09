﻿using System.Collections.Generic;
using Terraria;
using Terraria.Audio;

namespace EndlessEscapade.Common.Systems.Audio.Ambience.Tracks;

public class OceanTrack : AmbienceTrack
{
    private static readonly SoundStyle loop = new($"{nameof(EndlessEscapade)}/Assets/Sounds/Ambience/Ocean/Submerged", SoundType.Ambient) {
        IsLooped = true,
        Volume = 0.7f
    };

    private static readonly SoundStyle roarSounds = new($"{nameof(EndlessEscapade)}/Assets/Sounds/Ambience/Ocean/Roar", 1, SoundType.Ambient);

    protected override void Initialize() {
        Loops = new List<AmbienceSoundData> { new(loop) };

        Sounds = new List<AmbienceSoundData> { new(roarSounds, player => player.ZoneRockLayerHeight) };
    }

    protected override bool IsActive(Player player) { return player.wet; }
}
