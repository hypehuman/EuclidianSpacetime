using EuclidianSpacetime.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EuclidianSpacetime
{
    public interface ISpace
    {
        IReadOnlyList<IEntity> Entities { get; }
        public void AddEntity(IEntity Entity);
        ARGB Sample(ISamplePoint samplePoint);
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

        public ARGB Sample(ISamplePoint samplePoint)
        {
            return Entities.Select(e => e.SampleColor(samplePoint)).FirstOrDefault(c => c.A > 0);
        }
    }
}
