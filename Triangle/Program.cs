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

            double sumAveragePerimeterRightTriangles = 0;
            double sumAverageAreaRightTriangles = 0;

            double sumAveragePerimeterIsoscelesTriangles = 0;
            double sumAverageAreaIsoscelesTriangles = 0;

            double countRightTriangle = 0;
            double countIsoscelesTriangle = 0;
            // Баловался 
            for (int i = 0; i <= 100; i++)
            {
                Console.Write("\r {0} %", i);
                System.Threading.Thread.Sleep(10);
            }

            Console.WriteLine();
            Console.Write("Введите количесто треугольников = ");
            
            Triangle[] triangle = new Triangle[Convert.ToInt32(Console.ReadLine())];

            // Заполнение точек
            
            for (int i = 0; i < triangle.Length; i++)
            {
                for (int j = 0; j < points.Length; j++)
                {
                    points[j] = new Point(gen.Next(0, 10), gen.Next(0, 10));
                }
                CheckPointsInMassive(points);

                triangle[i] = new Triangle(points);
            }

            // Заполнение ребер
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges.Length - 1 == i)
                    edges[i] = new Edge(points[i-2], points[i]);
                else
                    edges[i] = new Edge(points[i], points[i + 1]);
            }

            for (int i = 0; i < triangle.Length; i++)
            {
                // Вывожу точки
                PrintPoints(triangle[i].x);
                double[] LenghtEdges = FormedLength(triangle[i]);
                // Нахождение периметра
                double perimetrTriangle = Perimeter(LenghtEdges);
                         Console.WriteLine("Периметр треугольника = {0}", perimetrTriangle);
                // Нахождение площади
                double areaTriangle = Area(LenghtEdges, perimetrTriangle);
                        Console.WriteLine("Площадь треугольника = {0}", areaTriangle);
                // Нахождение прямоугольного треугольника
                int rightTriangle = RightTriangle(LenghtEdges, ref countRightTriangle);
                    if (rightTriangle == 1)
                    {
                        sumAveragePerimeterRightTriangles = sumAveragePerimeterRightTriangles + Perimeter(LenghtEdges);
                        sumAverageAreaRightTriangles = sumAverageAreaRightTriangles + Area(LenghtEdges, perimetrTriangle);
                    }
                
                // Нахождение равнобедренного треугольника
                int isoscelesTriangle = IsoscelesTriangle(LenghtEdges, ref countIsoscelesTriangle);
                    if (isoscelesTriangle == 1)
                    {
                        sumAveragePerimeterIsoscelesTriangles = sumAveragePerimeterIsoscelesTriangles + Perimeter(LenghtEdges);
                        sumAverageAreaIsoscelesTriangles = sumAverageAreaIsoscelesTriangles + Area(LenghtEdges, perimetrTriangle);
                    }
                Console.WriteLine(@"
       ………(\__/)
      ………(=’.’=) -- Улыбнулся! Это {0} треугольник
 ..…є[:]||||||||[:]э
      ………(`)_(`)", i);

                Console.WriteLine();
            }

            Console.WriteLine("Cредняя площадь всех прямоугольных треугольников = {0}", sumAverageAreaRightTriangles / countRightTriangle);
            Console.WriteLine("Cредний периметр всех прямоугольных треугольников = {0}", sumAveragePerimeterRightTriangles / countRightTriangle);

            Console.WriteLine("Cредняя площадь всех равнобедренных треугольников = {0}", sumAverageAreaIsoscelesTriangles / countIsoscelesTriangle);
            Console.WriteLine("Cредний периметр всех равнобедренных треугольников = {0}", sumAveragePerimeterIsoscelesTriangles / countIsoscelesTriangle);

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
            double[] sumTwoPoint = new double[3];
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
        static void CheckPointsInMassive(Point[] points)
        {
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
        // Метод, который формирует длину ребер и записывает их в один массив,
        // для удобной работы в дальнейшем
        static double[] FormedLength(Triangle triangle)
        {
            double[] edgeLength = new double[3];
            
            for (int i = 0; i < triangle.x.Length; i++)
            {
                if (i == (triangle.x.Length - 1))
                {
                    edgeLength[i] = Math.Sqrt(Math.Pow((triangle.x[i - 2].x - triangle.x[i].x),2) +
                                              Math.Pow((triangle.x[i - 2].y - triangle.x[i].y), 2));
                }
                else
                {
                    edgeLength[i] = Math.Sqrt(Math.Pow((triangle.x[i + 1].x - triangle.x[i].x), 2) +
                                              Math.Pow((triangle.x[i + 1].y - triangle.x[i].y), 2));
                }
            }
            Console.WriteLine("Длина ребер\n" + edgeLength[0] + "\n" + edgeLength[1] + "\n" + edgeLength[2] + "\n");
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
            return areaTriangle;
        }
        // Метод, который находит прямоугольный треугольник, по Пифагору
        static int RightTriangle(double[] findEdge, ref double countRightTriangle)
        {
            //Считаем по Пифагору
            Array.Sort(findEdge); // Грех не воспользоваться готовым методом
            if (findEdge[2] == Math.Sqrt(Math.Pow((findEdge[1]), 2) + Math.Pow((findEdge[0]), 2)))
            {
                Console.WriteLine("Triangle is right");
                        countRightTriangle += 1;
                return 1;
            }
            else
            {
                Console.WriteLine("Triangle is't right");
                return 0;
            }
        }
        // Метод, который находит равнобедренность каждого треугольника
        static int IsoscelesTriangle(double[] findEdge, ref double countIsoscelesTriangle)
        {
            if ((findEdge[0] == findEdge[1]) || (findEdge[2] == findEdge[1]) || (findEdge[2] == findEdge[0]))
            {
                Console.WriteLine("Triangle is Isosceles");
                        countIsoscelesTriangle += 1;
                return 1;
            }
            else
            {
                Console.WriteLine("Triangle is't Isosceles");
                return 0;
            }
        }
    }
}
// 1/ В равностороннем все стороны равны 
// 2/ Если две из трех сторон одинаковы, то это равнобедренный 
//треугольник(ВНИМАНИЕ! сумма этих двух сторон ДОЛЖНА БЫТЬ БОЛЬШЕ третьей)
// 3/ По теореме Пифагора - если сумма квадратов двух меньших сторон РАВНА квадрату большей стороны, 
// то треугольник прямоугольный.Кстати, если МЕНЬШЕ - то угол тупой, а если БОЛЬШЕ - то острый. 