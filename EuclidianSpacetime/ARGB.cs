namespace EuclidianSpacetime
{
    public struct ARGB
    {
        public static ARGB TransparentBlack => default;

        public byte A { get; }
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }

        public ARGB(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public ARGB(byte r, byte g, byte b)
            : this(byte.MaxValue, r, g, b)
        {
        }

        public ARGB(byte a, RGB rgb)
            : this(a, rgb.R, rgb.G, rgb.B)
        {
        }

        public ARGB(RGB rgb)
            : this(rgb.R, rgb.G, rgb.B)
        {
        }

        public static implicit operator ARGB(RGB rgb) => new(rgb);
    }
}
