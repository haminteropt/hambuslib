using HamBusLib.Models;
using HamBusLib.Packets;
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


        static public DataBusInfo Parse(ConcurrentDictionary<string, JsonBase> busList)
        {
            var rc = new DataBusInfo();
            Parse(busList, rc);
            //foreach(KeyValuePair<string, JsonBase> item in busList)
            //{
            //    switch (item.Key.ToLower())
            //    {
            //        case "rigtype":
            //            RigType = ((JsonNode<string>)(item.Value)).Value;
            //            break;
            //        case "sendsyncinfo":
            //            SendSyncInfo = ((JsonNode<Boolean>)(item.Value)).Value;
            //            break;
            //        case "honortx":
            //            HonorTx = ((JsonNode<Boolean>)(item.Value)).Value;
            //            break;
            //        case "comport":
            //            ComPort = ((JsonNode<string>)(item.Value)).Value;
            //            break;
            //    }

            //}
            return rc;
        }
    }
}

