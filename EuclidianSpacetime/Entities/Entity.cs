using EuclidianSpacetime.Textures;
using MathNet.Numerics.LinearAlgebra;

namespace EuclidianSpacetime.Entities
{
    public interface IEntity
    {
        Vector<double>? ComputeIntersection(ISightRay ray);
        BoundingBox ComputeBoundingBox();
        BoundingBox ComputeBoundingBox(ITimeArrow timeArrow);
        /// <summary>
        /// Returns a snapshot of entity at the given time.
        /// </summary>
        ITexture Texture { get; }
        bool ContainsSample(ISamplePoint samplePoint);
    }
}
