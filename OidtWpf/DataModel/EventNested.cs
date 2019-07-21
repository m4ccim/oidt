using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidtWpf.DataModel
{
    public class EventNested
    {
        public int Id { get; set; }
        public string udid { get; set; }
        public DateTime date { get; set; }
        public int event_id { get; set; }
        public Parameters Parameters { get; set; }
    }
    public class Parameters
    {

        public string gender { get; set; }
        public int age { get; set; }
        public string country { get; set; }
        public int stage { get; set; }
        public bool win { get; set; }
        public int time { get; set; }
        public int income { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string item { get; set; }
    }
}
