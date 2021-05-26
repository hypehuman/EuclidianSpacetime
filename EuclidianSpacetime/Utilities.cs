using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace EuclidianSpacetime
{
    public static class Utilities
    {
        public static Vector<double> ToVectorDD(this IEnumerable<double> values)
        {
            return Vector<double>.Build.DenseOfEnumerable(values);
        }

        public static Vector<double> ToVectorDD(params double[] values)
        {
            return Vector<double>.Build.DenseOfArray(values);
        }
    }
}
