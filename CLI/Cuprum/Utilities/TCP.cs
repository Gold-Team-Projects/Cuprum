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

namespace Cuprum.Utilities;

internal class TCPPort 
{

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

	public async Task<T> WaitForMessageAsync<T>()
	{
		var stream = _client.GetStream();
		while (!stream.DataAvailable)
		{
			await Task.Delay(50);
		}
		return ReadMessage<T>();
	}

	public void Close()
    {
        _client.Close();
	}
}