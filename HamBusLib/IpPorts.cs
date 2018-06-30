using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace HamBusLib
{
    public class IpPorts
    {
        public const int basePort = 7301;
        public const int topPort = 7600;
        private static int listenTcpPort = -1;
        private static int listenUdpPort = -1;
        public static int UdpPort
        {
            get
            {
                if (listenUdpPort < 1024)
                    FindPorts();
                return listenUdpPort;
            }
        }
        public static int TcpPort
        {
            get
            {
                if (listenTcpPort < 1024)
                    FindPorts();
                return listenTcpPort;
            }
        }

        public static void FindPorts()
        {
            FindFreeUdpPort();
            FindFreeTcpPort();
            Console.WriteLine("port: {0}", listenUdpPort);
        }

        private static int FindFreeUdpPort()
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
        private static int FindFreeTcpPort()
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

        private static int SelectFreePort(HashSet<int> inUsePorts)
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

    }
}
