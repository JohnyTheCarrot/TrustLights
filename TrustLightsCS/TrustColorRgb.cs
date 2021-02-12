using System;
using System.Collections.Generic;
using System.Text;

namespace TrustLightsCS
{
    public class TrustColorRgb
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public TrustColorRgb(byte r, byte g, byte b)
        {
            R = RgbConstrained(r);
            G = RgbConstrained(g);
            B = RgbConstrained(b);
        }

        public override string ToString()
        {
            return $"{R}, {G}, {B}";
        }

        public byte RgbConstrained(int value)
        {
            if (value < 0)
                return 0;
            if (value >= 255)
                return 255;
            return (byte) value;
        }

        public TrustColorXyz ToXyz()
        {
            var f1 = R / 255.0f;
            if (f1 < 0.04045f)
                f1 /= 12.92f;
            else
            {
                double d = f1;
                f1 = (float) Math.Pow((d + +0.055d) / 1.0549999475479126D, 2.4000000953674316D);
            }

            var f2 = G / 255.0f;
            if (f2 < 0.04045f)
                f2 /= 12.92f;
            else
            {
                double d = f2;
                f2 = (float)Math.Pow((d + +0.055d) / 1.0549999475479126D, 2.4000000953674316D);
            }

            var f3 = B / 255.0f;
            if (f3 < 0.04045f)
                f3 /= 12.92f;
            else
            {
                double d = f3;
                f3 = (float)Math.Pow((d + +0.055d) / 1.0549999475479126D, 2.4000000953674316D);
            }

            return new TrustColorXyz(0.4124F * f1 + 0.3576F * f2 + 0.1805F * f3, 0.2126F * f1 + 0.7152F * f2 + 0.0722F * f3, f1 * 0.0193F + f2 * 0.1192F + f3 * 0.9505F);
        }
    }
}
