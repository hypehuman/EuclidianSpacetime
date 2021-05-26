using EuclidianSpacetime;
using EuclidianSpacetime.Entities;
using EuclidianSpacetime.Textures;
using System;
using static EuclidianSpacetime.Utilities;

namespace EuclidianSpacetimeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpacetime st = new Spacetime(2, 1);
            var a = ToVectorDD(0, 0);
            var b = ToVectorDD(1, 1);
            st.AddEntity(new LineSegment(a, b, new LinearSpectralTexture(a, b)));
            Console.WriteLine($"Spacetime: {st}");
        }
    }
}
