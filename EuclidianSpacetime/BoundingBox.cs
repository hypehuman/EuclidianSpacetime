using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime
{
    public interface IBoundingBox
    {
        Vector<double> Min { get; }
        Vector<double> Max { get; }
    }
}
