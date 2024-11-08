using Spectre.Console.Cli;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cuprum.Functions;
using static Cuprum.Classes;

namespace Cuprum
{
	internal class ClientSettings : CommandSettings
	{

	}
	internal class ClientCreateCommand : Command<ClientSettings>
	{
		public override int Execute(CommandContext context, ClientSettings settings)
		{
			AnsiConsole.MarkupLine($"{Format(LogSource.Client, LogLevel.Info)} Starting the [darkorange]Cuprum[/] client creator...");
			string name = AnsiConsole.Prompt(new TextPrompt<string>("Enter the client name:"));
			string username = AnsiConsole.Prompt(new TextPrompt<string>("Enter the client username:"));

			AnsiConsole.MarkupLine($"{Format(LogSource.Client, LogLevel.Info)} Creating client [blue]{name}[/] in directory {Path.Combine(AppContext.BaseDirectory, "clients", HashSHA256(username))}...");

			Client.CreateClient(name, username);

			return 0;
		}
	}
	internal class ClientUICommand : Command<ClientSettings>
	{
		public override int Execute(CommandContext context, ClientSettings settings)
		{
			if (Client.GetClients() == null)
			{
				AnsiConsole.MarkupLine($"{Format(LogSource.Client, LogLevel.Info)} [red] No clients found.[/] Please run [blue]'cuprum client create'[/].");
			}

			return 0;
		}
	}
}
