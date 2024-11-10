using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using System.Numerics;
using static Cuprum.Classes;

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
        return Math.Min((int)dist, 256 - 1);
    }

    internal static async Task<string> GetIPAddress()
    {
        using (HttpClient client = new())
        {
            HttpResponseMessage res = await client.GetAsync("https://api.ipify.org");
            return await res.Content.ReadAsStringAsync();
        }
    }

    internal static string Format(string name, LogLevel level)
	{
		return _Format(name, level);
	}
    internal static string Format(LogSource source, LogLevel level)
    {
        Dictionary<LogSource, string> map = new()
		{
			{ LogSource.Client, "CLIENT" },
			{ LogSource.Cuprum, "CPRM" },
			{ LogSource.Node, "NODE" }
		};
		return _Format(map[source], level);
	}
	private static string _Format(string name, LogLevel level)
	{
        Dictionary<LogLevel, string> map = new()
        {
            { LogLevel.Info, "[blue]INFO[/]" },
            { LogLevel.Warning, "[orange]WARNING[/]" },
			{ LogLevel.Alert, "[yellow]ALERT[/]" },
			{ LogLevel.Error, "[red]ERROR[/]" }
		};
		return $"[[{name} {map[level]}]]";
	}

    internal static string HashSHA256(string value)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(value));
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}

