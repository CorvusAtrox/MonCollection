using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MonCollection
{
    public class MonSortOrder
    {
        public string Name { get; set; }
        public Dictionary<string,List<int>> Dexes { get; set; }
        public List<int> Foreign { get; set; }
    }
}
