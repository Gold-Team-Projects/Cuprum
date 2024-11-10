using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cuprum.Messages
{
	internal class RequestMessage : Message
	{
		internal Dictionary<string, object> Parameters { get; set; }
		internal RequestType ReqType { get; set; }

		internal RequestMessage(BigInteger sender, BigInteger reciever, Dictionary<string, object> parameters, RequestType type)
		{
			Initialize(sender, reciever, MessageType.Request);
			Parameters = parameters;
			ReqType = type;
		}
	}

	internal enum RequestType
	{
		Join, Leave,
		Discovery
	}
}
