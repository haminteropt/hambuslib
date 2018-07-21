using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Packets
{
    public class DocTypes
    {
        public const string DataBusInfo = "DataBusInfo";
        public const string RigBusInfo = "RigBusInfo";
        public const string OperatingState = "OperatingState";
        public const string DirectoryBusGreeting = "DirectoryBusGreeting";
        public const string ActiveBuses = "ActiveBuses";

        public const string VirtualConfig = "VirtualConfig";
        public const string RigBusConfig = "RigBusConfig";
        public const string LogBusConfig = "LogBusConfig";
        public const string DataBusConfig = "DataBusConfig";

    }
}
