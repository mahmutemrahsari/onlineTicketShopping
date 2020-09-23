﻿using Microsoft.AspNetCore.Mvc;
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
                        //Pris = bestill.Pris,
                        Billettype = bestill.Billettype
                    };
                    alleBilletter.Add(enBestilling);
                }
            }
            return alleBilletter;
        }

        
        //Hente ut til og fra stedene
        public async Task<List<Sted>> HentStop()
        {
            List<Sted> alleSteder = await _db.steder.ToListAsync();
            return alleSteder;
        }

        public async Task<List<PrisType>> HentPrisType(string type)
        {
            try
            {
                List<PrisType> allePris = await _db.pristype.ToListAsync();
                List<PrisType> typePris = new List<PrisType>();
                foreach (var pris in allePris)
                {
                    var enprisType = new PrisType()
                    {
                        type = pris.type
                    };
                    typePris.Add(enprisType);
                }
                return typePris;
            }
            catch
            {
                return null;
            }
            
        }


        //Hente ut tilpasset ruter info 
        public async Task<List<Rute>> HentRute(string dato)
        {
            try
            {
                List<Rute> alleRuter = await _db.ruter.ToListAsync();
                List<Rute> passerRuter = new List<Rute>();
                //Finn tilpasset rute som har sammen dato 
                foreach (var rute in alleRuter)
                {
                    Rute finnRute = await _db.ruter.FindAsync(dato);
                    var enRute = new Rute()
                    {
                        FraRute = finnRute.FraRute,
                        TilRute = finnRute.TilRute,
                        Dato = finnRute.Dato,
                        Time = finnRute.Time
                    };
                    passerRuter.Add(enRute);
                }
                return passerRuter;
            }
            catch
            {
                return null;
            }
        }
    }
}
