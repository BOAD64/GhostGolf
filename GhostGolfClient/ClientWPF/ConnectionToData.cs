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
        
        public ConnectionToData()
        {
            client = new Client();
        }

        public string getTest()
        {
            return client.getTest();
        }
    }
}
