using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Net;
using static Cuprum.Functions;

namespace Cuprum.Utilities;

internal class TCPPort 
{
    public int Port { get; set; }
    public List<NetworkStream> Streams { get; set; } = new();

	private TcpListener _listener;

    internal TCPPort(int port)
	{
		Port = port;
		
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress ipAddr = ipHost.AddressList[0];
		_listener = new TcpListener(ipAddr, port);


	}
}

internal class TCPConnection
{
    public string IP { get; set;}
    public int Port { get; set; }

    private TcpClient _client;

    public TCPConnection(string ip, int port)
    {
        IP = ip;
        Port = port;

        _client = new(ip, port);
    }

    public void Write(object data) 
    {
        byte[] buffer;
        if (data is string) buffer = Encoding.UTF8.GetBytes((string)data);
        if (data is byte[]) buffer = (byte[])data;
        if (data is Message)
        {
            ((Message)data).Time = DateTime.UtcNow;
            string json = JsonSerializer.Serialize(data);
            buffer = Encoding.UTF8.GetBytes(json);
        }
        else 
        {
            string json = JsonSerializer.Serialize(data);
            buffer = Encoding.UTF8.GetBytes(json);
        }

        _client.GetStream().Write(buffer);
    }

    public T ReadMessage<T>()
    {
        var stream = _client.GetStream();
        int unresolved = 0;
        List<byte> output = new();
        bool first = true;

        while (unresolved != 0 && !first)
        {
            byte[] buffer = new byte[1024];
            stream.Read(buffer);
            string str = Encoding.UTF8.GetString(buffer);
            int firsts = str.Count(c => c == '{');
            int lasts = str.Count(c => c == '}');
            unresolved += firsts - lasts;
            output.AddRange(buffer);
            first = false;
        }

        string json = Encoding.UTF8.GetString(output.ToArray());
        return JsonSerializer.Deserialize<T>(json)!;
    }

	public async Task<T> WaitForMessageAsync<T>(int timeoutMilliseconds = 5000)
	{
		var stream = _client.GetStream();
		var startTime = DateTime.UtcNow;

		while (!stream.DataAvailable)
		{
			if ((DateTime.UtcNow - startTime).TotalMilliseconds > timeoutMilliseconds)
			{
				throw new TimeoutException("Timed out waiting for message.");
			}
			await Task.Delay(50);
		}
		return ReadMessage<T>();
	}

	public void Close()
    {
        _client.Close();
	}
}