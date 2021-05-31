using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime
{
    public interface ISightRay
    {
        Vector<double> A { get; }
        Vector<double> B { get; }
        double ThicknessOfInfinitesimal { get; }
        Vector<double> AB { get; }
        Vector<double> ABUnit { get; }
    }

    public class SightRay : ISightRay
    {
        public Vector<double> A { get; }
        public Vector<double> B { get; }
        public double ThicknessOfInfinitesimal { get; }
        public Vector<double> AB { get; }
        public Vector<double> ABUnit { get; }

        public SightRay(Vector<double> a, Vector<double> b, double thicknessOfInfinitesimal)
        {
            A = a;
            B = b;
            ThicknessOfInfinitesimal = thicknessOfInfinitesimal;
            AB = B - A;
            ABUnit = AB / AB.L2Norm();
        }
    }
}
