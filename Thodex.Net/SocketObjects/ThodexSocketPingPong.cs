using System;
using System.Collections.Generic;
using System.Text;

namespace Thodex.Net.SocketObjects
{
    public class ThodexSocketPingPong
    {
        public DateTime PingTime { get; set; }

        public DateTime PongTime { get; set; }

        public string PongMessage { get; set; }

        public TimeSpan Latency { get; set; }
    }
}
