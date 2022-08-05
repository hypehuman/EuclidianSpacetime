using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace EuclidianSpacetime
{
    public readonly record struct BoundingBox(Vector<double> Min, Vector<double> Max)
    {
        public int N => Min.Count;

        public static BoundingBox Union(int n, IEnumerable<BoundingBox> boundingBoxes)
        {
            var any = false;
            var min = Vector<double>.Build.Dense(n);
            var max = Vector<double>.Build.Dense(n);
            foreach (var entityBB in boundingBoxes)
            {
                for (var i = 0; i < n; i++)
                {
                    var entityMin = entityBB.Min[i];
                    var entityMax = entityBB.Max[i];
                    if (!any || entityMin < min[i])
                    {
                        min[i] = entityMin;
                    }
                    if (!any || entityMax > max[i])
                    {
                        max[i] = entityMax;
                    }
                }
                any = true;
            }
            return new BoundingBox(min, max);
        }
    }
}
