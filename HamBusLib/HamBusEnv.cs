﻿using HamBusLib.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;


namespace HamBusLib
{
    public class HamBusEnv
    {
        public static Object LockObj = new Object();
        public static int SleepTimeMs { get; set; } = 3000;
        public static SortedDictionary<string, string> ConfigPairs = new SortedDictionary<string, string>();
        public static string GetHome()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            path = path + "/.hambus";
            System.IO.Directory.CreateDirectory(path);
            return path;
        }
        public static string GetAppDataPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return path;
        }
        public static string ReadConfigJson(string path)
        {
            if (File.Exists(path) == false)
            {
                var confPath = File.Create(path);
                var confWritter = new StreamWriter(confPath);
                confWritter.WriteLine("{}");
                confWritter.Dispose();
            }
            string rc = File.ReadAllText(path);
            return rc;
        }
        public static void WriteConfigJson(string path, string content)
        {
            if (File.Exists(path) == false)
            {
                var confPath = File.Create(path);
            }
            File.WriteAllText(path, content);
        }
        public static string GetOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "Win";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "OSX";

            return "Linux";
        }
        public static ActiveBuses Buses;
        public static ILoggerFactory loggerFactor;
        public static ILogger Logger;

    }
}
