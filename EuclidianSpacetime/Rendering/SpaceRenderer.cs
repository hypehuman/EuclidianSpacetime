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
        public static IEnumerable<(int[], ARGB32)> Render(ISpace space, double radiusOfInfinitesimal, IReadOnlyList<DimensionInfo> dimensionInfo)
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
                var sample = space.Sample(new SamplePoint(sampleCoords, radiusOfInfinitesimal));
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
                for (var i = n - 1; counter[i] >= dimensionInfo[i].NumSamples; i--)
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

        /// <param name="linearUnitsPerSample">the distance between samples, i.e., 1/resolution</param>
        public static IReadOnlyList<DimensionInfo> GetDimensionInfo(BoundingBox region, double linearUnitsPerSample)
        {
            var dimensionInfo = Enumerable.Range(0, region.N).Select(d => GetDimensionInfo(d, linearUnitsPerSample, region)).ToList();
            return dimensionInfo;
        }

        /// <param name="linearUnitsPerSample">the distance between samples, i.e., 1/resolution</param>
        public static (IReadOnlyList<DimensionInfo>, BoundingBox) GetDimensionInfo(ISpace space, double linearUnitsPerSample)
        {
            var region = space.ComputeBoundingBox();
            var dimensionInfo = GetDimensionInfo(region, linearUnitsPerSample);
            return (dimensionInfo, region);
        }

        /// <param name="linearUnitsPerSample">the distance between samples, i.e., 1/resolution</param>
        public static DimensionInfo GetDimensionInfo(int dimension, double linearUnitsPerSample, BoundingBox region)
        {
            var min = region.Min[dimension];
            var max = region.Max[dimension];
            var regionSize = max - min;
            var numSamples = (int)Math.Floor(regionSize / linearUnitsPerSample);
            if (numSamples <= 0)
            {
                return new DimensionInfo(1, (min + max) / 2, linearUnitsPerSample);
            }
            var sampledSize = (numSamples - 1) * linearUnitsPerSample; // the distance between the first and last samples
            var margin = (regionSize - sampledSize) / 2;
            var offset = min + margin;
            return new DimensionInfo(numSamples, offset, linearUnitsPerSample);
        }
    }
}
