using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Triangle
{
    class Program
    {
        static double[] sumTwoPoint;
        static void Main(string[] args)
        {
            Random gen = new Random();
            Point[] points = new Point[3];
            Edge[] edges = new Edge[3];

            Console.WriteLine("Введите количесто треугольников = ");
            Triangle[] triangle = new Triangle[Convert.ToInt32(Console.ReadLine())];
            // Заполнение точек
            for (int i = 0; i < triangle.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    points[j] = new Point(gen.Next(0, 10), gen.Next(0, 10));
                }
                triangle[i] = new Triangle(points);
            }
            PrintPoints(points);

            // Заполнение ребер
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges.Length - 1 == i)
                    edges[i] = new Edge(points[i-2], points[i]);
                else
                    edges[i] = new Edge(points[i], points[i + 1]);
            }

                // Формирование ребер и нахождение длины ребер
                double[] findEdge = ShapeEdges(edges, points);
                // Нахождение периметра
                double perimetrTriangle = Perimeter(triangle[i], findEdge);
                // Нахождение площади
                double areaTriangle = Area(triangle, findEdge, perimetrTriangle);
                // Нахождение прямоугольного треугольника
                RightTriangle(triangle, findEdge);
                // Нахождение равнобедренного треугольника
                IsoscelesTriangle(triangle, findEdge);
            
            Console.ReadKey();
        }
        static void PrintPoints(Point[] points)
        {
            for(int i = 0; i < points.Length; i++)
            {
                Console.Write(points[i].x + " " + points[i].y);
                Console.WriteLine();
            }
        }
        static double[] ShapeEdges(Edge[] edges, Point[] points)
        {
            sumTwoPoint = new double[3];

            double[] edgesLength = new double[3];
            Console.WriteLine("Значение ребр");
            for (int i = 0; i < edges.Length; i++)
            {      
                if (i == (edges.Length - 1))
                {
                    sumTwoPoint[i] = Math.Sqrt(Math.Pow((edges[i-2].b.x - edges[i].a.x),2) +
                        Math.Pow((edges[i-2].b.y - edges[i].a.y), 2));

                                                                    edgesLength[i] = sumTwoPoint[i];
                    Console.WriteLine(sumTwoPoint[i]);
                }
                else
                {
                    sumTwoPoint[i] = Math.Sqrt(Math.Pow((edges[i+1].b.x - edges[i].a.x), 2) +
                        Math.Pow((edges[i+1].b.y - edges[i].a.y), 2));

                                                                    edgesLength[i] = sumTwoPoint[i];
                    Console.WriteLine(sumTwoPoint[i]);
                }
            }
            return edgesLength;
        }
        static double Perimeter(Triangle[] triangle, double[] findEdge)
        {
            double perimeterTriangle = 0;

                for (int i = 0; i < findEdge.Length; i++)
                {
                    perimeterTriangle += findEdge[i];
                }
                Console.WriteLine("Периметр треугольника = {0}", perimeterTriangle);

            return perimeterTriangle;
        }
        static double Area(Triangle[] triangle, double[] findEdge, double perimetrtriangle)
        {
            double areaTriangle = 0;
            double semiPerimeter = perimetrtriangle/2;
            double helpValueFinal = 1; 
            for (int i = 0; i < findEdge.Length; i++)
            {
                double helpValue = semiPerimeter - findEdge[i];
                helpValueFinal *= helpValue;
                areaTriangle = Math.Sqrt(semiPerimeter*helpValueFinal);
            }
            Console.WriteLine("Площадь треугольника = {0}", areaTriangle);
            return areaTriangle;
        }
        static void RightTriangle(Triangle[] triangle, double[] findEdge)
        {
            //Считаем по Пифагору
            Array.Sort(findEdge);
            if (findEdge[2] == Math.Sqrt(Math.Pow((findEdge[1]), 2) + Math.Pow((findEdge[0]), 2)))
            {
                Console.WriteLine("Triangle is right");
            }
            else
            {
                Console.WriteLine("Triangle is't right");
            }
        }
        static void IsoscelesTriangle(Triangle[] triangle, double[] findEdge)
        {
            if ((findEdge[0] == findEdge[1]) || (findEdge[2] == findEdge[1]) || (findEdge[2] == findEdge[0]))
            {
                Console.WriteLine("Triangle is Isosceles");
            }
            else
            {
                Console.WriteLine("Triangle is't Isosceles");
            }
        }
    }
}
//static double[] EdgeLengthDetermination(Point[] points)
//{
//    double[] edgesLength = new double[3];
//    for (int i = 0; i < edgesLength.Length; i++)
//    {

//    }
//    return edgesLength;
//}