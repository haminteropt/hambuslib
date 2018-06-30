using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigBusInfo : UdpCmdPacket
    {

        public string RigType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Host { get; set; }
        public string Ip { get; set; }
        public int UdpPort { get; set; }
        public int TcpPort { get; set; }
        public bool SendSyncInfo { get; set; } = false;
        public bool HonorTx { get; set; } = false;
        public int MinVersion { get; set; }
        public int MaxVersion { get; set; }
    }
}
