using System.Linq;

namespace EuclidianSpacetime
{
    public interface ISpacetime : ISpace
    {
        double LinearUnitsPerSecond { get; }
        BoundingBox ComputeBoundingBox(ITimeArrow timeArrow);
    }

    public class Spacetime : Space, ISpacetime
    {
        /// <summary>
        /// The conversion factor from time to length, which some might call "c". Specifically, the number of linear spacial units per second.
        /// </summary>
        public double LinearUnitsPerSecond { get; }

        public Spacetime(int n, int linearUnitsPerSecond)
            : base(n)
        {
            LinearUnitsPerSecond = linearUnitsPerSecond;
        }

        public BoundingBox ComputeBoundingBox(ITimeArrow timeArrow)
        {
            return BoundingBox.Union(N, Entities.Select(e => e.ComputeBoundingBox(timeArrow)));
        }
    }
}
