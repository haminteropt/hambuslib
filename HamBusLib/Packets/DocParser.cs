using HamBusLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Packets
{
    public class DocParser
    {
        static public UdpCmdPacket ParsePacket(string jsonStr)
        {
            var pkt = JsonConvert.DeserializeObject<UdpCmdPacket>(jsonStr);
            UdpCmdPacket ret;
            switch (pkt.DocType)
            {
                case DocTypes.OperatingState:
                    ret = OperatingState.Parse(jsonStr);
                    return ret;
                case DocTypes.DirectoryBusGreeting:
                    ret = DirectoryBusGreeting.Parse(jsonStr);
                    return ret;
                case DocTypes.RigBusInfo:
                    ret = RigBusInfo.Parse(jsonStr);
                    return ret;
                case DocTypes.DataBusInfo:
                    ret = DataBusInfo.Parse(jsonStr);
                    return ret;
                case DocTypes.ActiveBuses:
                    ret = ActiveBuses.Parse(jsonStr);
                    return ret;
            }
            return null;
        }
    }
}
