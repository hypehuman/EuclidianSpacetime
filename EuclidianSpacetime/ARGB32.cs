namespace EuclidianSpacetime
{
    public readonly record struct ARGB32(byte A, byte R, byte G, byte B)
    {
        public static ARGB32 TransparentBlack => default;

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

        public override string ToString()
        {
            return $"{A},{R},{G},{B}";
        }
    }
}
