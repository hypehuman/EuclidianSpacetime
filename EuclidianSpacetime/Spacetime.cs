using System.Linq;

namespace EuclidianSpacetime
{
    public interface ISpacetime : ISpace
    {
        double LinearUnitsPerSecond { get; }
        /// <summary>
        /// Returns a snapshot of space at the given time.
        /// </summary>
        ISpace ComputeCrossSection(ITimeSlice timeSlice);
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

        public ISpace ComputeCrossSection(ITimeSlice slice)
        {
            ISpace space = new Space(N - 1);
            foreach (var lowerEntity in Entities.SelectMany(e => e.ComputeCrossSection(slice)))
            {
                space.AddEntity(lowerEntity);
            }
            return space;
        }

        public BoundingBox ComputeBoundingBox(ITimeArrow timeArrow)
        {
            return BoundingBox.Union(N, Entities.Select(e => e.ComputeBoundingBox(timeArrow)));
        }
    }
}
