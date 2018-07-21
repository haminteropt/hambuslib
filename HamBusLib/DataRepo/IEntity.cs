using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.CouchDB
{
    interface IEntity<T>
    {
        T Id { get; set; }
        string DocType { get; set; }

    }
}
