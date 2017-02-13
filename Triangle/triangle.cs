using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Triangle
    {
        public Point x;
        public Point y;
        public Point z;

        public Triangle(Point[] masPoint)
        {
            this.x = masPoint[0];
            this.y = masPoint[1];
            this.z = masPoint[2];
        }
    }
}
