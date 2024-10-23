using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessEscapade.Utilities;

/// <summary>
/// Provides math related utilities.
/// </summary>
public static class MathUtils {

    public static float Modulo(float value, float length) => value - (float)Math.Floor(value / length) * length;

}