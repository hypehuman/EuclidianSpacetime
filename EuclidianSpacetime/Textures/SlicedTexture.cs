using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace EuclidianSpacetime.Textures
{
    public class SlicedTexture : ITexture
    {
        public ITexture Underlying { get; }
        public ITimeSlice Slice { get; }

        public SlicedTexture(ITexture underlying, ITimeSlice slice)
        {
            Underlying = underlying;
            Slice = slice;
        }

        public ARGB32 ColorAt(Vector<double> position)
        {
            var augmentedPosition = position.Concat(new[] { Slice.T }).ToVectorDD();
            var spacetimePosition = Slice.Arrow.ConvertBack(augmentedPosition);
            return Underlying.ColorAt(spacetimePosition);
        }
    }
}
