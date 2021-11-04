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
        private FileIO fileIO;

        public ClientHandler(TcpClient tcpClient, Server server)
        {
            this.Client = new Client(tcpClient);
            this.server = server;
            this.fileIO = new FileIO();

            new Thread(Run).Start();
        }

        private async Task<string> getName()
        {
            string message = await Client.Read();
            Connection jsonObject = JsonConvert.DeserializeObject<Connection>(message);
            string name = jsonObject.name;

            Console.WriteLine(name);

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

        public void send(Connection message)
        {
            byte[] toSend = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message));
            Client.Write(toSend);
        }

        private async void Run()
        {
            this.active = true;
            Console.WriteLine("is running 1");
            this.Name = await getName();
            Console.WriteLine("is running 2");

            while (active)
            {
                try
                {
                    string result = await Client.Read();
                    Console.WriteLine("message recieved");
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
                Console.WriteLine($"DEBUG = player: {message.name} has shot ball to: {((BallPos)data).position}");
                message.name = "all";
                this.server.send(message, this);
            }
            else if (data is Finish)
            {
                Console.WriteLine($"DEBUG = player: {message.name} has finnished, strokes: {((Finish)data).strokes}");
                this.fileIO.UpdatePar(((Finish)data).strokes);
                if (!this.fileIO.TryUpdateHighscore(this.Name, ((Finish)data).strokes))
                {
                    this.fileIO.AddHighscore(this.Name, ((Finish)data).strokes);
                }
            }
            else if (data is Info)
            {
                Info info = new Info{par = this.fileIO.GetPar(), placement = this.fileIO.GetPlacement(this.Name),
                    highscore = this.fileIO.GetHighScore(this.Name)};
                message.data = info;

                this.server.send(message, this);
            }
        }
    }
}
