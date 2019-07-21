using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidtWpf.DataModel
{
    public class Prediction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DAU { get; set; }
        public int NewUsers { get; set; }
        public double Revenue { get; set; }
        public int SoldAmount { get; set; }
        public double SoldUSD { get; set; }

    }
}
