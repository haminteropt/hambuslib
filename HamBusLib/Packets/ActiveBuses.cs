using HamBusLib.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HamBusLib.Models
{
    public class ActiveBuses : UdpCmdPacket
    {
        public List<RigBusInfo> RigBuses = new List<RigBusInfo>();
        public List<DataBusInfo> DataBuses = new List<DataBusInfo>();
        public ActiveBuses()
        {
            Id = Guid.NewGuid().ToString();
            DocType = DocTypes.ActiveBuses;
        }
        static new public ActiveBuses Parse(string jsonStr)
        {
            var activeDir = JsonConvert.DeserializeObject<ActiveBuses>(jsonStr);
            return activeDir;
        }
    }
}
