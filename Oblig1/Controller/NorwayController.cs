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

namespace Oblig.Controllers
{
    [Route("[controller]/[action]")]
    public class NorWayController : ControllerBase
    {
        private readonly INorwayReposatory _db;

        private ILogger<NorWayController> _log;

        public NorWayController(INorwayReposatory db, ILogger<NorWayController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(NorWay BestilleBillett)
        {

            bool returOK = await _db.Lagre(BestilleBillett);
            if (!returOK)
            {
                _log.LogInformation("Billettet ble ikke bestilt");
                return BadRequest("Billettet ble ikke bestilt");
            }
            return Ok("Kunde lagret");
        }

        public async Task<ActionResult> HentAlle()
        {
            List<NorWay> hentAlle = await _db.HentAlle();
            return Ok(hentAlle);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentStop()
        {
            List<Sted> alleSteder = await _db.HentStop();
            return Ok(alleSteder);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentPrisType()
        {
            List<PrisType> alleprisType = await _db.HentPrisType();
            return Ok(alleprisType);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentRute(InfoMedRute info)
        {
            List<Rute> alleRuter = await _db.HentRute(info);
            return Ok(alleRuter);// returnerer alltid OK, null ved tom DB

        }
    }
}