using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidtWpf.DataModel
{
    public class DayStat
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DAU { get; set; }
        public int NewUsers { get; set; }
        public double Revenue { get; set; }
        public double ExchangeRate { get; set; }
    }
}
