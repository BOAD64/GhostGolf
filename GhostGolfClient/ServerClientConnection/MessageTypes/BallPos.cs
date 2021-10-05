using ServerClientConnection.MessageTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerClientConnection
{
    class BallPos : Data
    {
        public float[] position { get; set; }
    }
}
