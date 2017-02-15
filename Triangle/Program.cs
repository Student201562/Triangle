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
            // Баловался 
            for (int i = 0; i <= 100; i++)
            {
                Console.Write("\r {0} %", i);
                System.Threading.Thread.Sleep(10);
            }
            Console.WriteLine();

            Random gen = new Random();
            Point[] points = new Point[3];
            Edge[] edges = new Edge[3];

            Console.Write("Введите количесто треугольников = ");
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
            // Вывожу один раз в качестве проверке
            PrintPoints(points);
            // Заполнение ребер
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges.Length - 1 == i)
                    edges[i] = new Edge(points[i-2], points[i]);
                else
                    edges[i] = new Edge(points[i], points[i + 1]);
            }
            // Формирование ребер и нахождение длины ребер, не обязательный метод, 
            // так как уже есть похожий FormedLength
            ShapeEdges(edges);

            for (int i = 0; i < triangle.Length; i++)
            {
                double[] findEdge = FormedLength(triangle[i]);
                // Нахождение периметра
                double perimetrTriangle = Perimeter(findEdge);
                // Нахождение площади
                double areaTriangle = Area(findEdge, perimetrTriangle);
                // Нахождение прямоугольного треугольника
                RightTriangle(findEdge);
                // Нахождение равнобедренного треугольника
                IsoscelesTriangle(findEdge);
                Console.WriteLine(@"
       ………(\__/)
      ………(=’.’=) -- Улыбнулся, это {0} треугольник
 ..…є[:]||||||||[:]э
      ………(`)_(`)", i);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        // Метод вывода точек на консоль
        static void PrintPoints(Point[] points)
        {
            Console.WriteLine("\nТочки!");
            for(int i = 0; i < points.Length; i++)
            {
                Console.Write(points[i].x + " " + points[i].y);
                Console.WriteLine();
            }
        }
        // Метод формирования ребер, проблема этого метода, он считает,
        // для одного триугольника, а метод FormedLength - считает для всех
        static void ShapeEdges(Edge[] edges)
        {
            sumTwoPoint = new double[3];
            //double[] edgesLength = new double[3];
            Console.WriteLine("Значение ребр");
            for (int i = 0; i < edges.Length; i++)
            {      
                if (i == (edges.Length - 1))
                {
                    sumTwoPoint[i] = Math.Sqrt(Math.Pow((edges[i-2].b.x - edges[i].a.x),2) +
                        Math.Pow((edges[i-2].b.y - edges[i].a.y), 2));
                                                                   // edgesLength[i] = sumTwoPoint[i];
                    Console.WriteLine(sumTwoPoint[i]);
                }
                else
                {
                    sumTwoPoint[i] = Math.Sqrt(Math.Pow((edges[i+1].b.x - edges[i].a.x), 2) +
                        Math.Pow((edges[i+1].b.y - edges[i].a.y), 2));
                                                                    //edgesLength[i] = sumTwoPoint[i];
                    Console.WriteLine(sumTwoPoint[i]);
                }
            }
            //return edgesLength; Хотел сделать функцию
        }
        // Метод, который формирует длину ребер и записывает их в один массив,
        // для удобной работы в дальнейшем
        static double[] FormedLength(Triangle triangle)
        {
            double[] edgeLength = new double[triangle.x.Length];
            
            for (int i = 0; i < triangle.x.Length; i++)
            {
                if (i == (triangle.x.Length - 1))
                {
                    edgeLength[i] = Math.Sqrt(Math.Pow(triangle.x[i - 2].x - triangle.x[i].x, 2) +
                                              Math.Pow(triangle.x[i - 2].y - triangle.x[i].y, 2));
                }
                else
                {
                    edgeLength[i] = Math.Sqrt(Math.Pow(triangle.x[i + 1].x - triangle.x[i].x, 2) +
                                              Math.Pow(triangle.x[i + 1].y - triangle.x[i].y, 2));
                }
            }
            return edgeLength;
        }
        // Метод, который находит периметр каждого треугольника, здесь мы передаем длину ребер, 
        // которую мы сформировали в предыдущем методе
        static double Perimeter(double[] findEdge)
        {
            double perimeterTriangle = 0;
                for (int j = 0; j < findEdge.Length; j++)
                {
                    perimeterTriangle += findEdge[j];
                }
                Console.WriteLine("Периметр треугольника = {0}", perimeterTriangle);
            return perimeterTriangle;
        }
        // Метод, который находит площадь для кадого треугольника, здесь мы передаем длину 
        //ребер и периметр. Зачем?(считаем по формуле полупериметра).
        static double Area(double[] findEdge, double perimetrtriangle)
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
        // Метод, который находит прямоугольный треугольник, по Пифагору
        static void RightTriangle(double[] findEdge)
        {
            //Считаем по Пифагору
            Array.Sort(findEdge); // Грех не воспользоваться готовым методом
            if (findEdge[2] == Math.Sqrt(Math.Pow((findEdge[1]), 2) + Math.Pow((findEdge[0]), 2)))
            {
                Console.WriteLine("Triangle is right");
            }
            else
            {
                Console.WriteLine("Triangle is't right");
            }
        }
        // Метод, который находит равнобедренность каждого треугольника
        static void IsoscelesTriangle(double[] findEdge)
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
