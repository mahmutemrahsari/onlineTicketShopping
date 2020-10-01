﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig.Models
{
    public class NorWay
    {
        public int Id { get; set; }
        public string Epost { get; set; }
        public string Billettype { get; set; }
        public int Pris { get; set; }
        public string FraSted { get; set; }
        public string AvgangersDato { get; set; }
        public string TilSted { get; set; }
        public string ReturDato { get; set; }
        public int Antall { get; set; }
        public string Avgangstid { get; set; }
        public string Ankomsttid { get; set; }
        public string BussNr { get; set; }
        public string AvgangstidR { get; set; }
        public string AnkomsttidR { get; set; }
        public string BussNrR { get; set; }
    }


    
}
