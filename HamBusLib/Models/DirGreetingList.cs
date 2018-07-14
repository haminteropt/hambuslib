using System;
using System.Collections.Generic;
using System.Text;

namespace HamBusLib.Models
{
    public class DirGreetingList
    {
        public List<DirectoryBusGreeting> List { get; set; } = new List<DirectoryBusGreeting>();
        static public DirGreetingList Instance { get; set; } = new DirGreetingList();

        private DirGreetingList() { }
        public DirectoryBusGreeting First
        {
            get {
                if (List.Count >= 1)
                    return List[0];
                return null;
            }
        }

    }
}
