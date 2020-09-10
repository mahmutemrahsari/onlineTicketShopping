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

        public async Task<bool> SettInn(NorWay BestilleBillett)
        {
            try
            {
                var nyKunder = new Kunde();
                nyKunder.Id = BestilleBillett.Id;
                nyKunder.Epost = BestilleBillett.Epost;
                nyKunder.Telefonnr = BestilleBillett.Telefonnr;

                var nyBillett = new Billett();
                nyBillett.ReferanseID = BestilleBillett.ReferanseID;
                nyBillett.AvgangersDato = BestilleBillett.AvgangersDato;
                nyBillett.ReturDato = BestilleBillett.ReturDato;
                nyBillett.FraSted = BestilleBillett.FraSted;
                nyBillett.TilSted = BestilleBillett.TilSted;
                nyBillett.Pris = BestilleBillett.Pris;
                nyBillett.Billettype = BestilleBillett.Billettype;

                _db.kunder.Add(nyKunder);
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
            List<NorWay> alleKunder = await _db.kunder.Select(k => new NorWay
            {
                Id = k.Id,
                Epost = k.Epost,
                Telefonnr = k.Telefonnr,
                ReferanseID=k.Billett.ReferanseID,
                AvgangersDato = k.Billett.AvgangersDato,
                ReturDato = k.Billett.ReturDato,
                FraSted = k.Billett.FraSted,
                TilSted = k.Billett.TilSted,
                Pris = k.Billett.Pris,
                Billettype = k.Billett.Billettype

            }).ToListAsync();
            return alleKunder;
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
                Epost = enKunde.Epost,
                Telefonnr = enKunde.Telefonnr,
                
            };
            return hentetKunde;
        }

        public async Task<bool> Endre (NorWay endreBillett)
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
        }
    }
}
