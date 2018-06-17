using System;
using System.Collections.Generic;
using System.Net;

namespace HamBusLib
{
    public class DirectoryBusGreeting : UdpCmdPacket
    {
        public string Somedata { get; set; }
        public string Somedata1 { get; set; }
        public string Somedata2 { get; set; }
        public const int DirPortUdp = 7300;
    }
}
