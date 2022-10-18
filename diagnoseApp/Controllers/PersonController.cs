using System;
using System.Collections.Generic;
using System.Linq;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace diagnoseApp.Controllers
{

    [Route("[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonDB _db;

        public PersonController(PersonDB db)
        {
            _db = db;
        }

        public bool Lagre(Person innPerson)
        {
            try
            {
                _db.personer.Add(innPerson);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<Person> HentAlle()
        {
            try
            {
                List<Person> allePersonene = _db.personer.ToList(); // hent hele tabellen
                return allePersonene;
            }
            catch
            {
                return null;
            }
        }

        public bool Slett(int id)
        {
            try
            {
                Person enPerson = _db.personer.Find(id);
                _db.personer.Remove(enPerson);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Person HentEn(int id)
        {
            try
            {
                Person enPerson = _db.personer.Find(id);
                return enPerson;
            }
            catch
            {
                return null;
            }
        }

        public bool Endre(Person endrePerson)
        {
            try
            {
                Person enPerson = _db.personer.Find(endrePerson.id);
                enPerson.fornavn = endrePerson.fornavn;
                enPerson.etternavn = endrePerson.etternavn;
                enPerson.fodselsnr = endrePerson.fodselsnr;
                enPerson.adresse = endrePerson.adresse;
                enPerson.tlf = endrePerson.tlf;
                enPerson.epost = endrePerson.epost;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*public bool slettAlle()
        {
            try
            {
                List<Person> allePersonene = _db.personer.ToList();
                _db.personer.Remove(allePersonene);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }*/

        public bool sjekk()
        {
            return false;
        }
    }
}
