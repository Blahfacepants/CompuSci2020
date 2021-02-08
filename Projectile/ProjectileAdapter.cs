using UtilityLibraries;
using Visualizer.Kinematics;
using System;
using DongUtility;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Projectile
{
    class ProjectileAdapter : IProjectile
    {
        Projectile projectile;

        //Passes the projectile object by reference (I hope)
        public ProjectileAdapter(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public Vector3D Position
        {
            get
            {
                return new Vector3D(projectile.position.X, projectile.position.Y, projectile.position.Z);
            }
        }
        

        public Color Color
        {
            get
            {
                return Color.FromRgb(255, 0, 0);
            }
        }


        public VisualizerControl.Shapes.Shape3D Shape
        {
            get
            {
                return new VisualizerControl.Shapes.Sphere3D();
            }
        }


        public double Size
        {
            get
            {
                return 1;
            }
        }
    }
}
