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

                var fra1 = new reiseRundt_Fra { FraSted = "Oslo" };

                var til1 = new reiseRundt_Til { TilSted = "Gardermoen" };


                context.reiserFra.Add(fra1);
                context.reiserTil.Add(til1);

                context.SaveChanges();

            }
        }
    }
}
