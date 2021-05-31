using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace EuclidianSpacetime.Entities
{
    public interface IEntity
    {
        IRayIntersection ComputeIntersection(IRay ray);
        BoundingBox ComputeBoundingBox();
        /// <summary>
        /// Returns a snapshot of entity at the given time.
        /// </summary>
        IEnumerable<IEntity> ComputeCrossSection(ITimeSlice slice);
    }
}
