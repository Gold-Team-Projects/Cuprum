using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cuprum.Messages
{
	internal class ErrorMessage : Message
	{
		internal string Error { get; set; }

		internal ErrorMessage(BigInteger sender, BigInteger reciever, string err) 
		{
			Initialize(sender, reciever, MessageType.Error);
			Error = err;
		}
	}
}
