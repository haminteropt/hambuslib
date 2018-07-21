using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CouchDB.Model
{
    public class Credentials
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
