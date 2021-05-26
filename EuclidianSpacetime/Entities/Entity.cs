using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace EuclidianSpacetime.Entities
{
    public interface IEntity
    {
        IRayIntersection GetIntersection(IRay ray);
        BoundingBox ComputeBoundingBox();
        /// <summary>
        /// Returns a snapshot of entity at the given time.
        /// </summary>
        /// <param name="t">The time</param>
        /// <param name="timeArrow">A unit vector that defines the direction of the arrow of time.</param>
        /// <returns>A set of entities with one less dimension than the current entity.</returns>
        IEnumerable<IEntity> TakeCrossSection(double t, Vector<double> timeArrow);
    }
}
