using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Models
{
    public class PrisInn
    {
        public int TId { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string Type { get; set; }

        [RegularExpression(@"^\+?[1-9][0-9]*$")]
        //[RegularExpression(@"^([1 - 9][0 - 9]*){1,3}$")]
        public int Pris { get; set; }
    }
}
