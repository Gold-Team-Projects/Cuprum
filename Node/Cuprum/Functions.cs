using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;

namespace Cuprum;

internal static class Functions 
{
    internal static BigInteger ComputeKey(string value)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            return new BigInteger(hash, isBigEndian: true);
        }
    }
    internal static BigInteger XOR(BigInteger key1, BigInteger key2)
    {
        return key1 ^ key2;
    }

    internal static int GetNodeBucketIndex(BigInteger id1, BigInteger id2)
    {
        BigInteger dist = XOR(id1, id2);
        return Math.Min(dist, 256 - 1)
    }

    internal static async Task<string> GetIPAddress()
    {
        using (HttpClient client = new())
        {
            HttpResponseMessage res = await client.GetAsync("https://api.ipify.org");
            return await res.Content.ReadAsStringAsync();
        }
    }
}