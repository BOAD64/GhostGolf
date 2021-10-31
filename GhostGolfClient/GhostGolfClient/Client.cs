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
        private Level level;

        public Client()
        {
            //this.connection = new ServerConnection(new TcpClient("localhost", 5005), "bob");
            this.level = new Level("level 1", null);
        }

        public Level getLevel ()
        {
            return this.level;
        }
    }
}