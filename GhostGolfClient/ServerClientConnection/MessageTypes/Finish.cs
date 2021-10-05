using ServerClientConnection.MessageTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientConnection
{
    class Finish : Data
    {
        public int strokes { get; set; }
    }
}
