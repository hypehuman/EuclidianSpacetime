namespace EuclidianSpacetime
{
    public interface ITimeSlice
    {
        public double T { get; }
        public ITimeArrow Arrow { get; }
    }

    public class TimeSlice : ITimeSlice
    {
        public double T { get; }
        public ITimeArrow Arrow { get; }
        public double RadiusOfInfinitesimal { get; }

        public TimeSlice(double t, ITimeArrow arrow)
        {
            T = t;
            Arrow = arrow;
        }
    }
}
