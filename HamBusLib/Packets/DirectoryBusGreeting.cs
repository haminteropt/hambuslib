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
            DocType = "DirectoryBusGreeting";
            Host = NetworkUtils.getHostName();
            CallSign = "None";

        }
        public void JsonParse(string jsonData)
        {
        }
        public void ParseCommand(string returnData)
        {
            var obj = JsonConvert.DeserializeObject<DirectoryBusGreeting>(returnData);
            if (obj.DocType == DocType)
            {
                obj.copy(this);
            }
        }
        public void copy(DirectoryBusGreeting source)
        {
            base.Copy(source);
            CallSign = source.CallSign;
            Host = source.Host;

        }
    }
}
