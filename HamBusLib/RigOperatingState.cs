using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigOperatingState : UdpCmdPacket
    {
        public Int64 Freq { get; set; }
        public string Mode { get; set; }
        public string ModeStd { get; set; }
        public bool Tx { get; set; }

        public RigOperatingState()
        {
            Freq = 14250000;
            Tx = false;
            Mode = ModeConst.USB;
        }
    }
}
