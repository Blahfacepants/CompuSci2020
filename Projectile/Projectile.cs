using System;
using UtilityLibraries;

namespace Projectile
{
    class Program
    {
        static void Main(string[] args)
        {
            Level3();
        }

        static void Level1()
        {
            Vector position = new Vector();
            Vector velocity = new Vector();
            Vector force = new Vector();

            double launch_magnitude = 5.0;//m/s
            double launch_angle_xz = Math.PI/4;//radians
            double mass = 4;//kg
            double gravity = 9.8;//m/s2

            //m, m/s, and m/s2
            //(x, y, z)
            (double, double, double) initial_position = (0.0, 0.0, 0.0);
            (double, double, double) initial_velocity = (launch_magnitude*Math.Cos(launch_angle_xz), 
                0.0, launch_magnitude*Math.Sin(launch_angle_xz));
            (double, double, double) initial_force = (0.0, 0.0, -mass*gravity);
            //start at the origin
            position.SetVector(initial_position.Item1, initial_position.Item2, initial_position.Item3);
            velocity.SetVector(initial_velocity.Item1, initial_velocity.Item2, initial_velocity.Item3);
            force.SetVector(initial_force.Item1, initial_force.Item2, initial_force.Item3);
            
            MainLoop(position, velocity, mass, time_increment:0.01);

        }
        
        static void Level2()
        {
            Vector position = new Vector();
            Vector velocity = new Vector();
            Vector force = new Vector();

            double launch_magnitude = 5.0;//m/s
            double launch_angle_xz = Math.PI/4;//radians
            double mass = 4;//kg
            double gravity = 9.8;//m/s2
            double c_air_resistance = 0.5;

            //m, m/s, and m/s2
            //(x, y, z)
            (double, double, double) initial_position = (0.0, 0.0, 0.0);
            (double, double, double) initial_velocity = (launch_magnitude*Math.Cos(launch_angle_xz), 
                0.0, launch_magnitude*Math.Sin(launch_angle_xz));
            (double, double, double) initial_force = (0.0, 0.0, -mass*gravity);
            //start at the origin
            position.SetVector(initial_position.Item1, initial_position.Item2, initial_position.Item3);
            velocity.SetVector(initial_velocity.Item1, initial_velocity.Item2, initial_velocity.Item3);
            force.SetVector(initial_force.Item1, initial_force.Item2, initial_force.Item3);
            
            MainLoop(position, velocity, mass, time_increment:0.01, c_air:c_air_resistance);
        }

        static void Level3()
        {
            Vector position = new Vector();
            Vector velocity = new Vector();
            Vector force = new Vector();

            double mass = 4;//kg
            double gravity = 9.8;//m/s2
            double c_air_resistance = 0.5;
            double spring_constant = 8.0;

            //m, m/s, and m/s2
            //(x, y, z)
            (double, double, double) initial_position = (-1.0, 1.0, -1.0);
            (double, double, double) initial_velocity = (5.0, -1.0, 3.0);
            (double, double, double) initial_force = (0.0, 0.0, -mass*gravity);

            position.SetVector(initial_position.Item1, initial_position.Item2, initial_position.Item3);
            velocity.SetVector(initial_velocity.Item1, initial_velocity.Item2, initial_velocity.Item3);
            force.SetVector(initial_force.Item1, initial_force.Item2, initial_force.Item3);
            
            MainLoop(position, velocity, mass, time_increment:0.01, time_limit:25.0, c_air:c_air_resistance, k_spring:spring_constant, stop_ground:false);
        }
        static bool grounded(double z, bool on=true)
        {
            if(on)
            {
                return z<0;
            }
            else
            {
                return false;
            }
        }
        private static void MainLoop(Vector position, Vector velocity, double mass, double g=-9.8, double c_air=0, double k_spring=0.0, double time_increment=0.1, double time_limit=100.0, bool stop_ground=true)
        {
            double time = 0.0;

            //acceleration can always be derived from force
            Vector acceleration = new Vector();
            Vector force = new Vector();
            Vector f_air = new Vector();
            Vector f_spring = new Vector();
            double displacement;
            Vector gravity = new Vector(0, 0, g*mass);

            //print headers
            Console.WriteLine("Time;x;y;z;Distance;vx;vy;vz;Speed;ax;ay;az;m_Acc");

            while(time<time_limit && !(grounded(position.GetZ(), on:stop_ground)))
            {
                //air resistance
                //C|v|^2 * (-1*unit vector for velocity)
                f_air = (velocity.GetMagnitude()*velocity.GetMagnitude()*c_air)*((-1.0/velocity.GetMagnitude())*velocity);

                //spring force
                //again using unit vector
                displacement = position.GetMagnitude() - 2;
                f_spring = (-k_spring*displacement)*((1.0/position.GetMagnitude())*position);

                force = gravity + f_air + f_spring;

                //print it all out
                Console.Write($"{time};");
                Console.Write(position);
                Console.Write(velocity);
                Console.Write(acceleration);
                Console.Write("\n");


                acceleration.SetVector(force.GetX()/mass, force.GetY()/mass, force.GetZ()/mass);
                //change in position according to velocity
                position = position + (time_increment*velocity);

                //change in velocity according to acceleration
                velocity = velocity + (time_increment*acceleration);

                time = time + time_increment;
            }
        }
    }
    // class Vector
    // {
    //     private double x;
    //     private double y;
    //     private double z;

    //     public Vector(double xin, double yin, double zin)
    //     {
    //         this.SetVector(xin, yin, zin);
    //     }
    //     public Vector(){}
    //     public double GetMagnitude()
    //     {
    //         return Math.Sqrt((x*x)+(y*y)+(z*z));
    //     }
    //     public void SetVector(double xin, double yin, double zin)
    //     {
    //         x = xin;
    //         y = yin;
    //         z = zin;
    //     }
    //     public double GetX()
    //     {
    //         return x;
    //     }

    //     public double GetY()
    //     {
    //         return y;
    //     }

    //     public double GetZ()
    //     {
    //         return z;
    //     }

    //     public static Vector operator+ (Vector a, Vector b)
    //     {
    //         return new Vector(a.GetX()+b.GetX(), a.GetY()+b.GetY(), a.GetZ()+b.GetZ());
    //     }

    //     public static Vector operator* (double c, Vector a)
    //     {
    //         return new Vector(a.GetX()*c, a.GetY()*c, a.GetZ()*c);
    //     }

    //     public void print()
    //     {
    //         Console.Write($"{x};{y};{z};{this.GetMagnitude()};");
    //     }
    // }
}