using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuprum;

internal class KBucket
{
    internal List<string> Nodes { get; set; } = new();
    internal int K { get; set;} = 20;

    internal void AddNode(string node)
    {
        if (Nodes.Count < K) 
            Nodes.Add(node);
        else 
        {
            Nodes.RemoveAt(0);
            Nodes.Add(node);
        }
    }
}