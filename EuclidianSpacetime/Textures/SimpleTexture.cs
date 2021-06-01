using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Textures
{
    public interface ISimpleTexture : ITexture
    {
        ARGB32 Color { get; }
        ARGB32 ITexture.ColorAt(Vector<double> position) => Color;
    }
}
