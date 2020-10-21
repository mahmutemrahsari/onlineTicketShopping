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
                _db.Kunder.Add(kunde);
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

            List<Kunde> alleKunder = await _db.Kunder.ToListAsync();
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
            List<Sted> alleSteder = await _db.Steder.ToListAsync();
            return alleSteder;
        }

        public async Task<List<PrisType>> HentPrisType()
        {

            List<PrisType> allePrisType = await _db.Pristype.ToListAsync();
            return allePrisType;
        }


        //Hente ut tilpasset ruter info 
        public async Task<List<Rute>> HentTilpasseRute(InfoMedRute info)
        {
            try
            {
                List<Rute> alleRuter = await _db.Ruter.ToListAsync();

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

        //Fra her er metodene til oblig2 funksjoner

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

                //hvis finner ikke den brukernavn
                if (funnetAdmin == null)
                {
                    return false;
                }

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

        public async Task<List<Rute>> HentRute()
        {
            List<Rute> alleRuter = await _db.Ruter.ToListAsync();
            return alleRuter;
        }

        public async Task<Rute> HentEnRute(int rid)
        {
            try
            {
                Rute enRute = await _db.Ruter.FindAsync(rid);

                var hentetRute = new Rute()
                {
                    RId = enRute.RId,
                    BussNR = enRute.BussNR,
                    FraRute = enRute.FraRute,
                    TilRute = enRute.TilRute,
                    Dato = enRute.Dato,
                    AvgangsTid = enRute.AvgangsTid,
                    AnkomstTid = enRute.AnkomstTid
                };
                return enRute;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> EndreRute(RuteInn endreRute)
        {
            try
            {
                var endreObjekt = await _db.Ruter.FindAsync(endreRute.RId);
                endreObjekt.BussNR = endreRute.BussNR;
                endreObjekt.FraRute = endreRute.FraRute;
                endreObjekt.TilRute = endreRute.TilRute;
                endreObjekt.Dato = endreRute.Dato;
                endreObjekt.AvgangsTid = endreRute.AvgangsTid;
                endreObjekt.AnkomstTid = endreRute.AnkomstTid;

                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;

            }
            return true;
        }

        public async Task<bool> SlettRute(int rid)
        {
            try
            {
                Rute ruten = await _db.Ruter.FindAsync(rid);
                _db.Ruter.Remove(ruten);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> LagreRute(RuteInn innRute)
        {
            try
            {
                var nyRute = new Rute()
                {
                    BussNR = innRute.BussNR,
                    FraRute = innRute.FraRute,
                    TilRute = innRute.TilRute,
                    Dato = innRute.Dato,
                    AvgangsTid = innRute.AvgangsTid,
                    AnkomstTid = innRute.AnkomstTid
                };

                _db.Ruter.Add(nyRute);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EndreStop(StedInn endreSted)
        {
            try
            {
                var endreObjekt = await _db.Steder.FindAsync(endreSted.SId);
                endreObjekt.StedNavn = endreSted.StedNavn;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;

            }
            return true;
        }
        public async Task<bool> SlettSted(int sid)
        {
            try
            {
                Sted steden = await _db.Steder.FindAsync(sid);
                _db.Steder.Remove(steden);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> LagreSted(StedInn innSted)
        {
            try
            {
                var sjekkSted = await _db.Steder.FirstOrDefaultAsync(s => s.StedNavn == innSted.StedNavn);
                if (sjekkSted == null)
                {
                    var nySted = new Sted { StedNavn = innSted.StedNavn };

                    _db.Steder.Add(nySted);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EndrePris(PrisInn endrePris)
        {
            try
            {
                var endreObjekt = await _db.Pristype.FindAsync(endrePris.TId);
                endreObjekt.Pris = endrePris.Pris;
                endreObjekt.Type = endrePris.Type;
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;

            }
            return true;
        }

        public async Task<bool> SlettPris(int tid)
        {
            try
            {
                PrisType prisen = await _db.Pristype.FindAsync(tid);
                _db.Pristype.Remove(prisen);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<bool> LagrePris(PrisInn innPris)
        {
            try
            {
                var sjekkPrisType = await _db.Pristype.FirstOrDefaultAsync(t => t.Type == innPris.Type);
                if (sjekkPrisType == null)
                {
                    var nyPris = new PrisType()
                    {
                        Type = innPris.Type,
                        Pris = innPris.Pris
                    };

                    _db.Pristype.Add(nyPris);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}