using System;
using System.Collections.Generic;
using System.Security.Crytography;
using System.Text;
using static Cuprum.Functions;

namespace Cuprum
{
    internal static class Node 
    {
        internal static BigInteger ID { get; set; } = new(-1);
        internal static List<KBucket> Buckets { get; set; } = new();

        internal static float DiscoveryInterval { get; set;} = 0.001f;

        internal static string RootPath { get; set;} = "null";
        internal static string DataPath { get; set;} = "null";

        internal static bool Initialized { get; set;} = false;

        internal static async Task Initialize()
        {
            if (Environment.GetEnvironmentVariable("CPRM") == null)
            {

            }
            else Initialized = true;
        }
        internal static async Task ReadData()
        {
            while (!Initialized) await Task.Delay(50);

            RootPath = Environment.GetEnvironmentVariable("CPRM_PATH");
            DataPath = $"{RootPath}/data.json";

            
        }

        internal static void AddNode(string ip)
        {
            Buckets[GetNoddBucketIndex(ID, ComputeKey(ip))].AddNode(ip);
        }

        internal struct Data 
        {}
    }
}