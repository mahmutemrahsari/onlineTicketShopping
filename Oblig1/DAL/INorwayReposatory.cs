using Oblig.Models;
using Oblig1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig1.DAL
{
    public interface INorwayReposatory
    {

        Task<bool> Lagre(NorWay BestilleBillett);

        Task<List<NorWay>> HentAlle();

        Task<List<Sted>> HentStop();

        Task<List<PrisType>> HentPrisType();

        Task<List<Rute>> HentRute(InfoMedRute info);
        Task<bool> LoggInn(Admin admin);
    }
}