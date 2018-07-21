using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouchDB
{
    public class CouchDbSize
    {
        [JsonProperty(PropertyName = "sizes")]
        public Int64 Sizes { get;  }
        [JsonProperty(PropertyName = "external")]
        public Int64 External { get;  }
        [JsonProperty(PropertyName = "active")]
        public Int64 Active { get; }
    }

    public class CouchCluster
    {
        [JsonProperty(PropertyName = "q")]
        public int Q { get; set; }
        [JsonProperty(PropertyName = "n")]
        public int N { get; set; }
        [JsonProperty(PropertyName = "w")]
        public int W { get; set; }
        [JsonProperty(PropertyName = "r")]
        public int R { get; set; }
    }
    public class CouchDbinfo
    {
        [JsonProperty(PropertyName = "db_name")]
        public string Name { get;  }

        [JsonProperty(PropertyName = "update_seq")]
        public string UpdateSeq { get; }

        [JsonProperty(PropertyName = "sizes")]
        public CouchDbSize Sizes { get;  }

        [JsonProperty(PropertyName = "purge_seq")]
        public Int64 PurgeSeq { get;  }

        [JsonProperty(PropertyName = "other")]
        public Int64 Other { get; }

        [JsonProperty(PropertyName = "doc_del_count")]
        public Int64 DocDeleteCount { get; }

        [JsonProperty(PropertyName = "doc_count")]
        public Int64 DocCount { get; }

        [JsonProperty(PropertyName = "disk_size")]
        public Int64 DiskSize { get; }

        [JsonProperty(PropertyName = "data_size")]
        public Int64 DataSize { get; }

        [JsonProperty(PropertyName = "compact_running")]
        public Boolean CompactRunning { get; }

        [JsonProperty(PropertyName = "cluster")]
        public CouchCluster Cluster { get; }

        [JsonProperty(PropertyName = "instance_start_time")]
        public string InstanceStartTime { get; set; }
    }
}
