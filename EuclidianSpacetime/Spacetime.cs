using EuclidianSpacetime.Entities;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace EuclidianSpacetime
{
    public interface ISpacetime
    {
        public void Add(IEntity Entity);
    }

    internal class Spacetime : ISpacetime
    {
        /// <summary>
        /// The number of dimensions, including time
        /// </summary>
        int N { get; }

        /// <summary>
        /// The conversion factor from time to length. Specifically, the number of linear spacial units per second.
        /// </summary>
        int C { get; }

        List<IEntity> Entities { get; } = new List<IEntity>();

        public Spacetime(int n, int c)
        {
            if (n != 2 && n != 3 && n != 4)
            {
                throw new ArgumentOutOfRangeException(nameof(n), n, "Number of dimensions must be 2, 3, or 4.");
            }

            N = n;
            C = c;
        }

        /// <summary>
        /// Returns a snapshot of space at the given time.
        /// </summary>
        /// <param name="t">The time</param>
        /// <param name="timeArrow">A unit vector that defines the direction of the arrow of time.</param>
        /// <returns>A space with one less dimension than the current spacetime.</returns>
        ISpace TakeCrossSection(double t, Vector<double> timeArrowAnyLength = null)
        {
            var timeArrow = ValidateTimeArrow(timeArrowAnyLength);
            throw new NotImplementedException();
        }

        Vector<double> ValidateTimeArrow(Vector<double> timeArrowAnyLength)
        {
            if (timeArrowAnyLength == null)
            {
                return GetDefaultTimeArrow();
            }
            else
            {
                if (timeArrowAnyLength.Count != N)
                {
                    throw new ArgumentException($"Time arrow length must be {nameof(N)}", nameof(timeArrowAnyLength));
                }

                var length = timeArrowAnyLength.L2Norm();
                var timeArrow = timeArrowAnyLength / length;
                return timeArrow;
            }
        }

        Vector<double> GetDefaultTimeArrow()
        {
            var timeArrow = Vector<double>.Build.Dense(N);
            timeArrow[N - 1] = 1;
            return timeArrow;
        }

        public void Add(IEntity entity)
        {
            Entities.Add(entity);
        }
    }
}
