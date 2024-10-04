using EndlessEscapade.Core.Audio;
using EndlessEscapade.Utilities.Extensions;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace EndlessEscapade.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class WaterMufflingEffects : ModPlayer
{
    private float intensity;

    public float Intensity {
        get => intensity;
        set => intensity = MathHelper.Clamp(value, 0f, 0.9f);
    }

    public override void PostUpdate() {
        if (Player.IsDrowning()) {
            Intensity += 0.05f;
        }
        else {
            Intensity -= 0.05f;
        }

        if (Intensity < 0f) {
            return;
        }

        AudioManager.AddModifier(
            $"{nameof(EndlessEscapade)}:{nameof(WaterMufflingEffects)}",
            60,
            (ref AudioParameters parameters, float progress) => { parameters.LowPass = Intensity * progress; }
        );
    }
}
