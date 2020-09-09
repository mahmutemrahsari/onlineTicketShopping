using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hallo.Modell
{
    public class KundeDB :DbContext
    {
        public KundeDB (DbContextOptions<KundeDB>options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Kunde> Kunder { get; set; }
    }


    public class BillettDB : DbContext
    {
        public BillettDB(DbContextOptions<BillettDB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Billett> Billetter { get; set; }
    }


}
