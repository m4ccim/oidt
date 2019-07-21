using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidtWpf.DataModel
{
    public class StageStat
    {
        [Key]
        public int StageNum { get; set; }
        public int Started { get; set; }
        public int Finished { get; set; }
        public int Wins { get; set; }
        public double Income { get; set; }
        public double Revenue { get; set; }
    }
}
