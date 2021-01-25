using System;

namespace UtilityLibrary
{
    struct Vector
    {
        private double x;
        private double y;
        private double z;

        public Vector(double xin, double yin, double zin) : this()
        {
            SetVector(xin, yin, zin);
        }
        public double GetMagnitude()
        {
            return Math.Sqrt((x*x)+(y*y)+(z*z));
        }
        public void SetVector(double xin, double yin, double zin)
        {
            x = xin;
            y = yin;
            z = zin;
        }
        public double GetX()
        {
            return x;
        }

        public double GetY()
        {
            return y;
        }

        public double GetZ()
        {
            return z;
        }

        public static Vector operator+ (Vector a, Vector b)
        {
            return new Vector(a.GetX()+b.GetX(), a.GetY()+b.GetY(), a.GetZ()+b.GetZ());
        }

        public static Vector operator* (double c, Vector a)
        {
            return new Vector(a.GetX()*c, a.GetY()*c, a.GetZ()*c);
        }

        public void print()
        {
            Console.Write($"{x};{y};{z};{this.GetMagnitude()};");
        }
    }
}
