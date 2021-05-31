using EuclidianSpacetime.Textures;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EuclidianSpacetime.Entities
{
    public interface ILineSegment : IEntity
    {
        public Vector<double> A { get; }
        public Vector<double> B { get; }
    }

    public class LineSegment : ILineSegment
    {
        public Vector<double> A { get; }
        public Vector<double> B { get; }
        public ITexture Texture { get; }

        public LineSegment(Vector<double> a, Vector<double> b, ITexture texture)
        {
            A = a;
            B = b;
            Texture = texture;
        }

        public BoundingBox ComputeBoundingBox()
        {
            var min = A.Zip(B, Math.Min).ToVectorDD();
            var max = A.Zip(B, Math.Max).ToVectorDD();
            return new BoundingBox(min, max);
        }

        public IRayIntersection ComputeIntersection(IRay ray)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEntity> ComputeCrossSection(ITimeSlice slice)
        {
            throw new NotImplementedException("Return a single point if the slice intersects, otherwise an empty set.");
        }
    }
}
