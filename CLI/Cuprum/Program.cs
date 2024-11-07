using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;
using static Cuprum.Functions;

namespace Cuprum
{
    public static class Program 
    {
        public async static Task<int> Main()
        {
            AnsiConsole.WriteLine("[red]Cuprum[/] is starting...");

            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddBranch<NodeSettings>("node", add =>
                {
                    add.AddCommand<StartNodeCommand>("start");
                    add.AddCommand<StopNodeCommand>("stop");
				});
            });
			return 0;
        }
    }
}