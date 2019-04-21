using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TPLVsWhenAll
{
    static class Program
    {

        /**
         * When we call ParallelForEach(), it waits till all the call inside the loop are completed.
         * When we call WhenAll(), it move on to next line.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("=========================");
            Console.WriteLine("Parallel 1 ForEach start");
            Console.WriteLine("=========================");
            ParallelForEach();

            Console.WriteLine("=========================");
            Console.WriteLine("WhenAll 1 start");
            Console.WriteLine("=========================");
            WhenAll();

            Console.WriteLine("=========================");
            Console.WriteLine("WhenAll 2 start");
            Console.WriteLine("=========================");
            WhenAll();

            Console.WriteLine("=========================");
            Console.WriteLine("Parallel 2 ForEach start");
            Console.WriteLine("=========================");
            ParallelForEach();


            Console.ReadLine();
        }

        static async void WhenAll()
        {
            List<Task> list = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                var i1 = i;
                var t = WaitAndPrintForWhenAll($"WhenAll {i1}");
                list.Add(t);
            }

            await Task.WhenAll(list);
        }


        static void ParallelForEach()
        {
            var list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Parallel.ForEach(list, i =>
            {
                WaitAndPrintForParallel($"Parallel ForEach {i}");
            });
        }

        private static async Task<string> WaitAndPrintForWhenAll(string str)
        {
            await Task.Delay(3000); // Wait
            Console.WriteLine(str);
            return str;
        }

        private static void WaitAndPrintForParallel(string str)
        {
            Thread.Sleep(3000); // Wait
            Console.WriteLine(str);
        }

    }
}
