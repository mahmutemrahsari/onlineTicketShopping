﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<bool> SettInn(Billett BestilleBilett)
        {
             try
            {
               
            }
        }

    }
}
