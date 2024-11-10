using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Cuprum.Messages
{
	internal class AcknowledgementMessage : Message
	{
		Dictionary<string, object> Parameters { get; set; }
		internal AcknowledgementMessage(BigInteger sender, BigInteger reciever, Dictionary<string, object> parameters)
		{
			Initialize(sender, reciever, MessageType.Acknowledge);
			Parameters = parameters;
		}
	}
}
