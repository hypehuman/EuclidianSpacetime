using MathNet.Numerics.LinearAlgebra;
using System.Linq;

namespace EuclidianSpacetime
{
    public interface ITimeSlice
    {
        public double T { get; }
        public ITimeArrow Arrow { get; }
        public Vector<double> ConvertBack(Vector<double> v);
    }

    public class TimeSlice : ITimeSlice
    {
        public double T { get; }
        public ITimeArrow Arrow { get; }

        public TimeSlice(double t, ITimeArrow arrow)
        {
            T = t;
            Arrow = arrow;
        }

        public Vector<double> ConvertBack(Vector<double> v)
        {
            var augmentedPosition = v.Concat(new[] { T }).ToVectorDD();
            var spacetimePosition = Arrow.ConvertBack(augmentedPosition);
            return spacetimePosition;
        }
    }
}
