using EuclidianSpacetime.Entities;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Linq;

namespace EuclidianSpacetime
{
    public interface ISpace
    {
        /// <summary>
        /// The total number of space and/or time dimensions
        /// </summary>
        int N { get; }
        IReadOnlyList<IEntity> Entities { get; }
        public void AddEntity(IEntity Entity);
        ARGB32 Sample(ISamplePoint samplePoint);
        BoundingBox ComputeBoundingBox();
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

        public ARGB32 Sample(ISamplePoint samplePoint)
        {
            var sampledEntity = Entities.FirstOrDefault(e => e.ContainsSample(samplePoint));
            var result = sampledEntity?.Texture.ColorAt(samplePoint.P) ?? ARGB32.TransparentBlack;
            return result;
        }

        public BoundingBox ComputeBoundingBox()
        {
            var any = false;
            var min = Vector<double>.Build.Dense(N);
            var max = Vector<double>.Build.Dense(N);
            foreach (var entity in Entities)
            {
                var entityBB = entity.ComputeBoundingBox();
                for (var i = 0; i < N; i++)
                {
                    var entityMin = entityBB.Min[i];
                    var entityMax = entityBB.Max[i];
                    if (!any || entityMin < min[i])
                    {
                        min[i] = entityMin;
                    }
                    if (!any || entityMax > max[i])
                    {
                        max[i] = entityMax;
                    }
                }
                any = true;
            }
            return new BoundingBox(min, max);
        }
    }
}
