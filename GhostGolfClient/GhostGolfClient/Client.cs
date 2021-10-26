using Newtonsoft.Json;
using ServerClientConnection;
using System;
using System.Net.Sockets;

namespace GhostGolfClient
{
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Im a client!");

            
            ServerConnection connection = new ServerConnection( new TcpClient("localhost", 5005), "bob");
        }
    }
}
