using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll(
                registerProcess((name) =>
                {
                    Console.WriteLine($"Start the process {name}");
                    return Task.Run(() =>
                    {
                        Console.WriteLine($"Execution the second process {name}");
                        for (int i = 0; i < 30; i++)
                        {
                            Console.WriteLine($"{name} - {i}");
                            Thread.Sleep(1000);
                        };
                        Console.WriteLine($"Finish the second process {name}");
                    });

                }, "Carlos"),
                registerProcess((name) =>
                {
                    return Task.Run(() =>
                    {
                        Console.WriteLine($"Execution the second process {name}");
                        for (int i = 30; i > 0; i--)
                        {
                            Console.WriteLine($"{name} - {i}");
                            Thread.Sleep(1000);
                        };
                        Console.WriteLine($"Finish the second process {name}");
                    });
                }, "Mario"));
        }

        private static Task registerProcess(Func<string, Task> action, string name)
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"Start context {name}");

                        await action(name);

                        Console.WriteLine($"Wait the context {name}");

                        Thread.Sleep(2000);

                        Console.WriteLine($"Finish the context {name}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
        }
    }
}
