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
        public static int Main(string[] args)
        {
            AnsiConsole.MarkupLine("[red]Cuprum[/] is starting...");

            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddBranch<NodeSettings>("node", add =>
                {
                    add.AddCommand<StartNodeCommand>("start");
                    add.AddCommand<StopNodeCommand>("stop");
                });
                config.AddBranch<ClientSettings>("client", add =>
                {
                    add.AddCommand<ClientUICommand>("ui");
					add.AddCommand<ClientCreateCommand>("create");
                });
			});
            app.Run(args);
			return 0;
        }
    }
}