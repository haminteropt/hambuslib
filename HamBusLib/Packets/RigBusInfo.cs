using HamBusLib.Models;
using HamBusLib.Packets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigBusInfo : UdpCmdPacket
    {

        public string RigType { get; set; }
        public bool SendSyncInfo { get; set; } = false;
        public bool HonorTx { get; set; } = false;
        public string ComPort { get; set; }

        public RigBusInfo()
        {
            Id = Guid.NewGuid().ToString();
            DocType = DocTypes.RigBusInfo;
        }
        public RigBusInfo Parse(ConcurrentDictionary<string, JsonBase> busList)
        {
            var rc = new RigBusInfo();
            Parse(busList, rc);
            foreach (KeyValuePair<string, JsonBase> item in busList)
            {
                switch (item.Key.ToLower())
                {
                    case "rigtype":
                        RigType = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "sendsyncinfo":
                        SendSyncInfo = ((JsonNode<Boolean>)(item.Value)).Value;
                        break;
                    case "honortx":
                        HonorTx = ((JsonNode<Boolean>)(item.Value)).Value;
                        break;
                    case "comport":
                        ComPort = ((JsonNode<string>)(item.Value)).Value;
                        break;
                }
            }
            return rc;
        }

    }
}
