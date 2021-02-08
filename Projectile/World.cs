using System;
using UtilityLibraries;
using System.Collections.Generic;

namespace ProjectileN
{
    public class World
    {
        private double g_gravity = 9.8;//m/s2
        private bool verbose = true;//controls whether or not output is printed to the terminal after every tick
        public double time{get; set;}
        public List<Projectile> projectiles{get;}
        private double k_spring = 0;
        private double spring_length_unstretched = 0;
        private Vector spring_origin;
        public World(bool printOutputSetting=true, double startTime=0, double k_spring=0, double spring_length_unstretched=0)
        {
            verbose = printOutputSetting;
            time = startTime;
            this.k_spring = k_spring;
            this.spring_length_unstretched = spring_length_unstretched;
            spring_origin = new Vector(0,0,0);
        }
        public void AddProjectile(Projectile p)
        {
            projectiles.Add(p);
        }
        public void Tick(double dTime)
        {
            foreach(Projectile p in projectiles)
            {
                p.Move(dTime);
                p.ApplyForce(CalculateForce(p));
            }
        }
        private Vector CalculateForce(Projectile p)
        {
            Vector gravity = p.mass*g_gravity*(new Vector(0,0,-1));
            Vector air_resistance = (-p.velocity/p.velocity.GetMagnitude())*(p.c_air*(p.velocity.GetMagnitude() * p.velocity.GetMagnitude()));
            
            Vector displacement = (p.position - spring_origin)-(spring_length_unstretched*(p.position - spring_origin).GetUnitVector());            
            Vector spring_force = -k_spring*displacement;

            return gravity + air_resistance + spring_force;
        }
    }
}