using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.DataBus
{
    public class DataConnection
    {
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5984;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
