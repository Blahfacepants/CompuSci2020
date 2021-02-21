using System;
using UtilityLibraries;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectileN
{
    public class World
    {
        private double g_gravity;//m/s2
        private bool verbose = true;//controls whether or not output is printed to the terminal after every tick
        public double time{get; set;}
        public List<Projectile> projectiles{get;}
        private double k_spring = 0;
        private double spring_length_unstretched = 0;
        private Vector spring_origin;
        private double time_limit;
        private bool stop_on_ground;
        private bool origin_spring;
        Dictionary<int, int> connections;
        public World(bool printOutputSetting = true, bool stop_on_ground = false,  double startTime=0, double k_spring=0, 
            double spring_length_unstretched=0, double g_gravity = 0, double time_limit=100, bool origin_spring=true)
        {
            verbose = printOutputSetting;
            time = startTime;
            this.k_spring = k_spring;
            this.spring_length_unstretched = spring_length_unstretched;
            spring_origin = new Vector(0,0,0);
            projectiles = new List<Projectile>();
            this.g_gravity = g_gravity;
            this.time_limit = time_limit;
            this.stop_on_ground = stop_on_ground;
            this.origin_spring = origin_spring;
            connections = new Dictionary<int, int>();
        }
        public World(Dictionary<int, int> proj_connections, bool printOutputSetting = true, bool stop_on_ground = false, double startTime = 0, double k_spring = 0,
            double spring_length_unstretched = 0, double g_gravity = 0, double time_limit = 100,
            bool connected_projectiles = false, bool origin_spring=true) : this(printOutputSetting, stop_on_ground, startTime, k_spring, spring_length_unstretched, g_gravity, time_limit, origin_spring)
        {
            connections = new Dictionary<int, int>(proj_connections);
            foreach(int key in connections.Keys.ToList())
            {
                connections.TryAdd(connections[key], key);
            }
        }
        public void AddProjectile(Projectile p)
        {
            this.projectiles.Add(p);
        }
        public bool Tick(double newTime)
        {
            foreach (Projectile p in projectiles)
            {
                p.Move(newTime - time);
                p.ApplyForce(CalculateForce(p, projectiles.IndexOf(p)));
                //if (p.velocity.GetMagnitude() >= 1) Debug.WriteLine(time);
            }
            time = newTime;
            if(time<time_limit && IsAboveGround(projectiles[0], stop_on_ground))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsAboveGround(Projectile p, bool on)
        {
            if (on)
            {
                return p.position.Z >= 0;
            }
            else
            {
                return true;
            }
        }
        public Vector CalculateForce(Projectile p, int id)
        {
            Vector gravity;
            try
            {
                gravity = Gravity(p, projectiles[connections[id]]);
            }
            catch(KeyNotFoundException e)
            {
                System.Console.WriteLine("connection not provided, applying 0 force");
                return new Vector(0,0,0);
            }
            //Vector air_resistance = AirResistance(p);
            //Vector spring_force = new Vector();
            //REMOVED FOR PERFORMANCE REASONS
            /* if (origin_spring && id==0)
            {
                spring_force = SpringForce(p, spring_origin, spring_length_unstretched);
            }

            if(connections.ContainsKey(id))
            { 
                spring_force += SpringForce(p, projectiles[connections[id]].position, spring_length_unstretched);
            }
            */
            return gravity;// + air_resistance + spring_force;
        }
        public Vector SpringForce(Projectile p, Vector spring_origin, double spring_length_unstretched)
        {
            double displacement = (p.position - spring_origin).GetMagnitude() - spring_length_unstretched;
            Vector spring_force = (-k_spring * displacement) * ((p.position - spring_origin).GetUnitVector());
            return spring_force;
        }
        public Vector AirResistance(Projectile p)
        {
            return (p.velocity.GetMagnitude() * p.velocity.GetMagnitude() * p.c_air) * ((-1.0 / p.velocity.GetMagnitude()) * p.velocity);
        }
        public Vector Gravity(Projectile p, Projectile p2, double G=6.67430e-11)
        {
            return G * ((p.mass * p2.mass) / ((p.position - p2.position).GetMagnitude() * (p.position - p2.position).GetMagnitude())) * (p2.position - p.position);
        }
    }
}