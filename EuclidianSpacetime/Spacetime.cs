using MathNet.Numerics.LinearAlgebra;
using System;

namespace EuclidianSpacetime
{
    public interface ISpacetime
    {
        int N { get; }
    }

    internal class Spacetime : ISpacetime
    {
        public int N { get; }
        public Vector<double> TimeArrow { get; }

        public Spacetime(int n, Vector<double> timeArrowAnyLength = null)
        {
            if (n != 2 && n != 3 && n != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "Number of dimensions must be 2, 3, or 4.");
            }

            N = n;

            if (timeArrowAnyLength == null)
            {
                var timeArrow = Vector<double>.Build.Dense(N);
                timeArrow[N - 1] = 1;
                TimeArrow = timeArrow;
            }
            else
            {
                if (timeArrowAnyLength.Count != N)
                {
                    throw new ArgumentException("Time arrow length must be N", nameof(timeArrowAnyLength));
                }

                TimeArrow = timeArrowAnyLength / timeArrowAnyLength.L2Norm();
            }
        }
    }
}
