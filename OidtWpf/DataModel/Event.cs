using Newtonsoft.Json;
using OidtWpf.DataModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OidtWpf
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string udid { get; set; }
        public DateTime date { get; set; }
        public int event_id { get; set;}
        public string parametersGender { get; set; }
        public int parametersAge { get; set; }
        public string parametersCountry { get; set; }
        public int parametersStage { get; set; }
        public bool parametersWin { get; set; }
        public int parametersTime { get; set; }
        public int parametersIncome { get; set; }
        public string parametersName { get; set; }
        public float parametersPrice { get; set; }
        public string parametersItem { get; set; }
    }
}