using diagnoseApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace diagnoseApp.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDB _db;

        public PersonRepository(PersonDB db)
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
                List<Person> allePersonene = await _db.personer.ToListAsync(); // hent hele person-tabellen
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
                Person enPerson = await _db.personer.FindAsync(id); // finne den ønskede personen
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

        public async Task<int> LagreTest(Test innTest) // Lagre Test-resultat i DB
        {
            try
            {
                _db.tester.Add(innTest);
                await _db.SaveChangesAsync();

                int testid = innTest.id;
                return testid;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<Result> HentEnTest(Test test) // For å hente test resultat med person-info
        {
            try
            {
                Result enResult = new Result();

                Person enPerson = await _db.personer.FindAsync(test.personid);
                if (enPerson != null)
                {
                    enResult.fornavn = enPerson.fornavn;
                    enResult.etternavn = enPerson.etternavn;
                    enResult.fodselsnr = enPerson.fodselsnr;
                    enResult.adresse = enPerson.adresse;
                    enResult.tlf = enPerson.tlf;
                    enResult.epost = enPerson.epost;
                    enResult.id = enPerson.id;
                }

                Test enTest = await _db.tester.FindAsync(test.id);
                if (enTest != null)
                {
                    enResult.testid = enTest.id;
                    enResult.dato = enTest.dato;
                    enResult.resultat = enTest.resultat;
                }


                return enResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Test>> HentAlleTester() // Hent alle testene som finnes i DB 
        {
            try
            {
                List<Test> alleTester = await _db.tester.ToListAsync();
                return alleTester;
            }
            catch
            {
                return null;
            }
        }


    }
}
