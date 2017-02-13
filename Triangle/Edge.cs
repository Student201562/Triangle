using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Edge
    {
        public Point a;
        public Point b;

        public Edge(Point a, Point b)
        {
            this.a = a;
            this.b = b;
        }
    }
}
