using ServerClientConnection.MessageTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientConnection
{
    public class Finish : Data
    {
        public int strokes { get; set; }
    }
}
