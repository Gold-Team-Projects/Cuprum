using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;
using Spectre.Console.Cli;
using static Cuprum.Utilities.Functions;

namespace Cuprum
{
    internal static class Node 
    {
        internal static string ID { get; set; } = "";
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