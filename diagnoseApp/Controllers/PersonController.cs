using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> Lagre(Person innPerson) // metode for å lagre en person-info i DB
        {
            try
            {
                _db.personer.Add(innPerson);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<Person>> HentAlle()  // hente alle personene som finnes i DB
        {
            try
            {
                List<Person> allePersonene = await _db.personer.ToListAsync(); // hent hele tabellen
                return allePersonene;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int id)   //slette en person fra DB ved hjelp av primary key (id)
        {
            try
            {
                Person enPerson = _db.personer.Find(id); // finne den ønskede personen
                _db.personer.Remove(enPerson);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Person> HentEn(int id)    //hente en person fra DB ved hjelp av primary key (id)
        {
            try
            {
                Person enPerson = await _db.personer.FindAsync(id);
                return enPerson;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Endre(Person endrePerson)   // endre person-info
        {
            try
            {
                Person enPerson = await _db.personer.FindAsync(endrePerson.id);
                enPerson.fornavn = endrePerson.fornavn;
                enPerson.etternavn = endrePerson.etternavn;
                enPerson.fodselsnr = endrePerson.fodselsnr;
                enPerson.adresse = endrePerson.adresse;
                enPerson.tlf = endrePerson.tlf;
                enPerson.epost = endrePerson.epost;
                await _db.SaveChangesAsync();
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

       
    }
}
