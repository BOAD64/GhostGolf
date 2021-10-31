using System;
using System.Net.Sockets;

namespace GhostGolfClient
{
    public class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Im a client!");
            new Client();
        }

        private ServerConnection connection;

        public Client()
        {
            //connection = new ServerConnection(new TcpClient("localhost", 5005), "bob");
        }

        public string getTest()
        {
            return "hello bro I work";
        }
    }
}