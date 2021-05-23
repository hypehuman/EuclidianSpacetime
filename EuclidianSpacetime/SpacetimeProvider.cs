namespace EuclidianSpacetime
{
    public static class SpacetimeProvider
    {
        public static ISpacetime NewSpacetime(int n) => new Spacetime(n);
    }
}
