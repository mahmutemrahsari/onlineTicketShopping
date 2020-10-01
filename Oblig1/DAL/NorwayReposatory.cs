using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig.Models;
using Oblig1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.DAL
{
    public class NorwayReposatory : INorwayReposatory
    {
        private readonly BillettContext _db;

        public NorwayReposatory(BillettContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<bool> Lagre(NorWay BestilleBillett)
        {
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

                var kunde = new Kunde()
                {
                    Epost = BestilleBillett.Epost
                };

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
        }

        public async Task<List<NorWay>> HentAlle()
        {

            List<Kunde> alleKunder = await _db.kunder.ToListAsync();
            List<NorWay> alleBilletter = new List<NorWay>();

            //Hente bare den nyeste billett informasjon
            Kunde last = alleKunder.OrderByDescending(k => k.Id).First();

                foreach (var bestill in last.Billetter)
                {
                    var enBestilling = new NorWay()
                    {
                        Epost = last.Epost,
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
            return alleBilletter;
        }


        //Hente ut til og fra stedene
        public async Task<List<Sted>> HentStop()
        {
            List<Sted> alleSteder = await _db.steder.ToListAsync();
            return alleSteder;
        }

        public async Task<List<PrisType>> HentPrisType()
        {

            List<PrisType> allePrisType = await _db.pristype.ToListAsync();
            return allePrisType;
        }


        //Hente ut tilpasset ruter info 
        public async Task<List<Rute>> HentRute(InfoMedRute info)
        {
            try
            {
                List<Rute> alleRuter = await _db.ruter.ToListAsync();

                //Finn tilpasset rute som har sammen dato og stedene ved bruk av LINQ
                var finnRute = (from passRute in alleRuter
                                where passRute.Dato == info.dato && passRute.FraRute == info.fSted && passRute.TilRute == info.tSted
                                select passRute).ToList();

                return finnRute;
            }
            catch
            {
                return null;
            }
        }
    }
}