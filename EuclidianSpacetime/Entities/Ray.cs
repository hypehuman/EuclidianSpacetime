using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Entities
{
    public interface IRay
    {
        Vector<double> A { get; }
        Vector<double> B { get; }
    }
}
