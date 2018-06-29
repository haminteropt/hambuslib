using HamBusLib;
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
    public class NetworkThreadRunner
    {
        public int listenUdpPort = -1;
        public int listenTcpPort = -1;
        private const int basePort = 7301;
        private const int topPort = 7600;

        private static NetworkThreadRunner netWorkThread = null;
        public Thread serverThread;
        public Thread clientThread = null;
        public string guid = Guid.NewGuid().ToString();
        public RigOperatingState OptState { get; set; } = RigOperatingState.Instance;
        UdpClient udpClient = new UdpClient();


        public static NetworkThreadRunner GetInstance()
        {
            if (netWorkThread == null)
                netWorkThread = new NetworkThreadRunner();

            return netWorkThread;
        }
        private NetworkThreadRunner()
        {
            Console.WriteLine("in thread");
            FindPorts();
            udpClient.ExclusiveAddressUse = false;
            ServerInit();
            ClientInit();
        }

        private void FindPorts()
        {
            FindFreeUdpPort();
            FindFreeTcpPort();
            Console.WriteLine("tcp port: {0} udp port: {1}", listenTcpPort, listenUdpPort);
        }

        private int FindFreeUdpPort()
        {
            if (listenUdpPort > 1024)
                return listenUdpPort;

            HashSet<int> inUsePorts = new HashSet<int>();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveUdpListeners();

            foreach (IPEndPoint e in endPoints)
            {
                if (e.Port >= basePort)
                    inUsePorts.Add(e.Port);
            }
            if (listenTcpPort > 0 && inUsePorts.Contains(listenTcpPort) == false)
            {
                listenUdpPort = listenTcpPort;
                return listenUdpPort;
            }
            listenUdpPort = SelectFreePort(inUsePorts);
            return listenUdpPort;

        }
        private int FindFreeTcpPort()
        {
            if (listenTcpPort > 1024)
                return listenTcpPort;
            HashSet<int> inUsePorts = new HashSet<int>();
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endPoints = properties.GetActiveTcpListeners();

            foreach (IPEndPoint e in endPoints)
            {
                if (e.Port >= basePort)
                    inUsePorts.Add(e.Port);
            }
            if (listenUdpPort > 0 && inUsePorts.Contains(listenUdpPort) == false)
            {
                listenTcpPort = listenUdpPort;
                return listenTcpPort;
            }
            listenTcpPort = SelectFreePort(inUsePorts);
            return listenTcpPort;
        }

        private int SelectFreePort(HashSet<int> inUsePorts)
        {
            int port = -1;
            for (int i = basePort; i <= topPort; i++)
            {
                if (inUsePorts.Contains(i) == false)
                {
                    port = i;
                    break;
                }
            }
            return port;
        }

        private void ClientInit()
        {
            if (clientThread != null)
                return;
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

            int count = 0;
            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                Console.WriteLine("{0}:recv data {1} address: {2}", count++, returnData,
                    RemoteIpEndPoint.Address.ToString());
                ParseCommand(returnData);
            }
        }

        private static void ParseCommand(string returnData)
        {
            var obj = JsonConvert.DeserializeObject<UdpCmdPacket>(returnData);
            switch (obj.Type)
            {
                case "RigOperatingState":

                    break;
            }
            Console.WriteLine("parsed cmd");
        }


        public void SendBroadcast(UdpCmdPacket payload, int port)
        {
            udpClient.Connect("255.255.255.255", port);
            Byte[] senddata = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload));
            udpClient.Send(senddata, senddata.Length);
        }
    }
}
