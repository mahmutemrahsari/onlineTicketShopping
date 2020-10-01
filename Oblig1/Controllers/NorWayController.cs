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
using Microsoft.AspNetCore.Mvc;

namespace Oblig.Controllers
{
    [Route("[controller]/[action]")]
    public class NorWayController : ControllerBase
    {
        private readonly INorwayReposatory _db;

        public NorWayController(INorwayReposatory db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(NorWay BestilleBillett)
        {
            return await _db.Lagre(BestilleBillett);
        }

        public async Task<List<NorWay>> HentAlle()
        {
            return await _db.HentAlle();
        }

        public async Task<List<Sted>> HentStop()
        {
            return await _db.HentStop();
        }

        public async Task<List<PrisType>> HentPrisType()
        {
            return await _db.HentPrisType();
        }

        public async Task<List<Rute>> HentRute(InfoMedRute info)
        {
            return await _db.HentRute(info);
        }
    }
}
