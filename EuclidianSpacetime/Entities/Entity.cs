using EuclidianSpacetime.Textures;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;

namespace EuclidianSpacetime.Entities
{
    public interface IEntity
    {
        Vector<double>? ComputeIntersection(ISightRay ray);
        BoundingBox ComputeBoundingBox();
        /// <summary>
        /// Returns a snapshot of entity at the given time.
        /// </summary>
        IEnumerable<IEntity> ComputeCrossSection(ITimeSlice slice);
        ITexture Texture { get; }
        ARGB ComputeColorAtIntersection(ISightRay ray)
        {
            var intersection = ComputeIntersection(ray);
            return intersection == null ? ARGB.TransparentBlack : Texture.ColorAt(intersection);
        }
        bool ContainsSample(ISamplePoint samplePoint);
        ARGB SampleColor(ISamplePoint samplePoint) => ContainsSample(samplePoint) ? Texture.ColorAt(samplePoint.P) : default;
    }
}
