using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Triangle
    {
        public Point[] points;
        public Edge[] edges;

        public Triangle(Point[] masPoint, Edge[] masEdges)
        {
            this.points = new Point[masPoint.Length];
            this.edges = new Edge[masEdges.Length];
            for (int i = 0; i < masPoint.Length; i++)
            {
                this.points[i] = masPoint[i];
                this.edges[i] = masEdges[i];
            }
        }
        public void CheckPointsInMassive(Point[] points)
        {
            //TODO: проверить лежат ли они на одной прямой, если да, то исключить этот вариант
            Random gen = new Random();
            if (points[0] == points[1])
            {
                points[0].x += gen.Next(1, 5);
                points[0].y += gen.Next(6, 9);
            }
            if (points[1] == points[2])
            {
                points[1].x += gen.Next(9, 14);
                points[1].y += gen.Next(15, 19);
            }
            if (points[0] == points[2])
            {
                points[1].x += gen.Next(19, 23);
                points[1].y += gen.Next(24, 27);
            }
        }
        public bool RightTriangle(double[] getLengthEdge)
        {
            //Считаем по Пифагору
            Array.Sort(getLengthEdge); // Грех не воспользоваться готовым методом
            if (getLengthEdge[2] == Math.Sqrt(Math.Pow((getLengthEdge[1]), 2) + Math.Pow((getLengthEdge[0]), 2)))
            {
                Console.WriteLine("Triangle is right");
                return true;
            }
            else
            {
                Console.WriteLine("Triangle is't right");
                return false;
            }
        }
        public bool IsoscelesTriangle(double[] getLengthEdge)
        {
            if ((getLengthEdge[0] == getLengthEdge[1]) || (getLengthEdge[2] == getLengthEdge[1]) || (getLengthEdge[2] == getLengthEdge[0]))
            {
                Console.WriteLine("Triangle is Isosceles");
                return true;
            }
            else
            {
                Console.WriteLine("Triangle is't Isosceles");
                return false;
            }
        }
        public double Perimeter(double[] getLengthEdge)
        {
            double perimeterTriangle = 0;
            for (int j = 0; j < getLengthEdge.Length; j++)
            {
                perimeterTriangle += getLengthEdge[j];
            }
            return perimeterTriangle;
        }
        public double Area(double[] getLengthEdge, double perimetrTriangle)
        {
            double areaTriangle = 0;
            double semiPerimeter = perimetrTriangle / 2;
            double valuesSemiperimetr = 1;
            for (int i = 0; i < getLengthEdge.Length; i++)
            {
                double helpValue = semiPerimeter - getLengthEdge[i];
                valuesSemiperimetr *= helpValue;
                areaTriangle = Math.Sqrt(semiPerimeter * valuesSemiperimetr);
            }
            return areaTriangle;
        }
    }
}

