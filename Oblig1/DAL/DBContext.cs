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

    public class Adminere
    {
        [Key]
        public int AId { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }

    public class Billett
    {
        [Key]
        public int BId { get; set; }
        public string FraSted { get; set; }
        public string AvgangersDato { get; set; }
        public string TilSted { get; set; }
        public string Billettype { get; set; }
        public int Pris { get; set; }
        public int Antall { get; set; }
        public string Avgangstid { get; set; }
        public string Ankomsttid { get; set; }
        public string BussNr { get; set; }
        public string ReturDato { get; set; }
        public string AvgangstidR { get; set; }
        public string AnkomsttidR { get; set; }
        public string BussNrR { get; set; }
        //public virtual List<PrisType> Pristypes { get; set; }
    }

    public class Sted
    {
        [Key]
        public int SId { get; set; }
        public string StedNavn { get; set; }
    }

    public class Rute
    {
        [Key]
        public int RId { get; set; }
        public string BussNR { get; set; }
        public string FraRute { get; set; }
        public string TilRute { get; set; }
        public string Dato { get; set; }
        public string AvgangsTid { get; set; }
        public string AnkomstTid { get; set; }
    }



    public class PrisType
    {
        [Key]
        public int TId { get; set; }
        public int pris { get; set; }
        public string type { get; set; }
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
        public DbSet<Rute> ruter { get; set; }
        public DbSet<PrisType> pristype { get; set; }
        public DbSet<Adminere> Adminere { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}