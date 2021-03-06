﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            public int Id { get; set; }
            //public String Fornavn { get; set; }
            //public String Etternavn { get; set; }
            public String Epost { get; set; }
            public String Telefonnr { get; set; }
            virtual public Billett Billett  {get;set;}
        }

        public class Billett
        {
            [Key]
            [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int ReferanseID { get; set; }
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
        public DbSet<Kunde> kunder { get; set; }
        public DbSet<Billett> Billetter { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
    }

