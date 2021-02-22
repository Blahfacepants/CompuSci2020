using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;
using ProjectileN;

namespace UtilityTests
{
    [TestClass]
    public class WorldTest
    {        
        [TestMethod]
        public void TestGravity()
        {
            Vector zero = new Vector(0, 0, 0);
            var p = new ProjectileN.Projectile(mass:1, position:new Vector(0,10,0), velocity:zero, acceleration:zero);
            var p2 = new ProjectileN.Projectile(mass:1, immovable:true);
            var world = new World();

            var grav = world.Gravity(p, p2);
            var correct = new Vector(0, 0, -9.8);
            Assert.AreEqual(correct, grav);
        }

        [TestMethod]
        public void TestAirResistance()
        {
            var p = new ProjectileN.Projectile(mass: 1, velocity:new Vector(1,0,0), c_air:1.0, position:new Vector(0,0,0), acceleration:new Vector(0,0,0));
            var world = new World();

            var drag = world.AirResistance(p);
            var correct = new Vector(-1, 0, 0);
            Assert.AreEqual(correct, drag);
        }

        [TestMethod]
        public void TestSpring()
        {
            var p = new ProjectileN.Projectile(mass: 1, velocity: new Vector(0, 0, 0), c_air: 1.0, position: new Vector(0.2, 0, 0), acceleration: new Vector(0, 0, 0));
            var world = new World(k_spring:1);

            var spring_force = world.SpringForce(p, new Vector(0,0,0), 0);
            var correct = new Vector(-.2, 0, 0);
            Assert.AreEqual(correct, spring_force);
        }

        [TestMethod]
        public void TestAll()
        {
            var p = new ProjectileN.Projectile(mass: 1, velocity: new Vector(0, 1, 0), c_air: 1.0, position: new Vector(2, 0, 0), acceleration: new Vector(0, 0, 0));
            var world = new World(k_spring: 1, g_gravity:9.8);

            var f_net = world.CalculateForce(p, 0);
            var correct = new Vector(-2, -1, -9.8);
            Assert.AreEqual(correct, f_net);
        }
    }
}
