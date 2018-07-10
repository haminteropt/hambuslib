using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{
    public class RigOperatingState : OperatingState
    {
        public delegate void OptStateDelegate(OperatingState state);
        public static RigOperatingState Instance { get; set; } = new RigOperatingState();

        public OptStateDelegate newStateDelegate { get; set; } = null;
        public new OperatingState OperatingStateParse(string returnData)
        {
            var rigState = base.OperatingStateParse(returnData);
            newStateDelegate?.Invoke(rigState);
            return rigState;
        }
    }
}
