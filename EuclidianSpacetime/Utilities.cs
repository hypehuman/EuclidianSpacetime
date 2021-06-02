using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace EuclidianSpacetime
{
    public static class Utilities
    {
        public static Vector<double> EmptyVectorDD { get; } = Vector<double>.Build.Dense(0);

        public static Vector<double> ToVectorDD(this IEnumerable<double> values)
        {
            return Vector<double>.Build.DenseOfEnumerable(values);
        }

        public static Vector<double> ToVectorDD(params double[] values)
        {
            return Vector<double>.Build.DenseOfArray(values);
        }

        public static int[] FastClone(this int[] array)
        {
            var result = new int[array.Length];
            Buffer.BlockCopy(array, 0, result, 0, array.Length * sizeof(int));
            return result;
        }
    }
}
