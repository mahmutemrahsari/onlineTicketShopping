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
        private const string _ikkeLoggetInn = "";

        public NorWayController(INorwayReposatory db, ILogger<NorWayController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> Lagre(NorWay BestilleBillett)
        {
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Lagre(BestilleBillett);
                if (!returOK)
                {
                    _log.LogInformation("Billettet ble ikke bestilt");
                    return BadRequest("Billettet ble ikke bestilt");
                }
                return Ok("Kunde lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
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

        public async Task<ActionResult> HentTilpasseRute(InfoMedRute info)
        {
            var Rute = await _db.HentTilpasseRute(info);
            if (Rute.Count() == 0)
            {
                return NotFound();
            }
            return Ok(Rute);
        }

        public async Task<ActionResult> LoggInn(Admin admin)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(admin);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet som administrator" + admin.Brukernavn);
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
            HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
        }

        //sjekk metoden til admin.html, sikker at man kan ikke gå inn til admin siden uten logginn
        public IActionResult Sjekk()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            return Ok(true);
        }

        public async Task<ActionResult> HentRute()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized();
            }
            List<Rute> alleRuter = await _db.HentRute();
            return Ok(alleRuter);// returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> HentEnRute(int rid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            Rute ruten = await _db.HentEnRute(rid);
            if (ruten == null)
            {
                _log.LogInformation("Fant ikke Ruten");
                return NotFound("Fant ikke ruten");
            }
            return Ok(ruten);
        }


        public async Task<ActionResult> EndreRute(RuteInn endreRute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreRute(endreRute);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av ruten kunne ikke utføres");
                }
                return Ok("Rute endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> SlettRute(int rid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettRute(rid);
            if (!returOK)
            {
                _log.LogInformation("Sletting av ruten ble ikke utført");
                return NotFound("Sletting av ruten ble ikke utført");
            }
            return Ok("ruten slettet");
        }

        public async Task<ActionResult> LagreRute(RuteInn innRute)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreRute(innRute);
                if (!returOK)
                {
                    _log.LogInformation("Ruten kunne ikke lagres!");
                    return BadRequest("Ruten kunne ikke lagres");
                }
                return Ok("Rute lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> EndreStop(StedInn endreSted)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndreStop(endreSted);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av steden kunne ikke utføres");
                }
                return Ok("Steden endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> SlettSted(int sid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettSted(sid);
            if (!returOK)
            {
                _log.LogInformation("Sletting av avgangen ble ikke utført");
                return NotFound("Sletting av avgangen ble ikke utført");
            }
            return Ok("Avgangen slettet");
        }

        public async Task<ActionResult> LagreSted(StedInn innSted)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreSted(innSted);
                if (!returOK)
                {
                    _log.LogInformation("Avgangen kunne ikke lagres!");
                    return BadRequest("Avgangen kunne ikke lagres");
                }
                return Ok("Avgang lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> EndrePris(PrisInn endrePris)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.EndrePris(endrePris);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av prisen kunne ikke utføres");
                }
                return Ok("Prisen endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> SlettPris(int tid)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.SlettPris(tid);
            if (!returOK)
            {
                _log.LogInformation("Sletting av prisen ble ikke utført");
                return NotFound("Sletting av prisen ble ikke utført");
            }
            return Ok("Prisen slettet");
        }

        public async Task<ActionResult> LagrePris(PrisInn innPris)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagrePris(innPris);
                if (!returOK)
                {
                    _log.LogInformation("Prisen kunne ikke lagres!");
                    return BadRequest("Prisen kunne ikke lagres");
                }
                return Ok("Pris lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }
    }
}