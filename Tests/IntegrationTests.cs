using EuclidianSpacetime;
using EuclidianSpacetime.Entities;
using EuclidianSpacetime.Rendering;
using EuclidianSpacetime.Textures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using static EuclidianSpacetime.Utilities;

namespace Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void RenderEmptyR0()
        {
            ISpace space = new Space(0);
            (var di, _) = SpaceRenderer.GetDimensionInfo(space, 1);
            Assert.AreEqual(0, di.Count);
            var rendered = SpaceRenderer.Render(space, 1, di).ToList();
            Assert.AreEqual(1, rendered.Count);
            (var sampleID, var actualColor) = rendered[0];
            Assert.AreEqual(0, sampleID.Length);
            Assert.AreEqual(0, actualColor.A);
        }

        [TestMethod]
        public void RenderPointInR0()
        {
            var expectedColor = new ARGB32(139, 72, 119, 136);
            ISpace space = new Space(0);
            space.AddEntity(new Point(EmptyVectorDD, new SimpleTexture(expectedColor)));
            var rendered = SpaceRenderer.Render(space, 1, Array.Empty<DimensionInfo>()).ToList();
            Assert.AreEqual(1, rendered.Count);
            (var sampleID, var actualColor) = rendered[0];
            Assert.AreEqual(0, sampleID.Length);
            Assert.AreEqual(expectedColor, actualColor);
        }

        [TestMethod]
        public void RenderPointsInR1()
        {
            var x1 = -0.58984375;
            var x2 = 0.86328125;
            var c1 = new ARGB32(139, 72, 119, 136);
            var c2 = new ARGB32(63, 67, 40, 233);
            ISpace space = new Space(1);
            space.AddEntity(new Point(x1.ToVectorDD(), new SimpleTexture(c1)));
            space.AddEntity(new Point(x2.ToVectorDD(), new SimpleTexture(c2)));
            (var di, var bb) = SpaceRenderer.GetDimensionInfo(space, 8);
            Assert.AreEqual(1, di.Count);
            Assert.AreEqual(bb, new BoundingBox(x1.ToVectorDD(), x2.ToVectorDD()));
            var expectedNumSamples = di[0].NumSamples;
            var idValues = new bool[expectedNumSamples];
            var colors = new ARGB32[expectedNumSamples];
            Parallel.ForEach(SpaceRenderer.Render(space, 0.25, di), x =>
            {
                (var id, var color) = x;
                Assert.AreEqual(1, id.Length);
                var idVal = id[0];
                try
                {
                    Assert.IsFalse(idValues[idVal], "Sample indexes are not unique");
                }
                catch (IndexOutOfRangeException)
                {
                    Assert.Fail("Sample index is out of range");
                }
                colors[id[0]] = color;
            });
            Assert.AreEqual(c1, colors[0]); // the leftmost sample should be at the first point
            Assert.AreEqual(c1, colors[1]); // point should be more than one sample thick
            Assert.AreEqual(0, colors[expectedNumSamples / 2].A); // between the two points should be transparent
            Assert.AreEqual(c2, colors.Last()); // the rightmost sample should be at the second point
        }
    }
}
