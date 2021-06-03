using System;
using System.Collections.Generic;
using System.Linq;

namespace EuclidianSpacetime.Rendering
{
    public static class TimeRenderer
    {
        /// <summary>
        /// resulting lazy enumerable supports parallelization
        /// </summary>
        public static IEnumerable<ISpace> Render(ISpacetime spacetime, ITimeArrow timeArrow, DimensionInfo dimensionInfo)
        {
            var n = spacetime.N;
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(spacetime)}.{nameof(spacetime.N)}", spacetime.N, "Spacetime must have at least one dimension");
            }

            var result = Enumerable.Range(0, dimensionInfo.NumSamples).Select(spaceID =>
            {
                var t = dimensionInfo.GetSampleCoord(spaceID);
                var space = spacetime.ComputeCrossSection(new TimeSlice(t, timeArrow));
                return space;
            });
            return result;
        }

        public static DimensionInfo GetDimensionInfo(BoundingBox region, double linearUnitsPerFrame)
        {
            var timeDimension = region.N - 1;
            var dimensionInfo = SpaceRenderer.GetDimensionInfo(timeDimension, linearUnitsPerFrame, region);
            return dimensionInfo;
        }

        public static (DimensionInfo, BoundingBox) GetDimensionInfo(ISpacetime spacetime, ITimeArrow timeArrow, double secondsPerFrame)
        {
            var region = spacetime.ComputeBoundingBox(timeArrow);
            var linearUnitsPerFrame = spacetime.LinearUnitsPerSecond * secondsPerFrame;
            var dimensionInfo = GetDimensionInfo(region, linearUnitsPerFrame);
            return (dimensionInfo, region);
        }
    }
}
