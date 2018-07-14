using HamBusLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HamBusLib
{
    public class DirectoryClient
    {
        static public readonly DirectoryClient Instance = new DirectoryClient();
        private bool threadRunning = false;
        private Thread serviceInfoThread;

        public void StartThread()
        {
            if (threadRunning == false)
            {
                serviceInfoThread = new Thread(PollDirectoryBus);
                serviceInfoThread.Start();
            }
        }

        private async  void PollDirectoryBus()
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
                        var root = JsonConvert.DeserializeObject<dynamic>(response);
                        Thread.Sleep(HamBusEnv.SleepTimeMs);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("PollDirectoryBus exceptions: {0}", e.Message);
                threadRunning = false;
            }


        }

    }
}
