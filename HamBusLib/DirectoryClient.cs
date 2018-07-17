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
                DirectoryBusGreeting dirServices;
                string url;
                //url = string.Format("http://{0}:{1}/api/Directory/V{2}/list", dirServices.Host, dirServices.TcpPort, dirServices.MaxVersion);
                using (var httpClient = new HttpClient())
                {
                    

                    
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Virtual Rig Bus Version 1");
                    while (true)
                    {
                        dirServices = DirGreetingList.Instance.First;
                        if (dirServices != null)
                        {
                            url = string.Format("http://{0}:{1}/api/Directory/V{2}/list", dirServices.Host, dirServices.TcpPort, dirServices.MaxVersion);
                            var response = await httpClient.GetStringAsync(new Uri(url));
                            Console.WriteLine("Json: {0}", response);

                            var buses = JsonConvert.DeserializeObject<ActiveBuses>(response);
                            ProcessBus(buses);
                        }
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



        private static void ProcessBus(ActiveBuses buses)
        {
            lock (HamBusEnv.LockObj)
            {
                HamBusEnv.Buses = buses;
            }
        }
    }
}
