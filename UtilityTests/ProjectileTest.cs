using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;
using ProjectileN;

namespace UtilityTests
{
    [TestClass]
    public class ProjectileTest
    { 

        [TestMethod]
        public void TestConstMove()
        {
            var p = new Projectile(mass: 1, position: new Vector(0, 0, 0), velocity: new Vector(1, 0, 0), acceleration: new Vector(0, 0, 0));
            p.Move(1);

            var correct = new Projectile(mass: 1, position: new Vector(1, 0, 0), velocity: new Vector(1, 0, 0), acceleration: new Vector(0, 0, 0));

            Assert.AreEqual(correct.position, p.position);
        }
    }
}
