
using Newtonsoft.Json;
using ServerClientConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

using System.Threading.Tasks;

namespace GhostGolfClient
{
    class ServerConnection
    {
        private TcpClient client;

        public ServerConnection(TcpClient client)
        {
            this.client = client;
        }

        internal void sentInit()
        {
            Init I = new Init();
            Connection con = new Connection() { name = "bob", data = I };
            Debug.WriteLine(JsonConvert.SerializeObject(con));
            writeMessage(con);
            readMessage();
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
    }
}
