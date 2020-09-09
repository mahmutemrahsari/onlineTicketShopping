using Hallo.Modell;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hallo.Kontroller
{
    [Route("[controller]/[action]")]

    public class BillettKontroller : ControllerBase
    {
        private readonly BillettDB _billettDB;

        public BillettKontroller(BillettDB billettDB)
        {
            _billettDB = billettDB;
        }

        public List<Billett> HentAlle()
        {
            List<Billett> alleBillett = _billettDB.Billetter.ToList();
            return alleBillett;
        }


        public bool lagre(Billett inn)
        {
            try
            {
                _billettDB.Billetter.Add(inn);
                _billettDB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool slette(int id)
        {
            try
            {
                Billett billetten = _billettDB.Billetter.Find(id);
                _billettDB.Billetter.Remove(billetten);
                _billettDB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Billett hentEn(int id)
        {
            try
            {
                Billett billetten = _billettDB.Billetter.Find(id);

                return billetten;
            }
            catch
            {
                return null;
            }

        }


        public bool endre(Billett endring)
        {
            try
            {
                Billett billetten = _billettDB.Billetter.Find(endring.id);
                billetten.sted = endring.sted;
                billetten.dato = endring.dato;
                billetten.type = endring.type;

                _billettDB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


    }
}
