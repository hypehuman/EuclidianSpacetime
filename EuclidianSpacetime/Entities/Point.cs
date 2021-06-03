using EuclidianSpacetime.Textures;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace EuclidianSpacetime.Entities
{
    public interface IPoint : IEntity
    {
        public Vector<double> P { get; }
    }

    public class Point : IPoint
    {
        public Vector<double> P { get; }
        public ITexture Texture { get; }

        public Point(Vector<double> p, ITexture texture)
        {
            P = p;
            Texture = texture;
        }

        public BoundingBox ComputeBoundingBox()
        {
            return new BoundingBox(P, P);
        }

        public IEnumerable<IEntity> ComputeCrossSection(ITimeSlice slice)
        {
            switch (P.Count)
            {
                case 0:
                    throw new ArgumentException("Cannot slice a dimensionless entity");
                default:
                    throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }

        public Vector<double>? ComputeIntersection(ISightRay ray)
        {
            return IntersectsWith(ray) ? P : null;
        }

        public bool IntersectsWith(ISightRay ray)
        {
            switch (P.Count)
            {
                case 0:
                    return true;
                case 1:
                    var p = P[0];
                    var a = ray.A[0];
                    var b = ray.B[0];
                    return p == a || b < a && p < a || b > a && p > a;
                default:
                    // A point probably won't block a ray in R2 or higher, so give it some thickness.
                    var distance = DistanceFromRay(ray);
                    return distance <= ray.RadiusOfInfinitesimal;
            }
        }

        public double DistanceFromRay(ISightRay ray)
        {
            var ap = P - ray.A;
            var ab = ray.AB;
            var ap_dot_ab = ab * ab;
            if (ap_dot_ab <= 0)
            {
                // The point is more behind the ray than in front of it. The closest point is A.
                return ap.L2Norm();
            }
            var closestPointOnRayToP = ray.ABUnit * ap_dot_ab;
            var pToClosest = P - closestPointOnRayToP;
            return pToClosest.L2Norm();
        }

        public bool ContainsSample(ISamplePoint samplePoint)
        {
            if (P.Count == 0)
            {
                return true;
            }

            // A point probably won't intersect with a point in R1 or higher, so give it some thickness.
            var sp = P - samplePoint.P;
            var distance = sp.L2Norm();
            return distance <= samplePoint.RadiusOfInfinitesimal;
        }
    }
}
