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
                var sted3 = new Sted { StedNavn = "Lillehamme" };
                var sted4 = new Sted { StedNavn = "Lysaker" };

                context.steder.Add(sted1);
                context.steder.Add(sted2);
                context.steder.Add(sted3);
                context.steder.Add(sted4);

                //initialiserer rute 
                var rute1 = new Rute { BussNR = "230", FraRute = "Oslo", TilRute = "Sandvika", AvgangsTid = "14:00", AnkomstTid = "15:00", Dato = "2020-10-05"};
                var rute2 = new Rute { BussNR = "150", FraRute = "Oslo", TilRute = "Sandvika", AvgangsTid = "17:00", AnkomstTid = "19:30", Dato = "2020-10-05" };
                var rute3 = new Rute { BussNR = "100", FraRute = "Sandvika", TilRute = "Lillehamme", AvgangsTid = "13:00", AnkomstTid = "16:00", Dato = "2020-11-30" };
                var rute4 = new Rute { BussNR = "230", FraRute = "Sandvika", TilRute = "Oslo", AvgangsTid = "09:30", AnkomstTid = "15:00", Dato = "2020-10-20" };
                context.ruter.Add(rute1);
                context.ruter.Add(rute2);
                context.ruter.Add(rute3);
                context.ruter.Add(rute4);

                //initialiserer pris
                var Voksen = new PrisType { type = "Voksen", pris = 50 };
                var Barn = new PrisType { type = "Barn", pris = 25 };
                var Student = new PrisType { type = "Student", pris = 35 };
                var Ungdom = new PrisType { type = "Ungdom", pris = 30 };
                var Honnor = new PrisType { type = "Honnor", pris = 20 };
                var Verneplikt = new PrisType { type = "Verneplikt", pris = 25 };
                var Ledsager = new PrisType { type = "Ledsager", pris = 20 };
                context.pristype.Add(Voksen);
                context.pristype.Add(Barn);
                context.pristype.Add(Student);
                context.pristype.Add(Ungdom);
                context.pristype.Add(Honnor);
                context.pristype.Add(Verneplikt);
                context.pristype.Add(Ledsager);

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