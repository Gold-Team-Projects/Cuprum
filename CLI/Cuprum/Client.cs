using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console.Cli;
using Spectre.Console;
using System.Text.Json;
using System.Net.Security;
using static Cuprum.Functions;
using static Cuprum.Classes;
using System.Numerics;

namespace Cuprum
{
	internal static class Client
	{
		public static List<ClientData>? GetClients()
		{
			if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, "clients")))
			{
				return null;
			}

			List<ClientData> clients = new();

			foreach (string file in Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, "clients")))
			{
				string text = File.ReadAllText(file);
				ClientData? data = JsonSerializer.Deserialize<ClientData>(text);

				if (data.HasValue)
				{
					clients.Add(data.Value);
				}
			}

			return clients;
		}
		public static ClientData CreateClient(string name, string username)
		{
			ClientData data = new()
			{
				Name = name,
				Username = username,
				Public = ComputeKey(username),
			};

			string json = JsonSerializer.Serialize(data);
			
			if (!Directory.Exists(Path.Combine(AppContext.BaseDirectory, "clients"))) Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "clients"));

			File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "clients", $"{data.Public}.json"), json);
			return data;
		}
	}

	internal struct ClientData
	{
		public string Name { get; set; }
		public string Username { get; set; }
		public BigInteger Public { get; set; }
		public string Private { get; set; }
	}
}
