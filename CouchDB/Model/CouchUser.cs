using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CouchDB
{
    public class CouchUser
    {
        [JsonProperty(PropertyName = "name")]
        string Name { get; set; }
        [JsonProperty(PropertyName = "roles")]
        List<string> Roles { get; set; } = new List<string>();
        
    }
}
