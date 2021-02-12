using System;
using System.Collections.Generic;
using System.Text;

namespace TrustLightsCS
{
    public class TrustXyValue
    {
        private readonly TrustColorYxy _color;

        public TrustXyValue(long value)
        {
            var bytes = new byte[4];
            bytes[0] = (byte) (value & 0xFFL);
            bytes[1] = (byte) (value >> 8 & 0xFFL);
            bytes[2] = (byte) (value >> 16 & 0xFFL);
            bytes[3] = (byte) (value >> 24 & 0xFFL);
            const float x = 1.0f;
            var y = (bytes[2] | (bytes[3] << 8)) / ushort.MaxValue;
            var y2 = (bytes[0] | (bytes[1] << 8)) / ushort.MaxValue;
            _color = new TrustColorYxy(x, y, y2);
        }

        public TrustXyValue(TrustColorYxy color)
        {
            _color = color;
        }

        public long ToValue()
        {
            var i = (int) (_color.GetX() * ushort.MaxValue);
            var j = (int) (_color.GetY() * ushort.MaxValue);
            var bytes = new byte[4];
            bytes[0] = (byte) (i & 0xFF);
            bytes[1] = (byte) (i >> 8 & 0xFF);
            bytes[2] = (byte) (j & 0xFF);
            bytes[3] = (byte) (j >> 8 & 0xFF);
            var value = bytes[2] | bytes[3] << 8 | bytes[0] << 16 | bytes[1] << 24;
            return value;
        }
    }
}
