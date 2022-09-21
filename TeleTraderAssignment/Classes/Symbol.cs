using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeleTraderAssignment
{
    public class Symbol
    {
        public string id { get; set; }
        public string name { get; set; }
        public string ticker { get; set; }
        public float lastPrice { get; set; }
        public float highPrice { get; set; }
        public float lowPrice { get; set; }
        public float bidPrice { get; set; }
        public float askPrice { get; set; }       
    }
}
