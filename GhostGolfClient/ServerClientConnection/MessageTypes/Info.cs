using ServerClientConnection.MessageTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientConnection
{
    class Info : Data
    {
        public int par { get; set; }
        public int highscore { get; set; }
        public int placement { get; set; }
    }
}
