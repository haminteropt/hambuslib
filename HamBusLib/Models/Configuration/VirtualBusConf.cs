using HamBusLib.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Models.Configuration
{

    public class VirtualBusConf : ConfRec
    {
        public string Host;
        public List<CommPortConf> CommPorts = new List<CommPortConf>();
        public VirtualBusConf()
        {
            DocType = DocTypes.VirtualConfig;
        }
        public void AddCommPort(CommPortConf port)
        {
            CommPorts.Add(port);
        }
        public void RemoveCommPort(CommPortConf port)
        {
            List<CommPortConf> newPorts = new List<CommPortConf>();
            foreach(var pItem in CommPorts)
            {
                if (pItem.PortName != port.PortName)
                    newPorts.Add(pItem);
            }
            CommPorts = newPorts;
        }
    }
}
