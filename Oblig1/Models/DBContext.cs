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
            public DbSet<Norway> Id { get; set; }
            //public String Fornavn { get; set; }
            //public String Etternavn { get; set; }
            public DbSet<Norway> Epost { get; set; }
            public DbSet<Norway> Telefonnr { get; set; }
        }

        public class Billett
        {
            [Key]
            [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
            public DbSet<Norway> Billettype { get; set; }
            public int Pris { get; set; }
            public DbSet<Norway> FraSted { get; set; }
            public DbSet<Norway> AvgangersDato { get; set; }
            public DbSet<Norway> TilSted { get; set; }
            public DbSet<Norway> ReturDato { get; set; }
        }

        public class BillettContext : DbContext
    {
        public BillettContext(DbContextOptions<BillettContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
    }

