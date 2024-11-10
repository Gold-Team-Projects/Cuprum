using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using System.Numerics;

namespace Cuprum.Messages;

internal class PingMessage : Message 
{
    public PingMessage(BigInteger sender, BigInteger reciever, bool pong = false)
    {
        Type = pong ? MessageType.Pong : MessageType.Ping;
        Sender = sender;
        Reciever = reciever;
    }
}