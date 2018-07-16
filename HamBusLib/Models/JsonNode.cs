using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Models
{
    public class JsonBase
    {
        public string PropName { get; set; }
    }
    public class JsonNode<T> : JsonBase
    {

        public T Value { get; set; }
        public JsonNode() { }
    }
}
