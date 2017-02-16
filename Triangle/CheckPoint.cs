using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class CheckPoint
    {
        public Point[] checkMassive;

        public CheckPoint(Point[] points)
        {
            checkMassive = new Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                this.checkMassive[i] = points[i];
            }
        }

        public void CheckPointsInMassive(Point[] points)
        {
            Random gen = new Random();
                if (points[0] == points[1] && points[0] == points[1])
                {
                    points[0].x += gen.Next(1, 5);
                    points[0].y += gen.Next(6, 9);
                }
                if (points[1] == points[2] && points[1].y == points[2].y)
                {
                    points[1].x += gen.Next(9, 14);
                    points[1].y += gen.Next(15, 19);
                }
                if (points[0].x == points[2].x && points[0].y == points[2].y)
                {
                    points[1].x += gen.Next(19, 23);
                    points[1].y += gen.Next(24, 27);
                }
        }
    }
}
