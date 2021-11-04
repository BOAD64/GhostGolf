
using Newtonsoft.Json;
using ServerClientConnection;
using ServerClientConnection.MessageTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GhostGolfClient
{
    public class ServerConnection
    {
        public string name;
        private TcpClient client;
        private ServerClientConnection.Client rw;
        private bool active;
        public Level level { get; }
        public int par { get; private set; } = 0;
        public int placement { get; private set; } = 0;
        public int highscore { get; private set; } = 0;

        public ServerConnection(TcpClient client, string name)
        {
            this.client = client;
            this.name = name;
            this.rw = new ServerClientConnection.Client(this.client);
            sentInit();
            new Thread(async e => await Run()).Start();

            this.level = new Level(name, this);
        }

        public void sentInit()
        {
            Init initMessage = new Init();
            Connection con = new Connection() { name = this.name, data = initMessage };
            Debug.WriteLine(JsonConvert.SerializeObject(con));
            writeMessage(con);
        }
        
        public async Task Run()
        {
            this.active = true;

            while (active)
            {
                try
                {
                    string result = await this.rw.Read();
                    Parse(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void Parse(string result)
        {
            Connection message = JsonConvert.DeserializeObject<Connection>(result);
            Data data = message.data;

            if (data is BallPos)
            {
                bool found = false;
                foreach (Ball ball in this.level.opponents)
                {
                    if (ball.owner == ((BallPos)data).sender)
                    {
                        ball.setPos(((BallPos)data).position);
                        found = true;
                    }
                }
                if (!found) this.level.opponents.Add(new Ball(message.name, ((BallPos)data).position[0], ((BallPos)data).position[1]));
            }
            else if (data is Info)
            {
                Info info = (Info)data;
                par = info.par;
                placement = info.placement;
                highscore = info.highscore;
            }
        }

        public void writeMessage(Connection message)
        {
            var stream = new StreamWriter(client.GetStream());
            try
            {
                stream.Write(JsonConvert.SerializeObject(message));
                stream.Flush();
            } catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void readMessage()
        {
            var stream = new StreamReader(client.GetStream());
            try
            {
                Object obj = stream.Read();
                Debug.WriteLine(obj);      
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public void sendInfo ()
        {
            Connection message = new Connection()
            {
                name = System.Environment.MachineName,
                data = new Info { }
            };
            writeMessage(message);
        }
    }
}
