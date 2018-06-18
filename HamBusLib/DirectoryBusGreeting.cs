using System;
using System.Collections.Generic;
using System.Net;

namespace HamBusLib
{
    public class DirectoryBusGreeting : UdpCmdPacket
    {
        public int TcpPort = -1;
        public int UdpPort = -1;
        public string HostName;
        public string callSign;
    }
}
