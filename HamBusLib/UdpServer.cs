﻿using HamBusLib;
using HamBusLib.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HamBusLib.UdpNetwork
{
    public class UdpServer
    {
        public int listenUdpPort = -1;
        public int listenTcpPort = -1;
        private static UdpServer netWorkThread = null;
        public Thread serverThread;
        public RigOperatingState OptState { get; set; } = RigOperatingState.Instance;
        UdpClient udpClient = new UdpClient();


        public static UdpServer GetInstance()
        {
            if (netWorkThread == null)
                netWorkThread = new UdpServer();

            return netWorkThread;
        }
        private UdpServer()
        {
            listenTcpPort = IpPorts.TcpPort;
            listenUdpPort = IpPorts.UdpPort;
            udpClient.ExclusiveAddressUse = false;
            ServerInit();
        }


        private void ServerInit()
        {

            serverThread = new Thread(ServerStart);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        private void ServerStart()
        {
            UdpClient udpServer = new UdpClient(listenUdpPort);

            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                ParseCommand(returnData);
            }
        }

        private UdpCmdPacket ParseCommand(string returnData)
        {
            var obj = DocParser.ParsePacket(returnData);
            Console.WriteLine("****** Parse packet from {0} of type {1}", obj.Host, obj.DocType);
            return obj;
        }

        public void SendBroadcast(UdpCmdPacket payload, int port)
        {
            udpClient.Connect("255.255.255.255", port);
            Byte[] senddata = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload));
            udpClient.Send(senddata, senddata.Length);
        }
    }
}
