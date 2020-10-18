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

        Task<List<Rute>> HentTilpasseRute(InfoMedRute info);

        Task<bool> LoggInn(Admin admin);

        Task<List<Rute>> HentRute();

        Task<Rute> HentEnRute(int rid);

        Task<bool> EndreRute(Rute endreRute);

        Task<bool> SlettRute(int rid);

        Task<bool> LagreRute(Rute innRute);

        Task<bool> EndreStop(Sted endreSted);

        Task<bool> SlettSted(int sid);

        Task<bool> LagreSted(Sted innSted);

        Task<bool> EndrePris(PrisType endrePris);

        Task<bool> SlettPris(int tid);

        Task<bool> LagrePris(PrisType innPris);
    }
}