using System;
using System.Collections.Generic;
using System.Net;

namespace HamBusLib
{
    public class DirectoryBusGreeting : UdpCmdPacket
    {

        public string HostName;
        public string callSign;
    }
}
