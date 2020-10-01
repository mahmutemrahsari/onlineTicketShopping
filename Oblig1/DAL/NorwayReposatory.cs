using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig.Models;
using Oblig1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oblig1.DAL;

namespace Oblig1.DAL
{
    public class NorwayReposatory : INorwayReposatory
    {
        private readonly INorwayReposatory _db;

<<<<<<<< HEAD:Oblig1/DAL/NorwayReposatory.cs
        public NorwayReposatory(BillettContext db)
========
        public NorWayController(INorwayReposatory db)
>>>>>>>> d0c5b98d22aaa531479120f10d54c8ebbe743429:Oblig1/Controllers/NorWayController.cs
        {
            _db = db;
        }

        public async Task<bool> Lagre(NorWay BestilleBillett)
        {
<<<<<<<< HEAD:Oblig1/DAL/NorwayReposatory.cs
            try
            {

                var bestill = new Billett()
                {
                    AvgangersDato = BestilleBillett.AvgangersDato,
                    ReturDato = BestilleBillett.ReturDato,
                    FraSted = BestilleBillett.FraSted,
                    TilSted = BestilleBillett.TilSted,
                    Pris = BestilleBillett.Pris,
                    Billettype = BestilleBillett.Billettype,
                    Antall = BestilleBillett.Antall,
                    Avgangstid = BestilleBillett.Avgangstid,
                    Ankomsttid = BestilleBillett.Ankomsttid,
                    BussNr = BestilleBillett.BussNr,
                    AvgangstidR = BestilleBillett.AvgangstidR,
                    AnkomsttidR = BestilleBillett.AnkomsttidR,
                    BussNrR = BestilleBillett.BussNrR
                };
========
>>>>>>>> d0c5b98d22aaa531479120f10d54c8ebbe743429:Oblig1/Controllers/NorWayController.cs

            return await _db.Lagre(BestilleBillett);

<<<<<<<< HEAD:Oblig1/DAL/NorwayReposatory.cs
                kunde.Billetter = new List<Billett>();
                kunde.Billetter.Add(bestill);
                _db.kunder.Add(kunde);
                await _db.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
========
>>>>>>>> d0c5b98d22aaa531479120f10d54c8ebbe743429:Oblig1/Controllers/NorWayController.cs
        }

        public async Task<List<NorWay>> HentAlle()
        {
<<<<<<<< HEAD:Oblig1/DAL/NorwayReposatory.cs

            List<Kunde> alleKunder = await _db.kunder.ToListAsync();
            List<NorWay> alleBilletter = new List<NorWay>();

            foreach (var kunde in alleKunder)
            {
                foreach (var bestill in kunde.Billetter)
                {
                    var enBestilling = new NorWay()
                    {
                        Epost = kunde.Epost,
                        AvgangersDato = bestill.AvgangersDato,
                        ReturDato = bestill.ReturDato,
                        FraSted = bestill.FraSted,
                        TilSted = bestill.TilSted,
                        Pris = bestill.Pris,
                        Billettype = bestill.Billettype,
                        Antall = bestill.Antall,
                        Avgangstid = bestill.Avgangstid,
                        Ankomsttid = bestill.Ankomsttid,
                        BussNr = bestill.BussNr,
                        AvgangstidR = bestill.AvgangstidR,
                        AnkomsttidR = bestill.AnkomsttidR,
                        BussNrR = bestill.BussNrR
                    };
                    alleBilletter.Add(enBestilling);
                }
            }
            return alleBilletter;
        }


        //Hente ut til og fra stedene
========

            return await _db.HentAlle();
        }

>>>>>>>> d0c5b98d22aaa531479120f10d54c8ebbe743429:Oblig1/Controllers/NorWayController.cs
        public async Task<List<Sted>> HentStop()
        {
            return await _db.HentStop();
        }

        public async Task<List<PrisType>> HentPrisType()
        {
            return await _db.HentPrisType();
        }

        public async Task<List<Rute>> HentRute(InfoMedRute info)
        {

<<<<<<<< HEAD:Oblig1/DAL/NorwayReposatory.cs
                //Finn tilpasset rute som har sammen dato og stedene ved bruk av LINQ
                var finnRute = (from passRute in alleRuter
                                where passRute.Dato == info.dato && passRute.FraRute == info.fSted && passRute.TilRute == info.tSted
                                select passRute).ToList();
========
            return await _db.HentRute(info);
>>>>>>>> d0c5b98d22aaa531479120f10d54c8ebbe743429:Oblig1/Controllers/NorWayController.cs

        }
    }
}