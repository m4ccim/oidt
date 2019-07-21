using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OidtWpf.DataModel
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid udid { get; set; }
        public string parametersGender { get; set; }
        public int parametersAge { get; set; }
        public string parametersCountry { get; set; }
        public float spentMoney { get; set; }
        public double earnedGameMoney { get; set; }
        public double spentGameMoney { get; set; }
        public int boughtItems { get; set; }
        public bool isCheater { get; set; }

        public int startedStages { get; set; }
        public int finishedStage { get; set; }
        public int wins { get; set; }

        public string Tier { get; set; }
    

    }
}
