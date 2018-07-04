using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class UdpCmdPacket
    {
        public string DocType { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
        public Int64 Time { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Host { get; set; }
        public string Ip { get; set; }
        public int UdpPort { get; set; } = -1;
        public int TcpPort { get; set; } = -1;
        public int MinVersion { get; set; }
        public int MaxVersion { get; set; }
    }
}
