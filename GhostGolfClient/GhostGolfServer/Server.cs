using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerClientConnection;

namespace GhostGolfServer
{
    public class Server
    {
        private TcpListener listener;
        private List<ClientHandler> clients;

        static void Main(string[] args)
        {
            new Server();
        }

        public Server()
        {
            this.clients = new List<ClientHandler>();
            this.listener = new TcpListener(System.Net.IPAddress.Any, 5005);

            listener.Start();
            while (true)
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private void OnConnect(IAsyncResult ar)
        {
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            var tcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            clients.Add(new ClientHandler(tcpClient, this));
        }

        public void OnDisconnect(ClientHandler client)
        {
            if (this.clients.Contains(client))
            {
                this.clients.Remove(client);
            }

        }

        public void send(Connection message, ClientHandler sender)
        {
            string target = message.name;

            if (target == "all")
            {
                foreach (ClientHandler client in clients)
                {
                    if (sender.Name != client.Name)
                        client.send(message);
                }
                return;
            }
            foreach (ClientHandler client in clients)
            {
                if (target == client.Name)
                    client.send(message);
            }

        }
    }
}
