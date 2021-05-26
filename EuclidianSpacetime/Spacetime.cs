using EuclidianSpacetime.Entities;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuclidianSpacetime
{
    public interface ISpacetime
    {
        public void AddEntity(IEntity Entity);

        /// <summary>
        /// Returns a snapshot of space at the given time.
        /// </summary>
        /// <param name="t">The time</param>
        /// <param name="timeArrowAnyLength">A vector that defines the direction of the arrow of time.</param>
        /// <returns>A space with one less dimension than the current spacetime.</returns>
        ISpace TakeCrossSection(double t, Vector<double>? timeArrowAnyLength = null);
    }

    public class Spacetime : ISpacetime
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

        public ISpace TakeCrossSection(double t, Vector<double>? timeArrowAnyLength = null)
        {
            var timeArrow = ValidateTimeArrow(timeArrowAnyLength);
            var crossSections = Entities.SelectMany(e => e.TakeCrossSection(t, timeArrow)).ToList();
            ISpace space = new Space(N - 1, crossSections);
            return space;
        }

        Vector<double> ValidateTimeArrow(Vector<double>? timeArrowAnyLength)
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

        public void AddEntity(IEntity entity)
        {
            Entities.Add(entity);
        }
    }
}
