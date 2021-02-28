using System;
using UtilityLibraries;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace ProjectileN
{
    public class Projectile
    {
        private double _mass;
        private double _c_air;
        private bool immovable = false;
        public List<double> mass_pts { get; set; }
        public List<Vector> position_pts { get; set; }
        public Vector position {get; set;}
        public Vector velocity {get; set;}
        public Vector acceleration {get; set;}
        public Shape ProjectileShape { get; }
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

            position_pts = new List<Vector>();
            mass_pts = new List<double>();
        }
        public Projectile(double mass, double c_air=0, bool immovable = false, Shape shape = Shape.Point, double radius=0, int ptcount=1) : this()
        {
            this.mass = mass;
            this.c_air = c_air;
            this.immovable = immovable;
            this.ProjectileShape = shape;
            ConfigDistribution(this.ProjectileShape, radius, ptcount);
        }

        public Projectile(double mass, Vector position, Vector velocity, Vector acceleration, double c_air = 0, bool immovable = false, Shape shape = Shape.Point, double radius = 0, int ptcount = 1)
            : this(mass, c_air:c_air, immovable:immovable, shape:shape, radius:radius, ptcount:ptcount)
        {
            this.position = new Vector(position.X, position.Y, position.Z);
            this.velocity = new Vector(velocity.X, velocity.Y, velocity.Z);
            this.acceleration = new Vector(acceleration.X, acceleration.Y, acceleration.Z);
        }

        /// <summary>
        /// 
        ///
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="radius"></param>
        /// <param name="count">disregarded for point, number of points for cube, number of shells for sphere</param>
        private void ConfigDistribution(Shape shape, double radius, int count)
        {
            Debug.WriteLine("shape configged");
            if(shape == Shape.Point)
            {
                position_pts.Clear();
                position_pts.Add(new Vector(0, 0, 0));
            }
            else if(shape == Shape.Cube)
            {
                int side = ((int)(Math.Cbrt(count)))/2;
                double space = radius / side;
                for (int x = -side; x <= side; x++)
                {
                    for (int y = -side; y <= side; y++)
                    {
                        for (int z = -side; z <= side; z++)
                        {
                            position_pts.Add(this.position+new Vector(x * space, y * space, z * space));
                        }
                    }
                }
            }
            else if(shape == Shape.Sphere)
            {
                //double space = radius / count;
                //double total_cross_section_area = 0;
                //double pt_spacing;
                //int num_pts;
                //double ring_r;
                //int ring_num_pts;
                //double dTheta;
                //double ga = (2.0 - 1.61803398874989484820458683436) * (2 * Math.PI);
                //for (double r = 0; r <= radius; r+=space)
                //{
                //    total_cross_section_area += Math.PI * (r*r);
                //}

                //for (double r=space; r<=radius; r+=space)
                //{
                //    //Non-functional ring method
                //    //Unfortunately does not work
                //    //if (r != 0)
                //    //{
                //    //    pt_spacing = space;
                //    //    for (double z = -r; z <= r; z += pt_spacing)
                //    //    {
                //    //        ring_r = Math.Sqrt((r * r) - (z * z));
                //    //        ring_num_pts = (int)((Math.PI * (ring_r)) / pt_spacing);
                //    //        dTheta = (2 * Math.PI) / ring_num_pts;

                //    //        for (double theta = 0; theta < 2 * Math.PI; theta += dTheta)
                //    //        {
                //    //            position_pts.Add(new Vector(Math.Cos(theta)*ring_r, Math.Sin(theta)*ring_r, z) + position);
                //    //        }
                //    //    }
                //    //}
                //    //else
                //    //{
                //    //    position_pts.Add(new Vector(0, 0, 0) + position);
                //    //}

                //    //Fibonacci sphere method
                //    //Adapted from code at https://bduvenhage.me/geometry/2019/07/31/generating-equidistant-vectors.html
                //    //Also doesn't work lol
                //    //num_pts = (int)(((Math.PI*(r*r))/total_cross_section_area) * count);
                //    //for (int i = 1; i <= num_pts; ++i)
                //    //{
                //    //    double lat = Math.Asin(-1.0 + 2.0 * i / (num_pts + 1));
                //    //    double lon = ga * i;

                //    //    double x = Math.Cos(lon) * Math.Cos(lat);
                //    //    double y = Math.Sin(lon) * Math.Cos(lat);
                //    //    double z = Math.Sin(lat);

                //    //    position_pts.Add(new Vector(x, y, z));
                //    //}


                //}
                //Making the cube
                int side = ((int)(Math.Cbrt(count))) / 2;
                double space = radius / side;
                for (int x = -side; x <= side; x++)
                {
                    for (int y = -side; y <= side; y++)
                    {
                        for (int z = -side; z <= side; z++)
                        {
                            position_pts.Add(this.position + new Vector(x * space, y * space, z * space));
                        }
                    }
                }
                //and removing points that fall outside sphere.
                foreach(Vector p in position_pts.ToArray())
                {
                    if(p.GetMagnitude() >= radius)
                    {
                        position_pts.Remove(p);
                    }
                }
            }
            //Debug.WriteLine(mass_pts.Sum());
            mass_pts.AddRange(Enumerable.Repeat(mass / position_pts.Count, position_pts.Count));
        }

        public void ConfigTetrahedron(List<Vector> vertices, int count)
        {
            List<Vector> face = vertices.GetRange(0, 3);


        }

        //private List<Vector> ConfigTriangle(List<Vector> vertices, int count)
        //{
        //    List<Vector> output = new List<Vector>();
        //    if(count > 3)
        //    {
        //        foreach(Vector v in vertices)
        //        {

        //        }
        //    }
        //}
        //changes acceleration to new value based on applied net force
        //discards old acceleration and applied forces.
        public void ApplyForce(Vector force)
        {
           // Debug.WriteLine(force);
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
                Debug.WriteLine(acceleration);
                Debug.WriteLine(position);
            }
        }
    }
}