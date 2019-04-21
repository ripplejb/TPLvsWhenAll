# Parallel ForEach vs WhenAll

I was asked to explain the difference between TPL and Async-Await. My understanding is TPL = Task Parallel Library contains API to simplify the process of adding parallelism and concurrency to applications. TPL handles partitioning of the work, the scheduling of threads on the ThreadPool, cancellation support, state management, and other low-level details. Therefore the answer to the question is TPL is a superset and async-await is a subset. However, the intention behind the question was to know what is the difference between parallelism and concurrency. Unfortunately, the difference is subtle and because of that, the misunderstanding is widespread. So here are my two cents on the subject.

In order to understand the TPL, one must understand the difference between parallelism and concurrency. Here is a brief but excellent explanation of the difference between [parallelism and concurrency] 

[parallelism and concurrency]: (http://tutorials.jenkov.com/java-concurrency/concurrency-vs-parallelism.html)

Concurrency = Doing more than one thing at a time.
Parallelism = Doing lots of work by dividing it up among multiple threads that run concurrently.

Code 1.1 below is an example of concurrency. In this case, When all is called with await, the calling thread is not going to be blocked.

### 1.1 Concurrency
```c#
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

    private static async Task WaitAndPrintForWhenAll(string str)
    {
        await Task.Delay(3000); 
        Console.WriteLine(str);
    }
```

Code 1.2 below is an example of parallelism. In this case, parallel foreach will block the main thread until the entire ``` list ``` is processed. However, the entire list will be processed in parallel in separate threads. The same can be accomplished using the ``` Task.WaitAll() ``` as well.

### 1.2 Parallelism
```c#
    static void ParallelForEach()
    {
        var list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Parallel.ForEach(list, i =>
        {
            WaitAndPrintForParallel($"Parallel ForEach {i}");
        });
    }

    private static void WaitAndPrintForParallel(string str)
    {
        Thread.Sleep(3000); // Wait
        Console.WriteLine(str);
    }
```
Code 1.3 calls both the methods ``` WhenAll() ``` and ``` ParallelForEach() ```.  When the ``` ParallelForEach() ``` is called, it blocks the main thread. However, when the ``` WhenAll() ``` is called, it does not block the main thread.

### 1.3 Main.
```c#
  static void Main(string[] args)
  {
      Console.WriteLine("Press Any Key to start...");
      Console.ReadLine();

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
```

## Output
![Alt Text](https://github.com/ripplejb/TPLvsWhenAll/blob/master/TPLVsWhenAll/output.gif)
