using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Textures
{
    public interface ITexture
    {
        ARGB ColorAt(Vector<double> position);
    }
}
