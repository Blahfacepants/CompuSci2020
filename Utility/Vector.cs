using System;

namespace UtilityLibraries
{
    public struct Vector
    {
        private double x;
        private double y;
        private double z;

        public Vector(double xin, double yin, double zin):this()
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
        public double X {get; set;}
        public double Y {get; set;}
        public double Z {get; set;}
        public static Vector operator+ (Vector a, Vector b)
        {
            return new Vector(a.X+b.X, a.Y+b.Y, a.Z+b.Z);
        }

        public static Vector operator* (double c, Vector a)
        {
            return new Vector(a.X*c, a.Y*c, a.Z*c);
        }

        public static Vector operator* (Vector a, double c)
        {
            return c*a;
        }

        public static Vector operator- (Vector a, Vector b)
        {
            return new Vector(a.X-b.X, a.Y-b.Y, a.Z-b.Z);
        }

        override public string ToString()
        {
            return $"({x},{y},{z});";
        }
    }
}
