using System;
using UtilityLibraries;

namespace ProjectileN
{
    public class Projectile
    {
        private double _mass;
        private double _c_air;
        private bool immovable = false;
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
        public Projectile(double mass, double c_air=0, bool immovable = false) : this()
        {
            this.mass = mass;
            this.c_air = c_air;
            this.immovable = immovable;
        }

        public Projectile(double mass, Vector position, Vector velocity, Vector acceleration, double c_air = 0, bool immovable = false) : this(mass, c_air:c_air)
        {
            this.position = new Vector(position.X, position.Y, position.Z);
            this.velocity = new Vector(velocity.X, velocity.Y, velocity.Z);
            this.acceleration = new Vector(acceleration.X, acceleration.Y, acceleration.Z);
        }

        //changes acceleration to new value based on applied net force
        //discards old acceleration and applied forces.
        public void ApplyForce(Vector force)
        {
            acceleration = force/mass;
        }

        //changes position based on constant velocity over dTime
        //changes velocity based on constant acceleration over dTime
        public void Move(double dTime)
        {
            if (!immovable)
            {
                this.position = (dTime * this.velocity) + this.position;
                this.velocity = (dTime * this.acceleration) + this.velocity;
                //Console.WriteLine(this.position);
            }
        }
    }
}