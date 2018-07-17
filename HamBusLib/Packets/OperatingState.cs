using HamBusLib.Packets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib
{

    public class OperatingState : UdpCmdPacket
    {
        public Int64 Freq { get; set; }
        public Int64 FreqA { get; set; }
        public Int64 FreqB { get; set; }
        public string Mode { get; set; }
        public string ModeStd { get; set; }
        public bool Tx { get; set; }
        public string Vfo { get; set; }
        public string Xit { get; set; }
        public string Split { get; set; }
        public string Rit { get; set; }
        public OperatingState()
        {
            this.Tx = false;
            this.Mode = ModeConst.USB;
            this.DocType = DocTypes.OperatingState;
        }
        public OperatingState OperatingStateParse(string returnData)
        {
            var rigState = JsonConvert.DeserializeObject<OperatingState>(returnData);
            Freq = rigState.Freq;
            FreqA = rigState.FreqA;
            FreqB = rigState.FreqB;
            ModeStd = rigState.ModeStd;
            Mode = rigState.Mode;
            Tx = rigState.Tx;
            Vfo = rigState.Vfo;
            Xit = rigState.Xit;
            Split = rigState.Split;
            Rit = rigState.Rit;
            Id = rigState.Id;

            return rigState;
        }
        static public OperatingState Parse(string jsonStr)
        {
            var data = JsonConvert.DeserializeObject<OperatingState>(jsonStr);
            return data;
        }

    }
}
