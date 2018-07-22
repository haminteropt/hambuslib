using HamBusLib.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Models.Configuration
{
    public class CommPortConf
    {
        public string DisplayName { get; set; }
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public string Parity { get; set; }
        public int DataBits { get; set; }
        public int StopBits { get; set; }
        public string Handshake { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
    }
    public class VirtualBusConf : ConfRec
    {
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
