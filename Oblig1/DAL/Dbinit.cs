using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Oblig.Models;
using Oblig1.DAL;

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
                var sted3 = new Sted { StedNavn = "Lillehammer" };
                var sted4 = new Sted { StedNavn = "Lysaker" };

                context.Steder.Add(sted1);
                context.Steder.Add(sted2);
                context.Steder.Add(sted3);
                context.Steder.Add(sted4);

                //initialiserer rute 
                var rute1 = new Rute { BussNR = "230", FraRute = "Oslo", TilRute = "Sandvika", AvgangsTid = "14:00", AnkomstTid = "15:00", Dato = "2020-12-05" };
                var rute2 = new Rute { BussNR = "150", FraRute = "Oslo", TilRute = "Sandvika", AvgangsTid = "17:00", AnkomstTid = "19:30", Dato = "2020-12-05" };
                var rute3 = new Rute { BussNR = "100", FraRute = "Sandvika", TilRute = "Lillehammer", AvgangsTid = "13:00", AnkomstTid = "16:00", Dato = "2020-11-30" };
                var rute4 = new Rute { BussNR = "230", FraRute = "Sandvika", TilRute = "Oslo", AvgangsTid = "09:30", AnkomstTid = "15:00", Dato = "2020-12-20" };
                var rute5 = new Rute { BussNR = "230", FraRute = "Lysaker", TilRute = "Oslo", AvgangsTid = "17:00", AnkomstTid = "19:00", Dato = "2020-11-30" };
                context.Ruter.Add(rute1);
                context.Ruter.Add(rute2);
                context.Ruter.Add(rute3);
                context.Ruter.Add(rute4);
                context.Ruter.Add(rute5);

                //initialiserer pris
                var Voksen = new PrisType { Type = "Voksen", Pris = 50 };
                var Barn = new PrisType { Type = "Barn", Pris = 25 };
                var Student = new PrisType { Type = "Student", Pris = 35 };
                var Ungdom = new PrisType { Type = "Ungdom", Pris = 30 };
                var Honnor = new PrisType { Type = "Honnor", Pris = 20 };
                var Verneplikt = new PrisType { Type = "Verneplikt", Pris = 25 };
                var Ledsager = new PrisType { Type = "Ledsager", Pris = 20 };
                context.Pristype.Add(Voksen);
                context.Pristype.Add(Barn);
                context.Pristype.Add(Student);
                context.Pristype.Add(Ungdom);
                context.Pristype.Add(Honnor);
                context.Pristype.Add(Verneplikt);
                context.Pristype.Add(Ledsager);

                //initialiserer/lag en admins konto
                string passord = "Admin123";
                byte[] salt = NorwayReposatory.LagSalt();
                byte[] hash = NorwayReposatory.LagHash(passord, salt);
                var admin = new Adminere { Brukernavn = "Admin", Passord = hash, Salt = salt };
                context.Adminere.Add(admin);

                context.SaveChanges();

            }
        }
    }
}