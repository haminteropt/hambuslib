using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigOperatingState : UdpCmdPacket
    {
        public int Freq { get; set; }
        public string Mode { get; set; }
    }
}
