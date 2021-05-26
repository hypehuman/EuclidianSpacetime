using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace EuclidianSpacetime.Entities
{
    public interface IEntity
    {
        IRayIntersection GetIntersection(IRay ray);
        BoundingBox ComputeBoundingBox();
        IEnumerable<IEntity> TakeCrossSection(double t, Vector<double> timeArrow);
    }
}
