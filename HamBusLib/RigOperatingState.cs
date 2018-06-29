using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigOperatingState : OperatingState
    {
        public static readonly RigOperatingState _instance = new RigOperatingState();
        public static RigOperatingState Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
