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
            listenTcpPort = IpPorts.TcpPort;
            listenUdpPort = IpPorts.UdpPort;
            udpClient.ExclusiveAddressUse = false;
            ServerInit();
            ClientInit();
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

            while (true)
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                ParseCommand(returnData);
            }
        }

        private void ParseCommand(string returnData)
        {
            var obj = JsonConvert.DeserializeObject<UdpCmdPacket>(returnData);
            switch (obj.Type)
            {
                case "RigOperatingState":
                    OptState.OperatingStateParse(returnData);
                    break;
            }
        }

        public void SendBroadcast(UdpCmdPacket payload, int port)
        {
            udpClient.Connect("255.255.255.255", port);
            Byte[] senddata = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(payload));
            udpClient.Send(senddata, senddata.Length);
        }
    }
}
