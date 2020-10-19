using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig.Models
{
    public class NorWay
    {
        public int Id { get; set; }
        
        [EmailAddress(ErrorMessage = "Ikke riktig Epost former")]
        public string Epost { get; set; }
        
        public string Billettype { get; set; }
        
        public int Pris { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string FraSted { get; set; }

        [Required]
        public string AvgangersDato { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}$")]
        public string TilSted { get; set; }

        [Required]
        public string ReturDato { get; set; }

        [Required]
        public int Antall { get; set; }
        // [RegularExpression(@"\d{2}:\d{2}$")]
        public string Avgangstid { get; set; }
        // [RegularExpression(@"\d{2}:\d{2}$")]
        public string Ankomsttid { get; set; }
        [RegularExpression(@"[0-9. \-]{2,20}$")]
        public string BussNr { get; set; }
        // [RegularExpression(@"\d{2}:\d{2}$")]
        public string AvgangstidR { get; set; }
        // [RegularExpression(@"\d{2}:\d{2}$")]
        public string AnkomsttidR { get; set; }

        public string BussNrR { get; set; }
    }



}
