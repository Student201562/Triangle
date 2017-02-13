using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Triangle
    {
        public Point[] x;

        public Triangle(Point[] masPoint)
        {
            this.x = new Point[masPoint.Length];
            for (int i = 0; i < masPoint.Length; i++)
            {
                this.x[i] = masPoint[i];
            }
        }
    }
}
