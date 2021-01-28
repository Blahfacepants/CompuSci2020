using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace UtilityTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProperties()
        {
            var vec1 = new Vector(1, 2, 3);
            Assert.AreEqual(1, vec1.X);
        }

        [TestMethod]
        public void TestAddition()
        {
            var vec1 = new Vector(1, 1, 1);
            var vec2 = new Vector(2, 3, 4);

            var correct = new Vector(3, 4, 5);

            Assert.AreEqual(correct, vec1+vec2);
        }

        [TestMethod]
        public void TestSubtraction()
        {
            var vec1 = new Vector(1, 1, 1);
            var vec2 = new Vector(2, 3, 4);

            var correct = new Vector(1, 2, 3);

            Assert.AreEqual(correct, vec2-vec1);
        }
    }
}
