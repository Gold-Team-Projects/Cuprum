using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;
using static Cuprum.Functions;

namespace Cuprum
{
    public static class Program 
    {
        public async static Task<int> Main()
        {
            AnsiConsole.WriteLine("[red]Cuprum[/] is starting...")
            List<Task> startupTasks = new()
            {
                async () => Node.Initialize(),
                async () => Nodr.ReadData()
            }

            using (SemaphoreSlim semaphore = new(4))
            {
                List<Task> realTasks = new();
                foreach (Task t in startupTasks) 
                    realTasks.Add(Task.Run(async () => { await semaphore.WaitAsync(); await t(); semaphore.Release();}));
                realTasks.WhenAll(realTasks);
            }

        }
    }
}