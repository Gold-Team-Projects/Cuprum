using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Numerics;
using static Cuprum.Functions;

namespace Cuprum
{
    internal static class Node 
    {
        internal static BigInteger ID { get; set; } = new(-1);
        internal static List<KBucket> Buckets { get; set; } = new();

        internal static float DiscoveryInterval { get; set;} = 0.001f;

        internal static void AddNode(string ip)
        {
            Buckets[GetNodeBucketIndex(ID, ComputeKey(ip))].AddNode(ip);
        }

        internal struct Data 
        {}
    }
}