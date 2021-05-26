using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime
{
    public struct BoundingBox
    {
        public Vector<double> Min { get; }
        public Vector<double> Max { get; }

        public BoundingBox(Vector<double> min, Vector<double> max) : this()
        {
            Min = min;
            Max = max;
        }
    }
}
