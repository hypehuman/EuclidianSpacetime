using EuclidianSpacetime;
using System;

namespace EuclidianSpacetimeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = SpacetimeProvider.NewSpacetime(3, 1);
            Console.WriteLine($"Spacetime: {st}");
        }
    }
}
