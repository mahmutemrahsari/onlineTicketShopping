using Hallo.Modell;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hallo.Kontroller
{
    [Route("[controller]/[action]")]
    public class KundeKontroller : ControllerBase
    {
        private readonly KundeDB _kundeDB;

        public KundeKontroller(KundeDB kundeDB)
        {
            _kundeDB = kundeDB;
        }

        public List<Kunde> HentAlle()
        {
            List<Kunde> alleKunde = _kundeDB.Kunder.ToList();
            return alleKunde;
        }

        public bool lagre(Kunde inn)
        {
            try
            {
                _kundeDB.Kunder.Add(inn);
                _kundeDB.SaveChanges();
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
                Kunde kunden = _kundeDB.Kunder.Find(id);
                _kundeDB.Kunder.Remove(kunden);
                _kundeDB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Kunde hentEn(int id)
        {
            try
            {
                Kunde kunden = _kundeDB.Kunder.Find(id);

                return kunden;
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
                billetten.fra = endring.fra;
                billetten.til = endring.til;
                billetten.dato = endring.dato;
                billetten.type = endring.type;
                billetten.lengde = endring.lengde;

                _billettDB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        /*public bool endre(Kunde endring)
        {
            try
            {
                Kunde kunden = _kundeDB.Kunder.Find(endring.id);
                kunden.fornavn = endring.fornavn;
                kunden.etternavn = endring.etternavn;
                kunden.alder = endring.alder;
                kunden.telefonnr = endring.telefonnr;
                kunden.epost = endring.epost;
                kunden.adresse = endring.adresse;

                _kundeDB.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        */
    }
}
