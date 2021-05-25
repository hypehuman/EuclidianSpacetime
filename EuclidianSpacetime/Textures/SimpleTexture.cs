using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Textures
{
    public interface ISimpleTexture : ITexture
    {
        ARGB Color { get; }
        ARGB ITexture.ColorAt(Vector<double> position) => Color;
    }
}
