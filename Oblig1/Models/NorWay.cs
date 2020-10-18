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
        //([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})
        [EmailAddress(ErrorMessage = "Ikke riktig Epost former")]
        public string Epost { get; set; }
        //[RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,10}$")]
        public string Billettype { get; set; }
        // [RegularExpression(@"^\d+$")]
        //[RegularExpression(@"^\d{0,4}(\.\d{0,2})?$")]
        public int Pris { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}")]
        public string FraSted { get; set; }
        // \d{2,2}:\d{2,2}:\d{2,2}
     //   [RegularExpression(@"\d{2,2}/\d{2,2}/\d{4,4}$")]
        public string AvgangersDato { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{4,20}$")]
        public string TilSted { get; set; }
       // [RegularExpression(@"\d{2}-\d{2}-\d{4}$")]
        //Hvorfor (-) i denne og ikke avganger dato?
        public string ReturDato { get; set; }
        [RegularExpression(@"^\d{0,20}$")]
       //kansje vi kan ta den med
        public int Antall { get; set; }
       // [RegularExpression(@"\d{2}:\d{2}$")]
        public string Avgangstid { get; set; }
       // [RegularExpression(@"\d{2}:\d{2}$")]
        public string Ankomsttid { get; set; }
        //[RegularExpression(@"[0-9. \-]{2,20}$")]
        public string BussNr { get; set; }
       // [RegularExpression(@"\d{2}:\d{2}$")]
        public string AvgangstidR { get; set; }
       // [RegularExpression(@"\d{2}:\d{2}$")]
        public string AnkomsttidR { get; set; }
        //[RegularExpression(@"[0-9. \-]{2,20}$")]
        public string BussNrR { get; set; }
    }



}
