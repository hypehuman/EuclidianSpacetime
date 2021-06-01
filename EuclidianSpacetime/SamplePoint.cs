using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime
{
    public interface ISamplePoint
    {
        Vector<double> P { get; }
        double ThicknessOfInfinitesimal { get; }
    }

    public class SamplePoint : ISamplePoint
    {
        public Vector<double> P { get; }
        public double ThicknessOfInfinitesimal { get; }

        public SamplePoint(Vector<double> p, double thicknessOfInfinitesimal)
        {
            P = p;
            ThicknessOfInfinitesimal = thicknessOfInfinitesimal;
        }
    }
}
