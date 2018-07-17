using HamBusLib.Models;
using HamBusLib.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class DataBusInfo : UdpCmdPacket
    {

        public DataBusInfo()
        {
            DocType = DocTypes.DataBusInfo;
        }

        static public DataBusInfo Parse(string jsonStr)
        {
            var data = JsonConvert.DeserializeObject<DataBusInfo>(jsonStr);
            return data;
        }
    }
}

