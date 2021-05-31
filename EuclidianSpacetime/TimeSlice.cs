namespace EuclidianSpacetime
{
    public interface ITimeSlice
    {
        public double T { get; }
        public ITimeArrow Arrow { get; }
    }
}
