using HamBusLib.Models;
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
        public static void Parse(ConcurrentDictionary<string, JsonBase> busList, UdpCmdPacket udpPacket)
        {
            foreach (KeyValuePair<string, JsonBase> item in busList)
            {
                switch (item.Key.ToLower())
                {
                    case "id":
                        udpPacket.Id = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "command":
                        udpPacket.Command = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "time":
                        udpPacket.Time = ((JsonNode<Int64>)(item.Value)).Value;
                        break;
                    case "name":
                        udpPacket.Name = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "description":
                        udpPacket.Description = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "host":
                        udpPacket.Host = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "ip":
                        udpPacket.Ip = ((JsonNode<string>)(item.Value)).Value;
                        break;
                    case "udpport":
                        udpPacket.UdpPort = ((JsonNode<Int64>)(item.Value)).Value;
                        break;
                    case "tcpport":
                        udpPacket.TcpPort = ((JsonNode<Int64>)(item.Value)).Value;
                        break;
                    case "minversion":
                        udpPacket.MinVersion = ((JsonNode<Int64>)(item.Value)).Value;
                        break;
                    case "maxversion":
                        udpPacket.MaxVersion = ((JsonNode<Int64>)(item.Value)).Value;
                        break;
                }
            }
        }
    }
}
