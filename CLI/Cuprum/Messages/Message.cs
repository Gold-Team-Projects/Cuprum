using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using System.Numerics;

namespace Cuprum;

internal enum MessageType
{
    Ping, Pong,
	Acknowledge, Request,
	Error,
	Bootstrap
}

internal class Message 
{
    internal MessageType Type { get; set;}
    internal BigInteger Sender { get; set; }
    internal BigInteger Reciever { get; set; }
    internal DateTime Time { get; set; }

    internal void Initialize(BigInteger sender, BigInteger reciever, MessageType type)
	{
		Sender = sender;
		Reciever = reciever;
		Type = type;
		Time = DateTime.Now;
	}
}