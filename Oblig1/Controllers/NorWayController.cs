using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oblig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig.Controllers
{
    [Route("[controller]/[action]")]
    public class NorWayController : ControllerBase
    {
        private readonly BillettContext _db;

        public NorWayController(BillettContext db)
        {
            _db = db;
        }

        //[HttpPost]
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
                    Billettype = BestilleBillett.Billettype
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
                        Billettype = bestill.Billettype
                    };
                    alleBilletter.Add(enBestilling);
                }
            }
            return alleBilletter;
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Kunde enDbKunde = await _db.kunder.FindAsync(id);
                _db.kunder.Remove(enDbKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Kunde> HentEn(int id)
        {
            Kunde enKunde = await _db.kunder.FindAsync(id);
            var hentetKunde = new Kunde()
            {
                Id = enKunde.Id,
                Epost = enKunde.Epost
                
            };
            return hentetKunde;
        }

        /*public async Task<bool> Endre (NorWay endreBillett)
        {
            try
            {
                var endreObjekt = await _db.kunder.FindAsync(endreBillett.Id);
                if(endreObjekt.Billett.ReferanseID != endreBillett.ReferanseID)
                {
                    var sjekkReferanse = _db.Billetter.Find(endreBillett.ReferanseID);
                    if(sjekkReferanse == null)
                    {
                        var referanseRad= new Billett();
                        referanseRad.ReferanseID = endreBillett.ReferanseID;
                        referanseRad.AvgangersDato = endreBillett.AvgangersDato;
                        referanseRad.ReturDato = endreBillett.ReturDato;
                        referanseRad.FraSted = endreBillett.FraSted;
                        referanseRad.TilSted = endreBillett.TilSted;
                        referanseRad.Pris = endreBillett.Pris;
                        referanseRad.Billettype = endreBillett.Billettype;
                        endreObjekt.Billett = referanseRad;

                    }
                    else
                    {
                        endreObjekt.Billett.ReferanseID = endreBillett.ReferanseID;
                    }
                }
                endreObjekt.Epost = endreBillett.Epost;
                endreObjekt.Telefonnr = endreBillett.Telefonnr;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }*/
    }
}
