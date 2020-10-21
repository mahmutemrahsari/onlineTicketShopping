using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Models
{
    public class RuteInn
    {
        public int RId { get; set; }

        [RegularExpression(@"[0-9. \-]{2,4}$")]
        public string BussNR { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string FraRute { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string TilRute { get; set; }

        [Required]
        public string Dato { get; set; }

        [Required]
        public string AvgangsTid { get; set; }

        [Required]
        public string AnkomstTid { get; set; }
    }
}
