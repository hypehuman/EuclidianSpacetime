using System;
using System.Linq;

namespace EuclidianSpacetime
{
    public interface ISpacetime : ISpace
    {
        /// <summary>
        /// Returns a snapshot of space at the given time.
        /// </summary>
        ISpace ComputeCrossSection(ITimeSlice timeSlice);
    }

    public class Spacetime : Space, ISpacetime
    {
        /// <summary>
        /// The conversion factor from time to length. Specifically, the number of linear spacial units per second.
        /// </summary>
        int C { get; }

        public Spacetime(int n, int c)
            : base(n)
        {
            if (n != 2 && n != 3 && n != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "Number of dimensions must be 2, 3, or 4.");
            }

            C = c;
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
    }
}
