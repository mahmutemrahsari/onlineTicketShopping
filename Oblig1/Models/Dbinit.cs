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

                var sted1 = new Sted {StedNavn = "Oslo", Zone ="1" };
                var sted2 = new Sted { StedNavn = "Sandvika", Zone = "2v" };

                context.steder.Add(sted1);
                context.steder.Add(sted2);

                context.SaveChanges();

            }
        }
    }
}