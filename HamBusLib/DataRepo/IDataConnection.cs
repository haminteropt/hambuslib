using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.CouchDB
{
    public interface IDataConnection
    {
        string ConnectionString { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
