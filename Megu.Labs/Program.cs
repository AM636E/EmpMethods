using System;
using System.Collections.Generic;
using System.Linq;

namespace Megu.Labs
{
    public class Program
    {
        private const int Count = 14 - 3 + 10;
        
        private static Random _random = new Random();
        
        /// <summary>
        /// Generate a sequence of pseudo random numbers.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private static int[] GenerateSequence(int count)
        {
            return  Enumerable.Range(0, count).Select(_ => _random.Next(1, 6)).ToArray();
        }

        private static void DisplayData(int[] data)
        {
            foreach (var item in data)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Calculate average as summ of all elements divided by the number of elements.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static double CalculateAverage(int[] arr)
        {
            return arr.Select(it => (double) it).Average();
        }

        /// <summary>
        /// Calculate Harmonic Average as power of -1 of summ of elements in -1 power.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static double CalculateHarmonicAverage(int[] arr)
        {
            if (arr.Length == 0) return 0;
            
            return arr.Length / (arr.Sum(it => 1 / (double) it));
        }

        /// <summary>
        /// Calculate quadratic average as square root of average of squares of elements.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static double CalculateQuadraticAverage(int[] arr)
        {
            if (arr.Length == 0) return 0;

            return (double)Math.Sqrt(arr.Sum(it => it*it) / (double)arr.Length);
        }

        /// <summary>
        /// Calculate Geometric average as power of 1/N of the product of the sequence.
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static double CalculateGeometricAverage(int[] arr)
        {
            if (arr.Length == 0) return 0;

            // A little trick tibetian monks taught me.
            // Very dangerous as a great loss of precision may occur.
            var p = arr.Aggregate(1M, (a, b) => a * b);
            return Math.Pow((double)p, 1.0 / arr.Length);
        }

        private static decimal FindMedian(int[] arr)
        {
            if (arr.Length == 0) return -1;
            if (arr.Length == 1) return arr[0];
            if (arr.Length % 2 == 0)
            {
                return (arr[arr.Length / 2] + arr[arr.Length / 2 + 1]) / 2.0M;
            }

            return arr[arr.Length / 2 + 1];
        }
        
        public static int[] FindModas(int[] arr)
        {
            return
                // Form { Element, Element Count } Type
                arr.Select(it => new {El = it, Count = arr.Count(e => e == it)})
                // Grouped by Element Count
                .GroupBy(it => it.Count)
                // Key is Count
                // Order by highest to lowest by Element Count.
                .OrderByDescending(it => it.Key)
                // Select elements with highest count.
                // First is Grouping. Key is Element Count. Value is array of arrays of elements with that count.
                .First()
                // Group by element and select first.
                .GroupBy(it => it.El).Select(it => it.First().El).ToArray();
        }
        
         /// <summary>
        /// Maps modas to data sequence.
        /// </summary>
        private static Dictionary<int[], int[]> modasToArr = new Dictionary<int[], int[]>
        {
        [new[] {1}] = new [] { 1, 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {1, 4}] = new [] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {1, 4}] = new [] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {1, 4, 5}] = new [] { 1, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5  }, 
        [new[] {3}] = new [] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 5, 5  }, 
        [new[] {4}] = new [] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {3,4, 5}] = new [] { 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5  }, 
        [new[] {1, 2, 3}] = new [] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 5, 5, 5, 5  }, 
        [new[] {4}] = new [] { 1, 1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5  }, 
        [new[] {3}] = new [] { 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {1}] = new [] { 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5  }, 
        [new[] {3}] = new [] { 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5  }, 
        [new[] {3}] = new [] { 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5  }, 
        [new[] {3}] = new [] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5  }, 
        [new[] {4}] = new [] { 1, 1, 1, 1, 1, 2, 2, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5  }, 
        [new[] {5}] = new [] { 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 4, 4, 5, 5, 5, 5, 5, 5, 5  }, 
        [new[] {1}] = new [] { 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {3}] = new [] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5  }, 
        [new[] {5}] = new [] { 1, 1, 1, 2, 2, 2, 2, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5  }, 
        [new[] {5}] = new [] { 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5  }, 
        [new[] {2}] = new [] { 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5  }, 
        };
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Вихідні Дані:");
            
            var data = GenerateSequence(Count);
           
            DisplayData(data);

            Console.WriteLine("Відсортовані Дані:");
            DisplayData(data.OrderBy(it => it).ToArray());

            Console.WriteLine("Середнє Арифметичне:");
            Console.WriteLine($"{CalculateAverage(data):0.0000}");

            Console.WriteLine("Ререднє Гармонічне:");
            Console.WriteLine($"{CalculateHarmonicAverage(data):0.0000}");

            Console.WriteLine("Середнє Гармонічне:");
            Console.WriteLine($"{CalculateQuadraticAverage(data):0.0000}");

            Console.WriteLine("Середнє Геометричне:");
            Console.WriteLine($"{CalculateGeometricAverage(data):0.0000}");
            
            Console.WriteLine("Медіана:");
            Console.WriteLine($"{FindMedian(data.OrderBy(_ => _).ToArray()):0.0000}");
            
            Console.WriteLine("Моди:");
            Console.WriteLine(string.Join(", ", FindModas(data)));
            
            foreach (var kv in modasToArr)
            {
                var modas = FindModas(kv.Value);
                
                Console.WriteLine($"Дані: {string.Join(",", kv.Value)}. Очікувані моди: {string.Join(",", kv.Key)}. Отримані моди: {string.Join(",", modas)}");
            }
        }
    }
}