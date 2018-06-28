using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigOperatingState : UdpCmdPacket
    {
        public Int64 Freq { get; set; }
        public Int64 FreqA { get; set; }
        public Int64 FreqB { get; set; }
        public string Mode { get; set; }
        public string ModeStd { get; set; }
        public bool Tx { get; set; }
        public string Vfo { get; set; }
        public string Xit { get; set; }
        public string Split { get; set; }
        public string Rit { get; set; }

        public RigOperatingState()
        {
            Freq = 14250000;
            Tx = false;
            Mode = ModeConst.USB;
        }
    }
}
