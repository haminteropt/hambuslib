using HamBusLib.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace HamBusLib
{
    public class DirectoryBusGreeting : UdpCmdPacket
    {

        public string CallSign;
        public DirectoryBusGreeting()
        {
            DocType = DocTypes.DirectoryBusGreeting;
            Host = NetworkUtils.getHostName();
            CallSign = "None";

        }
        public void JsonParse(string jsonData)
        {
        }
        public static DirectoryBusGreeting ParseCommand(string returnData)
        {
            var obj = JsonConvert.DeserializeObject<DirectoryBusGreeting>(returnData);
            return obj;
        }
        public void Copy(DirectoryBusGreeting source)
        {
            base.Copy(source);
            CallSign = source.CallSign;
            Host = source.Host;

        }
        static public DirectoryBusGreeting Parse(string jsonStr)
        {
            var data = JsonConvert.DeserializeObject<DirectoryBusGreeting>(jsonStr);
            return data;
        }
    }
}
