using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime
{
    public interface ISamplePoint
    {
        Vector<double> P { get; }
        double RadiusOfInfinitesimal { get; }
    }

    public class SamplePoint : ISamplePoint
    {
        public Vector<double> P { get; }
        public double RadiusOfInfinitesimal { get; }

        public SamplePoint(Vector<double> p, double radiusOfInfinitesimal)
        {
            P = p;
            RadiusOfInfinitesimal = radiusOfInfinitesimal;
        }
    }
}
