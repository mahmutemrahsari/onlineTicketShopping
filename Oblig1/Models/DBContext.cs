﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Oblig.Controllers;
using Oblig.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig.Models
{
        
           
        public class Kunde
        {
            public int ID { get; set; }
            //public String Fornavn { get; set; }
            //public String Etternavn { get; set; }
            public string Epost { get; set; }
            public int Telefonnr { get; set; }
        }

        public class Billett
        {
            [Key]
            [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
            public string Billettype { get; set; }
            public int Pris { get; set; }
            public string FraSted { get; set; }
            public string AvgangersDato { get; set; }
            public string TilSted { get; set; }
            public string ReturDato { get; set; }
        }

        public class BillettContext : DbContext
        {

        public BillettContext(DbContextOptions<BillettContext> options) : base(options)
            {
                Database.EnsureCreated();
            }

        public DbSet<NorWay> Billetter { get; set; }
        }
}

