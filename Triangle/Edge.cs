using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Edge
    {
        public Point pointA;
        public Point pointB;

        public Edge(Point a, Point b)
        {
            this.pointA = a;
            this.pointB = b;
        }
        public double GetLenghteEdge()
        {
            return Math.Sqrt(Math.Pow((pointB.x - pointA.x),2) + Math.Pow((pointB.y - pointA.y),2));
        }
    }
}
