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
        public int N => A.Count;

        public LineSegment(Vector<double> a, Vector<double> b, ITexture texture)
        {
            A = a;
            B = b;
            Texture = texture;
        }

        public static BoundingBox ComputeBoundingBox(Vector<double> a, Vector<double> b)
        {
            var min = a.Zip(b, Math.Min).ToVectorDD();
            var max = a.Zip(b, Math.Max).ToVectorDD();
            return new BoundingBox(min, max);
        }

        public BoundingBox ComputeBoundingBox()
        {
            return ComputeBoundingBox(A, B);
        }

        public BoundingBox ComputeBoundingBox(ITimeArrow timeArrow)
        {
            return ComputeBoundingBox(timeArrow.Convert(A), timeArrow.Convert(B));
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
            var a = slice.Arrow.Convert(A);
            var b = slice.Arrow.Convert(B);
            var aT = a.Last();
            var bT = b.Last();
            var sT = slice.T;

            var intersectsThisSlice = aT <= sT && sT <= bT || bT <= sT && sT <= aT;
            if (!intersectsThisSlice)
            {
                // Segment doesn't intersect this slice at all
                yield break;
            }

            var abT = bT - aT;
            if (abT == 0)
            {
                // Segment is completely within this slice
                yield return new LineSegment(a.Take(N - 1).ToVectorDD(), b.Take(N - 1).ToVectorDD(), new SlicedTexture(Texture, slice));
            }

            var ab = b - a;
            var asT = sT - aT;
            var displacement = ab / asT;
            var slicedPoint = a + displacement;
            // slicedPoint[t] should be sT
            var color = Texture.ColorAt(slicedPoint);
            yield return new Point(slicedPoint.Take(N - 1).ToVectorDD(), new SimpleTexture(color));
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
