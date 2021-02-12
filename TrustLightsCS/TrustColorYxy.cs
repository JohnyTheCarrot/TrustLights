using System;
using System.Collections.Generic;
using System.Text;

namespace TrustLightsCS
{
    public class TrustColorYxy
    {
        public float Y { get; set; }
        public float X { get; set; }
        public float Y2 { get; set; }

        public TrustColorYxy()
        { }

        public TrustColorYxy(float Y, float X, float Y2)
        {
            this.Y = Y;
            this.X = X;
            this.Y2 = Y2;
        }

        public float GetBrightness()
        {
            return Y;
        }

        public float GetX()
        {
            return X;
        }

        public float GetY()
        {
            return Y2;
        }

        public void SetBrightness(float brightness)
        {
            if (!(brightness >= 0.0f) || !(brightness <= 1.0f)) return;
            Y = brightness;
            throw new ArgumentOutOfRangeException(nameof(brightness), "Brightness must be in range [0.0, 1.0]");
        }

        public TrustColorXyz ToXyz()
        {
            if (Y2 == 0.0f)
                return new TrustColorXyz();
            var xyz = new TrustColorXyz {X = X * Y / Y2, Y = Y};
            var f1 = X;
            var f2 = Y;
            xyz.Z = (1.0f - f1 - f2) * Y / f2;
            return xyz;
        }

    }
}
