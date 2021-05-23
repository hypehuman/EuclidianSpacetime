using System;

namespace EuclidianSpacetime
{
    public interface ISpacetime
    {
        int N { get; }
    }

    internal interface ISpacetimeInternal : ISpacetime
    {
    }

    internal class Spacetime : ISpacetimeInternal
    {
        public int N { get; }

        public Spacetime(int n)
        {
            if (n != 2 && n != 3 && n != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "Number of dimensions must be 2, 3, or 4.");
            }

            N = n;
        }
    }
}
