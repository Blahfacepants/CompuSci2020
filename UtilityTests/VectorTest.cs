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
            //These "trivial" getters and setters broke my Vector class for a while!
            //For some reason the default {get; set;} stuff wasn't working
            //I fixed it by putting the manual getters and setters back in.
            var vec1 = new Vector(1, 2, 3);
            Assert.AreEqual(1, vec1.X);
            Assert.AreEqual(2, vec1.Y);
            Assert.AreEqual(3, vec1.Z);
        }
        
        [TestMethod]
        public void TestToString()
        {
            var vec1 = new Vector(1, 2, 3);
            Assert.AreEqual("(1,2,3)", vec1.ToString());
        }

        [TestMethod]
        public void TestZero()
        {
            var vec1 = new Vector(0, 0, 0);
            Assert.AreEqual("(0,0,0)", vec1.ToString());
        }

        [TestMethod]
        public void TestNegative()
        {
            var vec1 = new Vector(-1, -1, -1);
            Assert.AreEqual("(-1,-1,-1)", vec1.ToString());
        }

        [TestMethod]
        public void TestLessThanOne()
        {
            var vec1 = new Vector(0.4, 0.2, 0.5);
            Assert.AreEqual("(0.4,0.2,0.5)", vec1.ToString());
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

        [TestMethod]
        public void TestConstantMultiplication()
        {
            var vec1 = new Vector(1, 1, 1);
            var vec2 = new Vector(2, 2, 2);

            Assert.AreEqual(vec2, 2*vec1);
        }

        [TestMethod]
        public void TestSetVector()
        {
            var vec1 = new Vector();
            vec1.SetVector(1, 1, 1);

            var vec2 = new Vector(1, 1, 1);

            Assert.AreEqual(vec2, vec1);
        }

        [TestMethod]
        public void TestMagnitude()
        {
            var vec1 = new Vector(2, 10, 11);

            Assert.AreEqual(15, vec1.GetMagnitude(), 0.05);
        }
    }
}
