using HamBusLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class UdpCmdPacket
    {
        public string DocType { get; set; }
        public string Id { get; set; }
        public string Command { get; set; }
        public Int64 Time { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Host { get; set; }
        public string Ip { get; set; }
        public Int64 UdpPort { get; set; } = -1;
        public Int64 TcpPort { get; set; } = -1;
        public Int64 MinVersion { get; set; }
        public Int64 MaxVersion { get; set; }

        public void Copy(UdpCmdPacket source)
        {
            Id = source.Id;
            Command = source.Command;
            Time = source.Time;
            Name = source.Name;
            Description = source.Description;
            Host = source.Host;
            Ip = source.Ip;
            UdpPort = source.UdpPort;
            TcpPort = source.TcpPort;
            MinVersion = source.MinVersion;
            MaxVersion = source.MaxVersion;
        }
        static public UdpCmdPacket Parse(string jsonStr)
        {
            var data = JsonConvert.DeserializeObject<UdpCmdPacket>(jsonStr);
            return data;
        }
    }
}
