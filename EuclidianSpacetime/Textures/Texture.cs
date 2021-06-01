using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Textures
{
    public interface ITexture
    {
        ARGB32 ColorAt(Vector<double> position);
    }
}
