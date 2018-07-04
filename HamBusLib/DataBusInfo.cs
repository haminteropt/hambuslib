using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class DataBusInfo : UdpCmdPacket
    {
        private static DataBusInfo instance = null;
        private DataBusInfo()
        {
            DocType = "DataBusInfo";
        }
        public static DataBusInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataBusInfo();
                }
                return instance;
            }
        }
    }
}

