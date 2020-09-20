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
        //public String Fornavn { get; set; }
        //public String Etternavn { get; set; }
        public string Epost { get; set; }
        //public string Telefonnr { get; set; }
        //virtual public Billett Billett  {get;set;}
        public virtual List<Billett> Billetter { get; set; }
        public virtual List<reiseRundt_Fra> reiserFra { get; set; }
        public virtual List<reiseRundt_Til> reiserTil { get; set }
    }

    public class Billett
    {
        [Key]
        //[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReferanseID { get; set; }
        public string Billettype { get; set; }
        public int Pris { get; set; }
       // public string FraSted { get; set; }
        public string AvgangersDato { get; set; }
        //public string TilSted { get; set; }
        public string ReturDato { get; set; }
    }

    public class reiseRundt_Fra
    {
        [Key]

        public string FraSted { get; set; }

    }

    public class reiseRundt_Til
    {
        [Key]

        public string TilSted { get; set; }
    }


    public class BillettContext : DbContext
    {
    public BillettContext(DbContextOptions<BillettContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
        public DbSet<Kunde> kunder { get; set; }
        public DbSet<Billett> Billetter { get; set; }
        public DbSet<reiseRundt_Fra> reiserFra { get; set; }
        public DbSet<reiseRundt_Til> reiserTil { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

