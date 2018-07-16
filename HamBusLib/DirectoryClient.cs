namespace HamBusLib
{
    using HamBusLib.Models;
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
                        parseJson(response);
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
        private static void parseJson(string response)
        {

            var busItem = new ConcurrentDictionary<string,JsonBase>();
            JsonBase node = new JsonBase();
            string propName = "";


            JsonTextReader reader = new JsonTextReader(new StringReader(response));
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.StartArray:
                        break;
                    case JsonToken.StartObject:
                        break;
                    case JsonToken.Integer:
                        node = new JsonNode<Int64>();
                        (node as JsonNode<Int64>).Value = int.Parse(reader.Value.ToString());
                        node.PropName = propName;
                        busItem[propName] = node;
                        break;
                    case JsonToken.Boolean:
                        node = new JsonNode<Boolean>();
                        (node as JsonNode<Boolean>).Value = Boolean.Parse(reader.Value.ToString());
                        node.PropName = propName;
                        busItem[propName] = node;
                        break;
                    case JsonToken.Float:
                        node = new JsonNode<float>();
                        (node as JsonNode<float>).Value = float.Parse(reader.Value.ToString());
                        node.PropName = propName;
                        busItem[propName] = node;
                        break;
                    case JsonToken.String:
                        node = new JsonNode<string>();
                        (node as JsonNode<string>).Value = reader.Value.ToString();
                        node.PropName = propName;
                        busItem[propName] = node;
                        break;
                    case JsonToken.Date:
                        node = new JsonNode<DateTime>();
                        (node as JsonNode<DateTime>).Value = DateTime.Parse(reader.Value.ToString());
                        node.PropName = propName;
                        busItem[propName] = node;
                        break;
                    case JsonToken.EndObject:
                        ProcessBus(busItem);
                        busItem = new ConcurrentDictionary<string, JsonBase>();
                        break;
                    case JsonToken.PropertyName:
                        propName = reader.Value.ToString();
                        break;

                }

                node = null;
                if (reader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                }
                else
                {
                    Console.WriteLine("Token: {0}", reader.TokenType);
                }
            }
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
