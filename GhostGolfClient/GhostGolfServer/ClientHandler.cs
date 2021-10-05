using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerClientConnection;
using ServerClientConnection.MessageTypes;

namespace GhostGolfServer
{
    public class ClientHandler
    {
        public string Name { get; set; }
        private Client Client { get; }

        private Server server;
        private bool active;

        public ClientHandler(TcpClient tcpClient, Server server)
        {
            this.Client = new Client(tcpClient);
            this.server = server;


            new Thread(Run).Start();
        }

        private async Task<string> getName()
        {
            string message = await Client.Read();
            Connection jsonObject = JsonConvert.DeserializeObject<Connection>(message);
            string name = jsonObject.name;

            if (!(jsonObject.data is Init))
            {
                disconnect();
            }
            return name;
        }

        public void disconnect()
        {
            this.server.OnDisconnect(this);
            this.active = false;
            this.Client.Dispose();
        }

        internal void send(Connection message)
        {
            byte[] toSend = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message));
            Client.Write(toSend);
        }

        private async void Run()
        {
            this.Name = await getName();

            this.active = true;
            while (active)
            {
                try
                {
                    string result = await Client.Read();
                    Parse(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    this.disconnect();
                }
            }
        }

        private void Parse(string toParse)
        {
            Connection message = JsonConvert.DeserializeObject<Connection>(toParse);
            Data data = message.data;

            if (data is BallPos)
            {
                //ToDo send appropriate message
            }
            else if (data is Finish)
            {
                //ToDo send appropriate message
            }
        }
    }
}
