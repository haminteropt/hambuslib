namespace HamBusLib
{
    using HamBusLib.Models;
    using HamBusLib.Packets;
    using Microsoft.Extensions.Logging;
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
                DirectoryBusGreeting dirServices;
                string url;
                using (var httpClient = new HttpClient())
                {
                    

                    
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Virtual Rig Bus Version 1");
                    while (true)
                    {
                        try
                        {
                            dirServices = DirGreetingList.Instance.First;
                            if (dirServices != null)
                            {
                                url = string.Format("http://{0}:{1}/api/Directory/V{2}/list", dirServices.Host, dirServices.TcpPort, dirServices.MaxVersion);
                                var response = await httpClient.GetStringAsync(new Uri(url));

                                var buses = DocParser.ParsePacket(response) as ActiveBuses;
                                ProcessBus(buses);
                            }
                        } catch (Exception e)
                        {
                            HamBusEnv.Logger.LogInformation($"parse1 {e.Message}");
                            //Console.WriteLine("dir get {0}", e.Message);
                        }
                        Thread.Sleep(HamBusEnv.SleepTimeMs);
                    }
                }
            }
            catch (Exception e)
            {
                HamBusEnv.Logger.LogInformation($"PollDirectoryBus {e.Message}");
                //Console.WriteLine("PollDirectoryBus exceptions: {0}", e.Message);
                threadRunning = false;
            }
        }



        private static void ProcessBus(ActiveBuses buses)
        {
            lock (HamBusEnv.LockObj)
            {
                HamBusEnv.Buses = buses;
            }
        }
    }
}
