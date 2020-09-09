using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hallo.Modell
{
    public class Kunde
    {
        public int id { get; set; }

        public string fornavn { get; set; }

        public string etternavn { get; set; }

        public string epost { get; set; }

        public int telefonnr { get; set; }

    }


    public class Billett
    {
        public int referanseID { get; set; }

        public string fra { get; set; }

        public string til { get; set; }

        public DateTime dato { get; set; }

        public string type { get; set; }

        public double lengde { get; set; }

    }
}
