using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Entities
{
    public interface IEntity
    {
        IRayIntersection GetIntersection(IRay ray);
        Vector<double> GetBoundingBox();
        IEntity TakeCrossSection();
    }
}
