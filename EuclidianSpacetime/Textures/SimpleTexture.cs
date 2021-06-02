using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Textures
{
    public interface ISimpleTexture : ITexture
    {
        ARGB32 Color { get; }
        ARGB32 ITexture.ColorAt(Vector<double> position) => Color;
    }

    public class SimpleTexture : ISimpleTexture
    {
        public ARGB32 Color { get; }

        public SimpleTexture(ARGB32 color)
        {
            Color = color;
        }
    }
}
