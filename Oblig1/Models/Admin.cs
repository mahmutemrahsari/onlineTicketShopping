using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Models
{
    public class Admin
    {
        [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,10}$")]
        public string Brukernavn { get; set; }
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{6,}$")]
        public string Passord { get; set; }
    }
}
