using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Models
{
    public class JsonBase
    {
        public string name { get; set; }
        public string PropName { get; set; }
    }
    public class JsonNode<T> : JsonBase
    {

        public T value { get; set; }
        public JsonNode() { }
    }
}
