﻿using EuclidianSpacetime.Textures;
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
        bool ContainsSample(ISamplePoint samplePoint);
    }
}
