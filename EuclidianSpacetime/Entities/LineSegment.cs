using EuclidianSpacetime.Textures;
using MathNet.Numerics.LinearAlgebra;

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

        public Vector<double> GetBoundingBox()
        {
            throw new System.NotImplementedException();
        }

        public IRayIntersection GetIntersection(IRay ray)
        {
            throw new System.NotImplementedException();
        }

        public IEntity TakeCrossSection()
        {
            throw new System.NotImplementedException();
        }
    }
}
