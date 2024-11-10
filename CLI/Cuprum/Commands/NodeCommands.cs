using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuprum
{
	internal class NodeSettings : CommandSettings
	{

	}

	internal class StartNodeCommand : Command<NodeSettings>
	{
		public override int Execute(CommandContext context, NodeSettings settings)
		{
			throw new NotImplementedException();
		}
	}

	internal class StopNodeCommand : Command<NodeSettings>
	{
		public override int Execute(CommandContext context, NodeSettings settings)
		{
			throw new NotImplementedException();
		}
	}
}
