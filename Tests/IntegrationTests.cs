using Microsoft.VisualStudio.TestTools.UnitTesting;
using EuclidianSpacetime;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void RenderEmptyR0()
        {
            ISpace space = new Space(0);
            var rendered = SpaceRenderer.Render(space, 1, 1).ToList();
            Assert.AreEqual(1, rendered.Count);
            (var sampleID, var color) = rendered[0];
            Assert.AreEqual(0, sampleID.Length);
            Assert.AreEqual(0, color.A);
        }
    }
}
