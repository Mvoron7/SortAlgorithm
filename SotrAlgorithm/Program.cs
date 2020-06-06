using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SotrAlgorithm
{
    interface ISortter
    {
        UInt64 Sorting(int[] array);
    }

    class Bubble : ISortter
    {
        public UInt64 Sorting(int[] array)
        {
            UInt64 loops = 0;
            bool isReady = true;

            do
            {
                isReady = true;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    loops++;
                    if (array[i] > array[i + 1])
                    {
                        int temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        isReady = false;
                    }
                }
            } while (!isReady);

            return loops;
        }
    }

    class Choice : ISortter
    {
        public UInt64 Sorting(int[] array)
        {
            UInt64 loops = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    loops++;
                    if (array[j] < array[min])
                    {
                        min = j;
                    }
                }
                int temp = array[min];
                array[min] = array[i];
                array[i] = temp;
            }

            return loops;
        }
    }

    interface IArrayBuilder
    {
        int[] GetArray(int size);
    }

    class Forward : IArrayBuilder
    {
        public int[] GetArray(int size)
        {
            int[] array = new int[size];
            int ramdom = new Random().Next(32000);

            for (int i = 0; i < size; i++)
                array[i] = ramdom + i;

            return array;
        }
    }

    class Back : IArrayBuilder
    {
        public int[] GetArray(int size)
        {
            int[] array = new int[size];
            int ramdom = new Random().Next(32000);

            for (int i = 0; i < size; i++)
                array[i] = ramdom - i;

            return array;
        }
    }

    class Degenerate : IArrayBuilder
    {
        public int[] GetArray(int size)
        {
            int[] array = new int[size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
                array[i] = random.Next(0, 12);

            return array;
        }
    }

    class Сasual : IArrayBuilder
    {
        public int[] GetArray(int size)
        {
            int[] array = new int[size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
                array[i] = random.Next(0, 72000);

            return array;
        }
    }

    class Program
    {
        static string Test(ISortter sort, IArrayBuilder array, int size)
        {
            int[] testArray = array.GetArray(size);
            DateTime start = DateTime.Now;

            UInt64 loops = sort.Sorting(testArray);

            TimeSpan duraction = DateTime.Now - start;

            double N = Math.Log(loops, size);

            return $"Size:{size,7:N0}| Loops:{loops,14:N0}| N:{N,6:f3}| Sort:{sort.GetType().Name,7}| RawArray:{array.GetType().Name,11}| Milliseconds:{duraction.TotalMilliseconds,13:f6}| Ticks:{duraction.Ticks,11:N0}";
        }

        static void Main(string[] args)
        {
            int[] sizes = { 1000, 2000, 4000, 8000, 16000, 32000, 64000 };

            ISortter[] sorts = {
                new Bubble(),
                new Choice()
            };

            IArrayBuilder[] builders = {
                new Forward(),
                new Back(),
                new Degenerate(),
                new Сasual()
            };

            foreach (int size in sizes)
                foreach (ISortter sort in sorts)
                    foreach (IArrayBuilder array in builders)
                        Console.WriteLine(Test(sort, array, size));

            Console.ReadLine();
        }
    }
}
