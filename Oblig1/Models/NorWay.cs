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

        [RegularExpression(@"^[ÆØÅæøåA-Za-z0-9._%+-]+@(?:[ÆØÅæøåA-Za-z0-9-]+.)+[A-Za-z]{2,6}$")]
        public string Epost { get; set; }

        [Required]
        public string Billettype { get; set; }

        [Required]
        public int Pris { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string FraSted { get; set; }

        [Required]
        public string AvgangersDato { get; set; }

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}$")]
        public string TilSted { get; set; }

        [Required]
        public string ReturDato { get; set; }

        [RegularExpression(@"^\+?[1-9][0-9]*$")]
        public int Antall { get; set; }

        [Required]
        public string Avgangstid { get; set; }

        [Required]
        public string Ankomsttid { get; set; }

        [Required]
        public string BussNr { get; set; }

        [Required]
        public string AvgangstidR { get; set; }

        [Required]
        public string AnkomsttidR { get; set; }

        [Required]
        public string BussNrR { get; set; }
    }



}
