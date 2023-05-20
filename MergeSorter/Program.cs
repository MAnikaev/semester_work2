using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for(int i = 0; i < 100; i++)
            {
                var ad = IsSorted(new MergeSorter().Sort(GenerateArray()));
                if(ad == false)
                    Console.WriteLine(ad);
            }
        }

        private static int[] GenerateArray()
        {
            var random = new Random();
            var result = new int[111];
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = random.Next(1000);
            }
            return result;
        }

        private static bool IsSorted(int[] array)
        {
            for(int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
