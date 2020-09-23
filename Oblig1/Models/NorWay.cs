using System;
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
        //public string StedNavn { get; set; }
        //public string Zone { get; set; }
    }

    public class BillettPris
    {
        public int Id { get; set; }
        public int Voksenpris { get; set; }
        public int Studentpris { get; set; }
        public int Barnepris { get; set; }
        public int Ungdompris { get; set; }
        public int Honnorpris { get; set; }
        public int Vernepliktpris { get; set; }
        public int Ledsagerpris { get; set; }
    }

    
}
