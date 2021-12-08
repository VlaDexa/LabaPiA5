using LabaPiA5.Extensions;
using System;
using System.Linq;
using static System.Console;

namespace LabaPiA5
{
    class Program
    {
        static void Main()
        {
            foreach (var n in new int[3] { 8, 10, 11 })
                WriteLine("Команду 5 человек из {0} кандидатов можно составить {1} способами", n, Probability(n, 5));
            WriteLine();
            FirstTwo();
            WriteLine();
            FirstThree();
            WriteLine();
            SecondSix();
            WriteLine();
            SecondSeven();
            WriteLine();
            SecondEight();
            WriteLine();
            ThirdSix();
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
            for (var i = 0; i < A.Length; ++i)
            {
                if (max < A[i])
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

        /*В массив В размера 4 × 5 вставить после строки, содержащей максимальное количество положительных элементов,
        столбец массива С размера 5 × 6, содержащий максимальное количество положительных элементов.
        Определение количества положительных элементов в заданной строке (или столбце) матрицы осуществить в методе. */
        static private void SecondSeven()
        {
            var B = RandomMatrix(4, 5);
            PrintMatrix("Изначальная матрица B - ", B);
            var C = RandomMatrix(5, 6);
            PrintMatrix("Изначальная матрица C - ", C);
            var max_row = 0;
            var max_num = 0;
            for (var i = 0; i < 5; ++i)
            {
                var x = CountPositiveRows(B, i);
                if (max_num < x)
                {
                    max_num = x;
                    max_row = i;
                }
            }
            var max_col = 0;
            max_num = 0;
            for (var i = 0; i < 5; ++i)
            {
                var x = CountPositiveColumns(C, i);
                if (max_num < x)
                {
                    max_num = x;
                    max_col = i;
                }
            }
            var new_matrix = new int[6, 4];
            var after = false;
            for (var i = 0; i < 5; ++i)
                for (var j = 0; j < 4; ++j)
                    if (after)
                        new_matrix[i + 1, j] = B[i, j];
                    else
                    {
                        if (i == max_row + 1)
                        {
                            for (var z = 0; z < 4; ++z)
                                new_matrix[i, z] = C[max_col, z];
                            after = true;
                            new_matrix[i + 1, j] = B[i, j];
                        }
                        else
                            new_matrix[i, j] = B[i, j];
                    }
            PrintMatrix("Новая матрица - ", new_matrix);
        }

        /*Упорядочить по возрастанию элементы массивов А размера 9 и В размера 11,
        расположенные после максимального элемента.
        Упорядочение части массива, начинающейся элементом с заданным индексом, осуществить в методе.*/
        static private void SecondEight()
        {
            var A = RandomArray(9);
            // Вывести массив А
            WriteLine("Массив А - [{0}]", string.Join(", ", A));
            var B = RandomArray(11);
            // Вывести массив В
            WriteLine("Массив В - [{0}]", string.Join(", ", B));
            // Найти максимальный элемент в массиве A
            var max = A[0];
            var max_index = 0;
            for (var i = 0; i < A.Length; ++i)
                if (max < A[i])
                {
                    max = A[i];
                    max_index = i;
                }
            // Найти максимальный элемент в массиве B
            var max_B = B[0];
            var max_index_B = 0;
            for (var i = 0; i < B.Length; ++i)
                if (max_B < B[i])
                {
                    max_B = B[i];
                    max_index_B = i;
                }

            // Сортировка массива А
            A = SortAfter(A, max_index);
            // Сортировка массива В
            B = SortAfter(B, max_index_B);
            // Вывести отсортированные массивы
            WriteLine("Отсортированный массив А - [{0}]", string.Join(", ", A));
            WriteLine("Отсортированный массив В - [{0}]", string.Join(", ", B));
        }

        private delegate int FindMax(int[,] array);

        /*
        Поменять местами столбец, содержащий максимальный элемент на главной диагонали заданной квадратной матрицы,
        со столбцом, содержащим максимальный элемент в первой строке матрицы. Для замены столбцов использовать метод.
        Для поиска соответствующих максимальных элементов использовать делегата*/
        static private void ThirdSix()
        {
            var length = random.Next(3, 6);
            var A = RandomMatrix(length, length);
            PrintMatrix("Матрица A - ", A);
            static int at_main(int[,] array)
            {
                var max = array[0, 0];
                var max_index = 0;
                for (var i = 0; i < array.GetLength(0); ++i)
                    if (max < array[i, i])
                    {
                        max = array[i, i];
                        max_index = i;
                    }
                return max_index;
            }
            static int at_first(int[,] array)
            {
                var max = array[0, 0];
                var max_index = 0;
                for (var i = 0; i < array.GetLength(0); ++i)
                    if (max < array[0, i])
                    {
                        max = array[0, i];
                        max_index = i;
                    }
                return max_index;
            }
            PrintMatrix("Итоговая матрица - ", SwitchColumn(A, at_main, at_first));
        }

        static private int[,] SwitchColumn(int[,] A, FindMax at_main, FindMax at_first)
        {
            // Копировать A в новую матрицу
            var new_matrix = new int[A.GetLength(0), A.GetLength(1)];
            for (var i = 0; i < A.GetLength(0); ++i)
                for (var j = 0; j < A.GetLength(1); ++j)
                    new_matrix[i, j] = A[i, j];
            var max_main = at_main(A);
            var max_first = at_first(A);
            // Поменять местами столбец max_main и max_first
            for (var i = 0; i < A.GetLength(0); ++i)
            {
                new_matrix[i, max_main] = A[i, max_first];
                new_matrix[i, max_first] = A[i, max_main];
            }
            return new_matrix;
        }

        static private int[] SortAfter(int[] array, int idx)
        {
            var new_array = new int[array.Length];
            var sorted_bit = array.Skip(idx + 1).OrderBy(x => x);
            for (var i = 0; i < idx + 1; ++i)
                new_array[i] = array[i];
            for (var i = idx + 1; i < array.Length; ++i)
                new_array[i] = sorted_bit.ElementAt(i - (idx + 1));
            return new_array;
        }

        static private int CountPositiveRows(int[,] matrix, int row)
        {
            var count = 0;
            for (var i = 0; i < matrix.GetLength(1); ++i)
                if (matrix[row, i] > 0)
                    count++;
            return count;
        }

        static private int CountPositiveColumns(int[,] matrix, int column)
        {
            var count = 0;
            for (var i = 0; i < matrix.GetLength(0); ++i)
                if (matrix[i, column] > 0)
                    count++;
            return count;
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
        /// Создает матрицу с заданной длиной и случайными значениями
        /// </summary>
        /// <param name="n">Количество столбцов</param>
        /// <param name="m">Количество рядов</param>
        /// <returns>Созданая матрица</returns>
        static private int[,] RandomMatrix(int n, int m)
        {
            int[,] matrix = new int[m, n];
            for (int j = 0; j < n; j++)
                for (int i = 0; i < m; i++)
                    matrix[i, j] = random.Next(-20, 20);
            return matrix;
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
                list[i] = random.Next(-20, 20);
            return list;
        }

        /// <summary>
        /// Выводит в консоль матрицу
        /// </summary>
        /// <param name="matrix">Матрица, которую нужно напечатать</param>
        static private void PrintMatrix(int[,] matrix)
        {
            WriteLine("[");
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            for (int i = 0; i < n; ++i)
            {
                Write("\t[{0}", matrix[i, 0]);
                for (int j = 1; j < m; ++j)
                    Write(", {0}", matrix[i, j]);
                WriteLine("],");
            }
            WriteLine(']');
        }

        /// <summary>
        /// Выводит в консоль сначала сообщение start, потом матрицу
        /// </summary>
        /// <param name="start">Сообщение с которого надо начать вывод</param>
        /// <param name="matrix">Матрица, которую необходимо вывести</param>
        static public void PrintMatrix(string start, int[,] matrix)
        {
            Write(start);
            PrintMatrix(matrix);
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
                for (var i = 0; i < index; ++i)
                    ret[i] = init[i];
                for (var i = index + 1; i < init.Length; ++i)
                    ret[i - 1] = init[i];
                return ret;
            }
        }
    }
}
