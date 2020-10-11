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
using Microsoft.AspNetCore.Http;

namespace Oblig.Controllers
{
    [Route("[controller]/[action]")]
    public class NorWayController : ControllerBase
    {
        private readonly INorwayReposatory _db;

        private ILogger<NorWayController> _log;

        private const string _loggetInn = "loggetInn";

        public NorWayController(INorwayReposatory db, ILogger<NorWayController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(NorWay BestilleBillett)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            List<NorWay> hentAlle = await _db.HentAlle();
            return Ok(hentAlle);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentStop()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            List<Sted> alleSteder = await _db.HentStop();
            return Ok(alleSteder);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentPrisType()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            List<PrisType> alleprisType = await _db.HentPrisType();
            return Ok(alleprisType);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentRute(InfoMedRute info)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            List<Rute> alleRuter = await _db.HentRute(info);
            return Ok(alleRuter);// returnerer alltid OK, null ved tom DB

        }

        public async Task<ActionResult> LoggInn(Admin admin)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(admin);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet som administrator"+admin.Brukernavn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }
    }
}