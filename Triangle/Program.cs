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
        static void Main(string[] args)
        {
            Random gen = new Random();
            Point[] points = new Point[3];
            Edge[] edges = new Edge[3];

            double sumPerimeterRightTriangles = 0;
            double sumAreaIsoscelesTriangles = 0;
            double countRightTriangle = 0;
            double countIsoscelesTriangle = 0;
            
            // Баловался
            for (int i = 0; i <= 100; i++)
            {
                Console.Write("\r {0} %", i);
                System.Threading.Thread.Sleep(10);
            }

            Console.Write("\nВведите количесто треугольников = ");
            
            Triangle[] triangles = new Triangle[Convert.ToInt32(Console.ReadLine())];
            
            for (int indexTriangle = 0; indexTriangle < triangles.Length; indexTriangle++)
            {
                                    Console.WriteLine("\nТреугольник = {0}",indexTriangle);
                RandomCoordinates(gen, points, triangles, indexTriangle, edges); // Генерация точек
                    PrintPoints(triangles[indexTriangle].points); // Выводит точки
                double[] getLengthEdge = EdgesLengthInTriangle(edges, points); // Расчет длин ребер треугольника
                
                //TODO: добавить для обычных треугольников реализовать метод (периметра и площади)
                if (triangles[indexTriangle].RightTriangle(getLengthEdge) == true)
                {
                                                countRightTriangle++;
                    sumPerimeterRightTriangles += triangles[indexTriangle].Perimeter(getLengthEdge);
                    Console.WriteLine("Периметр прямоугольного треугольника = {0}", sumPerimeterRightTriangles);
                }
                if (triangles[indexTriangle].IsoscelesTriangle(getLengthEdge) == true)
                {
                                                countIsoscelesTriangle++;
                        double perimetr = triangles[indexTriangle].Perimeter(getLengthEdge);
                    sumAreaIsoscelesTriangles += triangles[indexTriangle].Area(getLengthEdge, perimetr);
                    Console.WriteLine("Площадь равнобедренного треугольника = {0}", sumAreaIsoscelesTriangles);
                }
                Console.WriteLine("---------------==== ---------------");
            }

            Console.WriteLine("Количество прямоугольных треугольников = {0}. Средний периметр = {1}",countRightTriangle, sumPerimeterRightTriangles / countRightTriangle);
            Console.WriteLine("Количество равнобедренных треугольников = {0}. Средняя площадь = {1}",countIsoscelesTriangle, sumAreaIsoscelesTriangles / countIsoscelesTriangle);

            Console.ReadKey();
        }
        // Метод вывода точек на консоль
        static void PrintPoints(Point[] points)
        {
            Console.WriteLine("Точки");
            for(int i = 0; i < points.Length; i++)
            {
                Console.Write(points[i].x + " " + points[i].y);
                Console.WriteLine();
            }
        }
        static void RandomCoordinates(Random gen, Point[] points, Triangle[] triangles, int indexTriangle, Edge[] edges)
        {
            for (int j = 0; j < points.Length; j++)
            {
                points[j] = new Point(gen.Next(0, 10), gen.Next(0, 10));
            }
            triangles[indexTriangle] = new Triangle(points,edges);

            triangles[indexTriangle].CheckPointsInMassive(points);
            triangles[indexTriangle] = new Triangle(points, edges);
        }
        static double[] EdgesLengthInTriangle(Edge[] edges, Point[] points)
        {
            //TODO: вернуться к этому моменту
            double[] getLengthEdge = new double[edges.Length]; // здесь длина ребер 

            Console.WriteLine("Длина ребер");
            for (int i = 0; i < edges.Length; i++)
            {
                if (i == (edges.Length-1))
                    edges[i] = new Edge(points[i], points[0]);
                else
                    edges[i] = new Edge(points[i], points[i + 1]);

                getLengthEdge[i] = edges[i].GetLenghteEdge();
                Console.WriteLine("Ребро {0} = {1}",i, getLengthEdge[i]);
            }
            return getLengthEdge;
        }
    }
}
// 1/ В равностороннем все стороны равны 
// 2/ Если две из трех сторон одинаковы, то это равнобедренный 
//треугольник(ВНИМАНИЕ! сумма этих двух сторон ДОЛЖНА БЫТЬ БОЛЬШЕ третьей)
// 3/ По теореме Пифагора - если сумма квадратов двух меньших сторон РАВНА квадрату большей стороны, 
// то треугольник прямоугольный.Кстати, если МЕНЬШЕ - то угол тупой, а если БОЛЬШЕ - то острый. 