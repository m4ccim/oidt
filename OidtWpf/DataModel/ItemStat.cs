using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidtWpf.DataModel
{
    public class ItemStat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double CurrencySpent{ get; set; }
        public double USD { get; set; }
    }
}
