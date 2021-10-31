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
            this.connection = new ServerConnection(new TcpClient("localhost", 5005), "bob");
        }

        public ServerConnection getConnection ()
        {
            return this.connection;
        }
    }
}