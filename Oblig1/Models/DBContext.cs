using Microsoft.EntityFrameworkCore;
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
        [Key]
        public int Id { get; set; }
        public string Epost { get; set; }
        public virtual List<Billett> Billetter { get; set; }
        //public virtual List<Sted> Steder { get; set; }
    }

    public class Billett
    {
        [Key]
        public int ReferanseID { get; set; }
        public string Billettype { get; set; }
        public int Pris { get; set; }
        public string FraSted { get; set; }
        public string AvgangersDato { get; set; }
        public string TilSted { get; set; }
        public string ReturDato { get; set; }
    }

    public class Sted
    {
        [Key]
        public int RId { get; set; }
        public string StedNavn { get; set; }
        public string Zone { get; set; }

    }

    public class BillettContext : DbContext
    {
        public BillettContext(DbContextOptions<BillettContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Kunde> kunder { get; set; }
        public DbSet<Billett> Billetter { get; set; }
        public DbSet<Sted> steder { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
