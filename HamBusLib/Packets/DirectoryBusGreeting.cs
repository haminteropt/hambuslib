using System;
using System.Collections.Generic;
using System.Net;

namespace HamBusLib
{
    public class DirectoryBusGreeting : UdpCmdPacket
    {

        public string CallSign;

        public DirectoryBusGreeting()
        {
            DocType = "DirectoryBusGreeting";
            Host = NetworkUtils.getHostName();
            CallSign = "None";
           
        }
    }
}
