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
       // Метод слияние двух отсортированных массивов
        private void Merge(int[] array, int leftIndex, int midIndex, int rightIndex)
        {
            //Создание левого и правого подмассивов
            int lenOfLeftSubArr = midIndex - leftIndex + 1;
            int lenOfRightSubArr = rightIndex - midIndex;

            int[] leftSubArr = new int[lenOfLeftSubArr + 1];
            int[] rightSubArr = new int[lenOfRightSubArr + 1];

            //Заполнение этих массивов
            Parallel.For(0, lenOfLeftSubArr, i => leftSubArr[i] = array[leftIndex + i]);

            Parallel.For(1, lenOfRightSubArr + 1, i => rightSubArr[i - 1] = array[midIndex + i]);

            leftSubArr[^1] = int.MaxValue;
            rightSubArr[^1] = int.MaxValue;

            var i = 0;
            var j = 0;
            //Обьединение двух отсортированных массивов в один
                for (int k = leftIndex; k <= rightIndex; k++)
                {
                    if (leftSubArr[i] < rightSubArr[j])
                    {
                        array[k] = leftSubArr[i];
                        i++;
                    }
                    else
                    {
                        array[k] = rightSubArr[j];
                        j++;
                    }
                }
        }

        //сортировка слиянием
        private void MergeSort(int[] array, int leftIndex, int rightIndex)
        {
            if (leftIndex < rightIndex)
            {
                var middleIndex = (leftIndex + rightIndex) / 2;
                Parallel.Invoke
                (
                    () => MergeSort(array, leftIndex, middleIndex),
                    () => MergeSort(array, middleIndex + 1, rightIndex)
                );
                Merge(array, leftIndex, middleIndex, rightIndex);
            }
        }

        public int[] Sort(int[] array)
        {
            var result = array;
            //MergeSort(result, 0, result.Length - 1);
            ParallelMergeSort1(array);
            return result;
        }

        void Merge1(int[] arr, int l, int m, int r)
        {
            int[] left = new int[m - l + 1];
            int[] right = new int[r - m];

            for (int i1 = 0; i1 < left.Length; i1++)
            { 
                left[i1] = arr[l + i1];
            }
            for (int j1 = 0; j1 < right.Length; j1++)
                right[j1] = arr[m + 1 + j1];

            int i = 0, j = 0, k = l;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                    arr[k++] = left[i++];
                else
                    arr[k++] = right[j++];
            }

            while (i < left.Length)
                arr[k++] = left[i++];

            while (j < right.Length)
                arr[k++] = right[j++];
        }

        void ParallelMergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = l + (r - l) / 2;

                Parallel.Invoke(
                    () => ParallelMergeSort(arr, l, m),
                    () => ParallelMergeSort(arr, m + 1, r));

                Merge1(arr, l, m, r);
            }
        }
        public static void ParallelMergeSort1(int[] array, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex) return;

            int mid = startIndex + (endIndex - startIndex) / 2;

            Task leftTask = Task.Run(() => ParallelMergeSort1(array, startIndex, mid));
            Task rightTask = Task.Run(() => ParallelMergeSort1(array, mid + 1, endIndex));

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

        public static void ParallelMergeSort1(int[] array)
        {
            ParallelMergeSort1(array, 0, array.Length - 1);
        }
    }
}
