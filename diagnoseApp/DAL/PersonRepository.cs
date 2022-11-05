using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Serilog;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace diagnoseApp.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private PersonDB _db;

        private ILogger<PersonRepository> _log;

        public PersonRepository(PersonDB db, ILogger<PersonRepository> log)
        {
            _db = db;
            _log = log;
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

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Brukere funnetBruker = await _db.brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }


    }
}
