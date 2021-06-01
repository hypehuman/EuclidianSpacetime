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

        public Vector<double>? ComputeIntersection(ISightRay ray)
        {
            switch (A.Count)
            {
                case 1:
                    var segA = A[0];
                    var segB = B[0];
                    var rayA = ray.A[0];
                    if (segA <= rayA && rayA <= segB || segB <= rayA && rayA <= segA)
                    {
                        // ray starts inside the line segment
                        return ray.A;
                    }
                    var raySign = ray.ABUnit[0];
                    var distanceToA = raySign * (segA - rayA);
                    if (distanceToA < 0)
                    {
                        // ray points away from segment
                        return null;
                    }
                    // ray points toward segment; return whichever side is closer
                    var distanceToB = raySign * (segB - rayA);
                    return distanceToA < distanceToB ? A : B;
                case 2:
                    throw new NotImplementedException("TODO: implement for R" + A.Count);
                default:
                    // A line segment probably won't block a ray in R3 or higher, so give it some thickness.
                    throw new NotImplementedException("TODO: implement for R" + A.Count);
            }
        }

        public IEnumerable<IEntity> ComputeCrossSection(ITimeSlice slice)
        {
            throw new NotImplementedException("TODO: Return a single point if the slice intersects, otherwise an empty set.");
        }

        public bool ContainsSample(ISamplePoint samplePoint)
        {
            switch (A.Count)
            {
                case 1:
                    var a = A[0];
                    var b = B[0];
                    var s = samplePoint.P[0];
                    return a <= s && s <= b || b <= s && s <= a;
                case 2:
                    // A line probably won't intersect with a point in R2 or higher, so give it some thickness.
                    throw new NotImplementedException("TODO: implement for R" + A.Count);
                default:
                    throw new NotImplementedException("TODO: implement for R" + A.Count);
            }
        }
    }
}
