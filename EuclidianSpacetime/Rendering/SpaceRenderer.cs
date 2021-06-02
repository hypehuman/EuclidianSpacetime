using System;
using System.Collections.Generic;
using System.Linq;
using static EuclidianSpacetime.Utilities;

namespace EuclidianSpacetime.Rendering
{
    public static class SpaceRenderer
    {
        /// <summary>
        /// resulting lazy enumerable supports parallelization
        /// </summary>
        public static IEnumerable<(int[], ARGB32)> Render(ISpace space, double thicknessOfInfinitesimal, IReadOnlyList<DimensionInfo> dimensionInfo)
        {
            var n = space.N;
            if (n == 0)
            {
                var sample = (Array.Empty<int>(), space.Sample(new SamplePoint(EmptyVectorDD, 0)));
                return new[] { sample };
            }

            var sampleIDs = GetSampleIDs(dimensionInfo);
            var result = sampleIDs.Select(sampleID =>
            {
                var sampleCoords = dimensionInfo.Zip(sampleID, (di, xi) => di.GetSampleCoord(xi)).ToVectorDD();
                var sample = space.Sample(new SamplePoint(sampleCoords, thicknessOfInfinitesimal));
                return (sampleID, sample);
            });
            return result;
        }

        private static IEnumerable<int[]> GetSampleIDs(IReadOnlyList<DimensionInfo> dimensionInfo)
        {
            var n = dimensionInfo.Count;
            var counter = new int[n];
            while (true)
            {
                yield return counter.FastClone();
                counter[n - 1]++;
                for (var i = n - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        yield break;
                    }

                    counter[i] = 0;
                    counter[i - 1]++;
                }
            }
        }

        public static IReadOnlyList<DimensionInfo> GetDimensionInfo(int n, BoundingBox region, double samplesPerLinearUnit)
        {
            var dimensionInfo = Enumerable.Range(0, n).Select(d => GetDimensionInfo(d, samplesPerLinearUnit, region)).ToList();
            return dimensionInfo;
        }

        public static (IReadOnlyList<DimensionInfo>, BoundingBox) GetDimensionInfo(ISpace space, double samplesPerLinearUnit)
        {
            var region = space.ComputeBoundingBox();
            var dimensionInfo = GetDimensionInfo(space.N, region, samplesPerLinearUnit);
            return (dimensionInfo, region);
        }

        private static DimensionInfo GetDimensionInfo(int dimension, double samplesPerLinearUnit, BoundingBox region)
        {
            var min = region.Min[dimension];
            var max = region.Max[dimension];
            var regionSize = max - min;
            var numSamples = (int)Math.Floor(regionSize * samplesPerLinearUnit);
            if (numSamples <= 0)
            {
                return new DimensionInfo(1, (min + max) / 2, samplesPerLinearUnit);
            }
            var sampledSize = (numSamples - 1) * samplesPerLinearUnit; // the distance between the first and last samples
            var margin = (regionSize - sampledSize) / 2;
            return new DimensionInfo(numSamples, margin, samplesPerLinearUnit);
        }
    }
}
