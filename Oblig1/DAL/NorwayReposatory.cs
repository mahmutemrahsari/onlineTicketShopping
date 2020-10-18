using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Oblig.Models;
using Oblig1.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Oblig1.DAL
{
    public class NorwayReposatory : INorwayReposatory
    {
        private readonly BillettContext _db;
        private ILogger<NorwayReposatory> _log;

        public NorwayReposatory(BillettContext db, ILogger<NorwayReposatory> log)
        {
            _db = db;
            _log = log;
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

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(password: passord,
                                        salt: salt,
                                        prf: KeyDerivationPrf.HMACSHA512,
                                        iterationCount: 1000,
                                        numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;

        }

        public async Task<bool> LoggInn(Admin admin)
        {
            try
            {
                Adminere funnetAdmin = await _db.Adminere.FirstOrDefaultAsync(a => a.Brukernavn == admin.Brukernavn);

                byte[] hash = LagHash(admin.Passord, funnetAdmin.Salt);
                bool OK = hash.SequenceEqual(funnetAdmin.Passord);
                if (OK)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}