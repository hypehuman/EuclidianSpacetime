namespace EuclidianSpacetime
{
    public static class SpacetimeProvider
    {
        public static ISpacetime NewSpacetime(int n, int c) => new Spacetime(n, c);
    }
}
