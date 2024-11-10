using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Cuprum.Messages
{
	internal class BootstrapMessage : Message
	{
		internal List<string> Nodes { get; set; }

		internal BootstrapMessage(BigInteger sender, BigInteger reciever, List<string> nodes)
		{
			Initialize(sender, reciever, MessageType.Bootstrap);
			Nodes = nodes;
		}
	}
}
