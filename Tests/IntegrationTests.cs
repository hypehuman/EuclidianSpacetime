using EuclidianSpacetime;
using EuclidianSpacetime.Entities;
using EuclidianSpacetime.Rendering;
using EuclidianSpacetime.Textures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
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
            var (di, _) = SpaceRenderer.GetDimensionInfo(space, 1);
            Assert.AreEqual(0, di.Count);
            var rendered = SpaceRenderer.Render(space, 1, di).ToList();
            Assert.AreEqual(1, rendered.Count);
            var (sampleID, actualColor) = rendered[0];
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
            var (sampleID, actualColor) = rendered[0];
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
            var (di, bb) = SpaceRenderer.GetDimensionInfo(space, 1d / 8);
            Assert.AreEqual(1, di.Count);
            var expectedNumSamples = 11;
            Assert.AreEqual(expectedNumSamples, di[0].NumSamples);
            Assert.AreEqual(bb, new BoundingBox(x1.ToVectorDD(), x2.ToVectorDD()));
            var rendered = SpaceRenderer.Render(space, 1d / 4, di).ToList();
            Assert.AreEqual(expectedNumSamples, rendered.Count);
            var idValues = new bool[expectedNumSamples];
            var colors = new ARGB32[rendered.Count];
            foreach (var (id, color) in rendered)
            {
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
            }
            Assert.AreEqual(c1, colors[0]); // the leftmost sample should be at the first point
            Assert.AreEqual(c1, colors[1]); // point should be more than one sample thick
            Assert.AreEqual(0, colors[expectedNumSamples / 2].A); // between the two points should be transparent
            Assert.AreEqual(c2, colors.Last()); // the rightmost sample should be at the second point
        }

        [TestMethod]
        public void RenderSegmentInR1()
        {
            var a = 0.16796875;
            var b = -0.8828125;
            var c1 = new ARGB32(139, 72, 119, 136);
            ISpace space = new Space(1);
            space.AddEntity(new LineSegment(a.ToVectorDD(), b.ToVectorDD(), new SimpleTexture(c1)));
            ARGB32 sample(double x) => space.Sample(new SamplePoint(x.ToVectorDD(), 1d / 4));
            Assert.AreEqual(0, sample(-1).A);
            Assert.AreEqual(0, sample(b - 1d / 256).A); // Lines in R1 are solid objects, so there should be no thickening.
            Assert.AreEqual(c1, sample(b));
            Assert.AreEqual(c1, sample(0));
            Assert.AreEqual(c1, sample(a));
            Assert.AreEqual(0, sample(a + 1d / 256).A);
            Assert.AreEqual(0, sample(1).A);
        }

        [TestMethod]
        public void SliceSegmentInR1()
        {
            var a = 0.16796875;
            var b = -0.8828125;
            var c1 = new ARGB32(139, 72, 119, 136);
            ISpacetime st = new Spacetime(1, 1);
            st.AddEntity(new LineSegment(a.ToVectorDD(), b.ToVectorDD(), new SimpleTexture(c1)));
            var arrow = TimeArrow.Default(1);
            var di = TimeRenderer.GetDimensionInfo(new BoundingBox(-1d.ToVectorDD(), 1d.ToVectorDD()), 1d / 8);
            var spaces = TimeRenderer.Render(st, arrow, di).ToList();
            Assert.AreEqual(16, spaces.Count);
            var points = spaces.Select(st => ((IPoint)st.Entities.FirstOrDefault())).Select(p => p == null ? -1 : p.P.Count).ToList();
            Assert.Inconclusive(string.Join(",", points));
        }
    }
}
