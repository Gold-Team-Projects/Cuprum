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

	internal class NodeCommand : Command<NodeSettings>
	{
		public override int Execute(CommandContext context, NodeSettings settings)
		{
			throw new NotImplementedException();
		}
	}
}
