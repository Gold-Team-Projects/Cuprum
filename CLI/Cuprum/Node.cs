using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;
using Spectre.Console.Cli;
using static Cuprum.Functions;
using System.Linq.Expressions;
using Cuprum.Utilities;
using Cuprum.Messages;

namespace Cuprum
{
    internal static class Node 
    {
        internal static BigInteger ID { get; set; } = new(-1);
        internal static List<KBucket> Buckets { get; set; } = new();

        internal static float DiscoveryInterval { get; set;} = 0.001f;
        
        internal async static Task Initialize(string? bootstrap = null) 
        {
            if (bootstrap == null) 
            {
                using (HttpClient client = new())
                {
                    var res = await client.GetAsync("http://gold-team.tech/api/static/cuprum-bootstrap.txt");
                    var content = (await res.Content.ReadAsStringAsync()).Split('\n');
                    bootstrap = content[ChooseRandom(0, content.Length)];
                }
            }

            TCPConnection conn = new(bootstrap, 2031);
            conn.Write(new PingMessage(ID, new(-1)));
            PingMessage pong = conn.ReadMessage<PingMessage>();

            BigInteger id = pong.Sender;
            ID = pong.Reciever;

            conn.Write(new RequestMessage(ID, id, new()));
            Message msg = conn.ReadMessage<Message>();

            if (msg.Type == MessageType.Error) throw new Exception("Network denied access: " + ((ErrorMessage)msg).Error))
		}

        internal static void AddNode(string ip)
        {
            Buckets[GetNodeBucketIndex(ID, ComputeKey(ip))].AddNode(ip);
        }
    }
    
    internal struct NodeData 
    {
        
    }
}