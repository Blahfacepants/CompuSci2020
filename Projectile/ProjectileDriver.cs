using ProjectileN;
using System;
using System.Collections.Generic;
using System.Text;
using UtilityLibraries;

namespace ProjectileN
{
    class ProjectileDriver
    {
        public static void Main(string[] args)
        {
            // Create a World object
            World world = new World(k_spring: 8, g_gravity: 0, spring_length_unstretched: 2, time_limit: 20);

            // Create Projectiles and set up the World 
            Projectile projectile = new Projectile(mass: 4.0, position: new Vector(-1, 1, -1), velocity: new Vector(5, -1, 3), acceleration: new Vector(0, 0, 0), c_air: 0);
            world.AddProjectile(projectile);
            //... and so forth
            var run = true;
            while (run)
            {
                run=world.Tick(world.time+0.1);
            }
        }
    }
}
