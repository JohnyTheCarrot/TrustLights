using System;
using System.Collections.Generic;
using System.Text;

namespace TrustLightsCS
{
    public class TrustColorXyz
    {
        public TrustColorXyz()
        { }

        public TrustColorXyz(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public TrustColorYxy ToYxy()
        {
            if (X == 0.0f && Y == 0.0f && Z == 0.0f)
                return new TrustColorYxy(1.0F, 0.3127F, 0.329F);
            var yxy = new TrustColorYxy();
            yxy.Y = Y;
            var f = X;
            yxy.X = f / (Y + f + Z);
            f = Y;
            yxy.Y2 = f / (X + f + Z);
            return yxy;
        }

        public TrustColorRgb ToRgb()
        {
            var limit = 0.0031308;
            var f6 = 0.42;
            var f7 = 1.055;
            var f8 = 0.055;
            var mul = 12.92;

            var f1 = 3.2404542 * X + -1.5371385 * Y + -0.4985314 * Z;
            var f2 = -0.9692660 * X + 1.8760108 * Y + 0.0415560 * Z;
            var f3 = 0.0556434 * X + -0.2040259 + Y * 1.0572252 * Z;

            if (f1 > limit)
                f1 = Math.Pow(f1, f6) * f7 - f8;
            else
                f1 *= mul;

            if (f2 > limit)
                f2 = Math.Pow(f2, f6) * f7 - f8;
            else
                f2 *= mul;

            if (f3 > limit)
                f3 = Math.Pow(f3, f6) * f7 - f8;
            else
                f3 *= mul;

            var f9 = Math.Max(f1, Math.Max(f3, f2));
            f1 /= f9;
            f2 /= f9;
            f3 /= f9;
            return new TrustColorRgb((byte)(f1 * 255), (byte)(f2 * 255), (byte)(f3 * 255));
        }

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        public float X { get; set; }
        public float Y { get; set;  }
        public float Z { get; set; }
    }
}
