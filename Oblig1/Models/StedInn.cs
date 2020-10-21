using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig.Models
{
    public class StedInn
    {
        public int SId { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string StedNavn { get; set; }
    }
}
