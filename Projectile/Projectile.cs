using System;
using UtilityLibraries;

namespace Projectile
{
    public class Projectile
    {
        private double _mass;
        private double _c_air;
        public Vector position {get; set;}
        public Vector velocity {get; set;}
        public Vector acceleration {get; set;}
        public double mass
        {
            get
            {
                return _mass;
            }
            set
            {
                if(value>0)
                {
                    _mass = value;
                }
                else
                {
                    throw new ArgumentException("Negative or zero mass is not valid!");
                }
            }
        }
        public double c_air
        {
            get
            {
                return _c_air;
            }
            set
            {
                if(value>=0)
                {
                    _c_air = value;
                }
                else
                {
                    throw new ArgumentException("Negative constant of air resistance is not valid!");
                }
            }
        }

        public Projectile()
        {
            position = new Vector();
            velocity = new Vector();
            acceleration = new Vector();
        }
        public Projectile(double mass, double c_air=0)
        {
            position = new Vector();
            velocity = new Vector();
            acceleration = new Vector();

            this.mass = mass;
            this.c_air = c_air;
        }

        //changes acceleration to new value based on applied net force
        //discards old acceleration and applied forces.
        public void ApplyForce(Vector force)
        {
            this.acceleration = force/mass;
        }

        //changes position based on constant velocity over dTime
        //changes velocity based on constant acceleration over dTime
        public void Move(double dTime)
        {
            this.position += dTime*this.velocity;
            this.velocity += dTime*this.acceleration;
        }
    }
}