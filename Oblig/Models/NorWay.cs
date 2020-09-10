using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig.Models
{
    public class NorWay
    {
        public int Id { get; set; }
        //public String Fornavn { get; set; }
        //public String Etternavn { get; set; }
        public String Epost { get; set; }
        public String Telefonnr { get; set; }
        public int ReferanseID { get; set; }
        public String Billettype { get; set; }
        public int Pris { get; set; }
        public String FraSted { get; set; }
        public String AvgangersDato { get; set; }
        public String TilSted { get; set; }
        public String ReturDato { get; set; }
    }
}
