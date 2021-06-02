using EuclidianSpacetime;
using EuclidianSpacetime.Entities;
using EuclidianSpacetime.Rendering;
using EuclidianSpacetime.Textures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            var rendered = SpaceRenderer.Render(space, 0.25, di).ToList();
            var expectedNumSamples = di[0].NumSamples;
            Assert.AreEqual(expectedNumSamples, rendered.Count);
            var idValues = new HashSet<int>();
            var colors = new ARGB32[rendered.Count];
            foreach ((var id, var color) in rendered)
            {
                Assert.Equals(1, id.Length);
                var idVal = id[0];
                Assert.IsTrue(idVal < expectedNumSamples, "Sample index is too high");
                Assert.IsTrue(idValues.Add(idVal), "Sample indexes are not unique");
                colors[id[0]] = color;
            }
            Assert.Equals(colors[0], c1);
            Assert.Equals(colors[1], c1); // point should be more than one sample thick
            Assert.Equals(colors.Last(), c2);
            Assert.Equals(colors[expectedNumSamples / 2].A, 0); // between the two points should be transparent
        }
    }
}
