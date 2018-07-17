namespace HamBusLib
{
    using HamBusLib.Models;
    using HamBusLib.Packets;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="DirectoryClient" />
    /// </summary>
    public class DirectoryClient
    {
        /// <summary>
        /// Defines the Instance
        /// </summary>
        static public readonly DirectoryClient Instance = new DirectoryClient();

        /// <summary>
        /// Defines the threadRunning
        /// </summary>
        private bool threadRunning = false;

        /// <summary>
        /// Defines the serviceInfoThread
        /// </summary>
        private Thread serviceInfoThread;

        /// <summary>
        /// The StartThread
        /// </summary>
        public void StartThread()
        {
            if (threadRunning == false)
            {
                serviceInfoThread = new Thread(PollDirectoryBus);
                serviceInfoThread.Start();
            }
        }

        /// <summary>
        /// The PollDirectoryBus
        /// </summary>
        private async void PollDirectoryBus()
        {
            threadRunning = true;
            try
            {
                var dirServices = DirGreetingList.Instance.First;
                var url = string.Format("http://{0}:{1}/api/Directory/V{2}/list", dirServices.Host, dirServices.TcpPort, dirServices.MaxVersion);
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Virtual Rig Bus Version 1");
                    while (true)
                    {
                        var response = await httpClient.GetStringAsync(new Uri(url));
                        Console.WriteLine("Json: {0}", response);
                        //var root = JsonConvert.DeserializeObject<dynamic>(response);
                        var buses = JsonConvert.DeserializeObject<List<UdpCmdPacket>>(response);
                        ParseJson(response);
                        Thread.Sleep(HamBusEnv.SleepTimeMs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("PollDirectoryBus exceptions: {0}", e.Message);
                threadRunning = false;
            }
        }

        /// <summary>
        /// The parseJson
        /// </summary>
        /// <param name="response">The response<see cref="string"/></param>
        public static UdpCmdPacket ParseJson(string response)
        {
            UdpCmdPacket packet;
            var udpPacket = JsonConvert.DeserializeObject<UdpCmdPacket>(response);
            switch(udpPacket.DocType)
            {
                case DocTypes.RigBusInfo:
                    packet = RigBusInfo.Parse(response);
                    break;
                case DocTypes.DataBusInfo:
                    packet = DataBusInfo.Parse(response);
                    break;
                case DocTypes.DirectoryBusGreeting:
                    packet = DirectoryBusGreeting.Parse(response);
                    break;
                case DocTypes.OperatingState:
                    packet = OperatingState.Parse(response);
                    break;
                default:
                    packet = null;
                    break;

            }
            return packet;
        }

        private static void ProcessBus(ConcurrentDictionary<string, JsonBase> busList)
        {
            var docType = busList["docType"];
            //switch(docType)
            //{

            //}
        }
    }
}
