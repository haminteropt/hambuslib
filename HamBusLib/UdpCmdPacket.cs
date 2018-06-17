using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class UdpCmdPacket
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
    }
}
