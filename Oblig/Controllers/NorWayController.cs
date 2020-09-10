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
                AvgangersDato = k.Billett.AvgangersDato,
                ReturDato = k.Billett.ReturDato,
                FraSted = k.Billett.FraSted,
                TilSted = k.Billett.TilSted,
                Pris = k.Billett.Pris,
                Billettype = k.Billett.Billettype

            }).ToListAsync();
            return alleKunder;
        }
    }
}
