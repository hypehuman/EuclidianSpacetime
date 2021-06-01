using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using static EuclidianSpacetime.Utilities;

namespace EuclidianSpacetime
{
    public static class SpaceRenderer
    {
        /// <summary>
        /// resulting lazy enumerable supports parallelization
        /// </summary>
        public static IEnumerable<(Vector<int>, ARGB32)> Render(ISpace space, double samplesPerLinearUnit, double thicknessOfInfinitesimal, BoundingBox? regionIn = null)
        {
            var n = space.N;
            if (n == 0)
            {
                var sample = (EmptyIntVector, space.Sample(new SamplePoint(EmptyDoubleVector, 0)));
                return new[] { sample };
            }

            var region = regionIn ?? space.ComputeBoundingBox();
            var dimensionInfo = Enumerable.Range(0, n).Select(d => GetDimensionInfo(d, samplesPerLinearUnit, region)).ToList();
            var samplePoints = GetSamplePoints(dimensionInfo);
            var result = samplePoints.Select(sampleID =>
            {
                var sampleCoords = Vector<double>.B;
                var sample = space.Sample(new SamplePoint(sampleCoords, thicknessOfInfinitesimal));
                return (sampleID, sample);
            });
            return result;
        }

        private static DimensionInfo GetDimensionInfo(int dimension, double samplesPerLinearUnit, BoundingBox region)
        {
            var min = region.Min[dimension];
            var max = region.Max[dimension];
            var regionSize = max - min;
            var numSamples = (int)Math.Floor(regionSize * samplesPerLinearUnit);
            if (numSamples <= 0)
            {
                return new DimensionInfo(1, (min + max) / 2);
            }
            var sampledSize = (numSamples - 1) * samplesPerLinearUnit; // the distance between the first and last samples
            var margin = (regionSize - sampledSize) / 2;
            return new DimensionInfo(numSamples, margin);
        }

        private static IEnumerable<Vector<int>> GetSamplePoints(IReadOnlyList<DimensionInfo> dimensionInfo)
        {
            var n = dimensionInfo.Count;
            var sampleIndexArray = new int[n];
            while (true)
            {
                sampleIndexArray[n - 1]++;
                for (var i = n - 1; i >= 0; i--)
                {
                    if ()
                }
                yield return Vector<int>.Build.DenseOfArray(sampleIndexArray);
            }
        }

        private class DimensionInfo
        {
            public int NumSamples { get; }
            public double Offset { get; }

            public DimensionInfo(int numSamples, double offset)
            {
                NumSamples = numSamples;
                Offset = offset;
            }
        }
    }
}
