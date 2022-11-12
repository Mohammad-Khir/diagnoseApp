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
using System.Reflection;
using System.Text;

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
                var nyPersonRad = new Personene();
                nyPersonRad.fornavn = innPerson.fornavn;
                nyPersonRad.etternavn = innPerson.etternavn;
                nyPersonRad.fodselsnr = innPerson.fodselsnr;
                nyPersonRad.adresse = innPerson.adresse;
                nyPersonRad.tlf = innPerson.tlf;
                nyPersonRad.epost = innPerson.epost;
                nyPersonRad.passord = LagHash(innPerson.passord, LagSalt());
                nyPersonRad.salt = LagSalt();
                _db.personene.Add(nyPersonRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }


        public async Task<List<Person>> HentAlle()  // hente alle personene som finnes i DB
        {
            try
            {
                List<Person> allePersonene = await _db.personene.Select(p => new Person
                {
                    id = p.id,
                    fornavn = p.fornavn,
                    etternavn = p.etternavn,
                    fodselsnr = p.fodselsnr,
                    adresse = p.adresse,
                    tlf = p.tlf,
                    epost = p.epost
                }).ToListAsync();
                return allePersonene;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> Slett(int id)   //slette en person fra DB ved hjelp av primary key (id)
        {
            try
            {
                Personene enPerson = await _db.personene.FindAsync(id); // finne den ønskede personen
                _db.personene.Remove(enPerson);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<Person> HentEn(int id)    //hente en person fra DB ved hjelp av primary key (id)
        {
            try
            {
                Personene enPerson = await _db.personene.FindAsync(id);
                var hentetPerson = new Person()
                {
                    id = enPerson.id,
                    fornavn = enPerson.fornavn,
                    etternavn = enPerson.etternavn,
                    fodselsnr = enPerson.fodselsnr,
                    adresse = enPerson.adresse,
                    tlf = enPerson.tlf,
                    epost = enPerson.epost
                };
                return hentetPerson;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return null;
            }
        }

        public async Task<bool> Endre(Person endrePerson)   // endre person-info
        {
            try
            {
                var enPerson = await _db.personene.FindAsync(endrePerson.id);
                enPerson.fornavn = endrePerson.fornavn;
                enPerson.etternavn = endrePerson.etternavn;
                enPerson.fodselsnr = endrePerson.fodselsnr;
                enPerson.adresse = endrePerson.adresse;
                enPerson.tlf = endrePerson.tlf;
                enPerson.epost = endrePerson.epost;
                enPerson.passord = LagHash(endrePerson.passord, LagSalt());
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
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

                var enPerson = await _db.personene.FindAsync(test.personid);
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
                    enResult.personid = enPerson.id;
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

        public async Task<bool> LoggInnPerson(Person person)
        {
            try
            {
                Personene funnetPerson = await _db.personene.FirstOrDefaultAsync(p => p.epost == person.epost);
                //byte[] p = funnetPerson.salt;
                byte[] hash = LagHash(person.passord, funnetPerson.salt);
                bool ok = hash.SequenceEqual(funnetPerson.passord);
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
