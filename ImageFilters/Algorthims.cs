using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    internal class Algorthims
    {
        public static void swap(ref byte a, ref byte b)
        {
            byte temp = a;
            a = b;
            b = temp;
        }
        public static byte[] QUICK_SORT(byte[] Array, int low, int high)
        {
            if (low < high)
            {
                int q = PARTITION(Array, low, high);
                QUICK_SORT(Array, low, q - 1);
                QUICK_SORT(Array, q + 1, high);
            }
            return Array;
        }
        public static int PARTITION(byte[] Array, int low, int high)
        {
            byte pivot = Array[high];
            int i = low-1;
            for (int j = low; j <= high-1; j++)
            {
                if (Array[j] < pivot)
                {
                    i++;
                    swap(ref Array[i],ref Array[j]);
                }
            }
            swap(ref Array[i+1],ref Array[high]);
            return i+1;
        }

        public static byte[]  countSort(byte[] array, int size)
        {
            byte[] output = new byte[size + 1];

            // Find the largest element of the array
            int max = array[0];
            for (int i = 1; i < size; i++)
            {
                if (array[i] > max)
                    max = array[i];
            }
            byte[] count = new byte[max + 1];

            // Initialize count array with all zeros.
            for (int i = 0; i < max; ++i)
            {
                count[i] = 0;
            }

            // Store the count of each element
            for (int i = 0; i < size; i++)
            {
                count[array[i]]++;
            }

            // Store the cummulative count of each array
            for (int i = 1; i <= max; i++)
            {
                count[i] += count[i - 1];
            }

            // Find the index of each element of the original array in count array, and
            // place the elements in output array
            for (int i = size - 1; i >= 0; i--)
            {
                output[count[array[i]] - 1] = array[i];
                count[array[i]]--;
            }

            // Copy the sorted elements into original array
            for (int i = 0; i < size; i++)
            {
                array[i] = output[i];
            }
            return array;
        }

    }
}
