using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigBusInfo : UdpCmdPacket
    {

        public string RigType { get; set; }
        public bool SendSyncInfo { get; set; } = false;
        public bool HonorTx { get; set; } = false;

        public RigBusInfo()
        {
            Id = Guid.NewGuid().ToString();
            DocType = "RigBus";
        }
    }
}
