using EuclidianSpacetime.Entities;
using System.Collections.Generic;

namespace EuclidianSpacetime
{
    public interface ISpace
    {
    }

    internal class Space : ISpace
    {
        public int N { get; }
        public IEnumerable<IEntity> Entities { get; }

        public Space(int n, IEnumerable<IEntity> entities)
        {
            N = n;
            Entities = entities;
        }
    }
}
