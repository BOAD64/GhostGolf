using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostGolfClient;

namespace ClientWPF
{
    class ConnectionToData
    {
        private Client client;
        private ServerConnection serverConnection;
        
        public ConnectionToData()
        {
            client = new Client();
            serverConnection = client.getConnection();
        }


        public Level GetLevel()
        {
            return serverConnection.level;
        }
    }
}
