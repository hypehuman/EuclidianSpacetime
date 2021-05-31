using EuclidianSpacetime.Entities;
using System.Collections.Generic;

namespace EuclidianSpacetime
{
    public interface ISpace
    {
        IReadOnlyList<IEntity> Entities { get; }
        public void AddEntity(IEntity Entity);
    }

    public class Space : ISpace
    {
        private readonly List<IEntity> _entities = new();
        public int N { get; }

        public Space(int n)
        {
            N = n;
        }

        public IReadOnlyList<IEntity> Entities => _entities;

        public void AddEntity(IEntity entity)
        {
            _entities.Add(entity);
        }
    }
}
