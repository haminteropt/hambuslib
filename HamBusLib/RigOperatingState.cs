using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigOperatingState : OperatingState
    {
        public delegate void OptStateDelegate(OperatingState state);
        public static readonly RigOperatingState _instance = new RigOperatingState();
        public static RigOperatingState Instance
        {
            get
            {
                return _instance;
            }
        }
        public OptStateDelegate newStateDelegate { get; set; } = null;
        public new OperatingState OperatingStateParse(string returnData)
        {
            var rigState = base.OperatingStateParse(returnData);
            newStateDelegate?.Invoke(rigState);
            return rigState;
        }
    }
}
