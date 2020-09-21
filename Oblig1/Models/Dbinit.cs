using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oblig.Models;

namespace Oblig1.Models
{
    public class Dbinit
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BillettContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //initialiserer fra og til steder
                var sted1 = new Sted {StedNavn = "Oslo"};
                var sted2 = new Sted { StedNavn = "Sandvika"};

                context.steder.Add(sted1);
                context.steder.Add(sted2);

                //initialiserer rute 
                var rute1 = new Rute { FraRute = "Oslo", TilRute = "Sandvika", Time = "14:00", Dato= "2020-09-30"};
                var rute2 = new Rute { FraRute = "Oslo", TilRute = "Sandvika", Time = "17:00", Dato = "2020-09-30" };
                var rute3 = new Rute { FraRute = "Sandvika", TilRute = "Sandvika", Time = "13:00", Dato = "2020-09-30" };
                var rute4 = new Rute { FraRute = "Oslo", TilRute = "Sandvika", Time = "17:00", Dato = "2020-10-02" };
                context.ruter.Add(rute1);
                context.ruter.Add(rute2);
                context.ruter.Add(rute3);
                context.ruter.Add(rute4);

                context.SaveChanges();

            }
        }
    }
}