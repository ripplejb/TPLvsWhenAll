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
                var t = new WaitPrint().WaitAndPrint($"WhenAll {i1}");
                list.Add(t);
            }

            await Task.WhenAll(list);
        }


        static void ParallelForEach()
        {
            var list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Parallel.ForEach(list, i =>
            {
                new WaitPrint().WaitAndPrint2($"Parallel ForEach {i}");
            });
        }

        private class WaitPrint
        {
            public async Task<string> WaitAndPrint(string str)
            {
                await Task.Delay(3000); // Wait
                Console.WriteLine(str);
                return str;
            }

            public void WaitAndPrint2 (string str)
            {
                Thread.Sleep(10000); // Wait
                Console.WriteLine(str);
            }
        }

    }
}
