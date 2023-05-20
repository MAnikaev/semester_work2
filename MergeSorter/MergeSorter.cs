using SortRequirement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSorter
{   
    public class MergeSorter: IArraySorter
    {
        public int[] Sort(int[] array)
        {
            var result = array;
            ParallelMergeSort(array);
            return result;
        }
        public static void ParallelMergeSort(int[] array, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex) return;

            int mid = startIndex + (endIndex - startIndex) / 2;

            Task leftTask = Task.Run(() => ParallelMergeSort(array, startIndex, mid));
            Task rightTask = Task.Run(() => ParallelMergeSort(array, mid + 1, endIndex));

            Task.WaitAll(leftTask, rightTask);

            // Merge the two sorted halves
            int leftIndex = startIndex;
            int rightIndex = mid + 1;

            while (leftIndex <= mid && rightIndex <= endIndex)
            {
                if (array[leftIndex] < array[rightIndex])
                {
                    leftIndex++;
                }
                else
                {
                    int value = array[rightIndex];
                    int i = rightIndex;
                    while (i > leftIndex)
                    {
                        array[i] = array[i - 1];
                        i--;
                    }
                    array[leftIndex] = value;
                    leftIndex++;
                    mid++;
                    rightIndex++;
                }
            }
        }

        public static void ParallelMergeSort(int[] array)
        {
            ParallelMergeSort(array, 0, array.Length - 1);
        }
    }
}
