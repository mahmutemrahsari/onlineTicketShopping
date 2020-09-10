using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.Models
{
    
        public class Kunde
        {
            public int Id { get; set; }
            //public String Fornavn { get; set; }
            //public String Etternavn { get; set; }
            public String Epost { get; set; }
            public String Telefonnr { get; set; }
        }

        public class Billett
        {
            [Key]
            [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
            public String Billettype { get; set; }
            public int Pris { get; set; }
            public String FraSted { get; set; }
            public String AvgangersDato { get; set; }
            public String TilSted { get; set; }
            public String ReturDato { get; set; }
        }

        public class BillettContext : DbContext
    {
        public BillettContext(DbContextOptions<BillettContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
    }

