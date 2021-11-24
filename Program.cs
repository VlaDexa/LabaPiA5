using LabaPiA5.Extensions;
using System;
using System.Linq;
using static System.Console;

namespace LabaPiA5
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var n in new int[3]{8, 10, 11})
                WriteLine("Команду 5 человек из {0} кандидатов можно составить {1} способами", n, Probability(n, 5));
            WriteLine();
            FirstTwo();
            WriteLine();
            FirstThree();
            WriteLine();
        }

        static readonly Random random = new Random();

        static private void FirstTwo()
        {
            var first = (random.Next(3, 6), random.Next(3, 6), random.Next(3, 6));
            var second = (random.Next(3, 6), random.Next(3, 6), random.Next(3, 6));
            WriteLine("Первый треугольник со сторонами {0}, {1}, {2}", first.Item1, first.Item2, first.Item3);
            WriteLine("Второй треугольник со сторонами {0}, {1}, {2}", second.Item1, second.Item2, second.Item3);
            WriteLine("{0} треугольник больше", (Geron(first.Item1, first.Item2, first.Item3) > Geron(second.Item1, second.Item2, second.Item3) ? "Первый" : "Второй"));
        }

        static private void FirstThree()
        {
            var first = (10, 1.0);
            var second = (9, 1.6);
            foreach (var i in new int[] { 1, 4 })
                WriteLine("{0} спортсмен преодолеет большее расстояние через {1} часов", Path(first.Item1, first.Item2, i) > Path(second.Item1, second.Item2, i) ? "Первый" : "Второй", i);
            var t = 1;
            while (Path(first.Item1, first.Item2, t) > Path(second.Item1, second.Item2, t)) t += 1;
            WriteLine("Второй догнал первого через {0} часа", t);
        }

        static private void SecondSix()
        {
            var A = RandomArray(7);
            var B = RandomArray(8);
            WriteLine("Массив А - [{0}]", string.Join(", ", A));
            WriteLine("Массив B - [{0}]", string.Join(", ", B));
            var max = A[0];
            var max_index = 0;
            for (var i = 0; i< A.Length; ++i)
            {
                if (max<A[i])
                {
                    max = A[i];
                    max_index = i;
                }
            }
            A = A.Delete(max_index);
            max = B[0];
            max_index = 0;
            for (var i = 0; i < B.Length; ++i)
            {
                if (max < B[i])
                {
                    max = B[i];
                    max_index = i;
                }
            }
            B = B.Delete(max_index);
            A = A.Concat(B).ToArray();
            WriteLine("Объединённый массив - [{0}]", string.Join(", ", A));
        }

        static double Path(double v, double a, double t)
        {
            return v * t + a * t * t / 2;
        }
        
        static long Probability(int n, int k)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }

        static long Factorial(int a)
        {
            var result = 1;
            for (var i = 2; i < a; ++i)
                result *= i;
            return result;
        }

        static double Geron(double a, double b, double c)
        {
            var p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        /// <summary>
        /// Массив случайной длины со случайными элементами
        /// Также выводит этот массив в консоль
        /// </summary>
        /// <returns>Созданый массив</returns>
        static public int[] RandomArray()
        {
            var length = random.Next(4, 7);
            var list = RandomArray(length);
            WriteLine("Рандомный массив - [{0}]", list);
            return list;
        }

        /// <summary>
        /// Массив длины length со случайными элементами
        /// </summary>
        /// <param name="length">Длина массива</param>
        /// <returns>Созданный массив</returns>
        static private int[] RandomArray(int length)
        {
            var list = new int[length];
            for (int i = 0; i < length; i++)
                list[i] = (random.Next(-20, 20));
            return list;
        }
    }

    namespace Extensions
    {
        public static class Extensions
        {
            /// <summary>
            /// Создаёт новый массив без элемента указанного в index
            /// </summary>
            /// <param name="init">Изначальный массив</param>
            /// <param name="index">Индекс значения, которое нужно исключить</param>
            /// <returns>Массив без элемента init[index]</returns>
            public static T[] Delete<T>(this T[] init, int index)
            {
                var ret = new T[init.Length - 1];
                var flag = false;
                for (var i = 0; i < init.Length; ++i)
                {
                    if (i == index)
                    {
                        flag = true;
                    }
                    ret[i - (flag ? 1 : 0)] = init[i];
                }
                return ret;
            }
        }
    }
}