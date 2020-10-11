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
        private ILogger<NorWayController> _Logg;

        public NorWayController(INorwayReposatory db, ILogger<NorWayController> Logg)
        {
            _db = db;

            _Logg = Logg;
        }

        public async Task<ActionResult> Lagre(NorWay BestilleBillett)
        {

            bool returOK = await _db.Lagre(BestilleBillett);
            if (!returOK)
            {
                _Logg.LogInformation("Kunden ble ikke lagret");
                return BadRequest("Kunden ble ikke lagret");
            }
            return Ok("Kunden ble lagret");
        }

        public async Task<ActionResult> HentAlle()
        {

            List<NorWay> kundeInfo = await _db.HentAlle();
            return Ok(kundeInfo);
        }

        public async Task<ActionResult> HentStop()
        {
            var steder = await _db.HentStop();
            if (steder.Count() == 0)
            {
                return NotFound();
            }
            return Ok(steder);
        }

        public async Task<ActionResult> HentPrisType()
        {
            var prisType = await _db.HentPrisType();
            if (prisType.Count() == 0)
            {
                return NotFound();
            }
            return Ok(prisType);
        }

        public async Task<ActionResult> HentRute(InfoMedRute info)
        {

            var Rute = await _db.HentRute(info);
            if (Rute.Count() == 0)
            {
                return NotFound();
            }
            return Ok(Rute);

        }
    }
}