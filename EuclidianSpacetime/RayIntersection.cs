using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime
{
    public interface IRayIntersection
    {
        Vector<double> Position { get; }
        IColor Color { get; }
    }
}
