namespace EuclidianSpacetime
{
    public struct ARGB32
    {
        public static ARGB32 TransparentBlack => default;

        public byte A { get; }
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }

        public ARGB32(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public ARGB32(byte r, byte g, byte b)
            : this(byte.MaxValue, r, g, b)
        {
        }

        public ARGB32(byte a, RGB rgb)
            : this(a, rgb.R, rgb.G, rgb.B)
        {
        }

        public ARGB32(RGB rgb)
            : this(rgb.R, rgb.G, rgb.B)
        {
        }

        public static implicit operator ARGB32(RGB rgb) => new(rgb);
    }
}
