using diagnoseApp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using static System.Net.Mime.MediaTypeNames;


namespace diagnoseApp.DAL
{
    public class DBInit
    {
        public static void init(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<PersonDB>();
                var db = serviceScope.ServiceProvider.GetService<PersonDB>();

                //Her slettes og opprette databasen hver gang når den skal initieres.
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                /*var nyPerson = new Person
                {
                    fornavn = "Ali",
                    etternavn = "Alsaid",
                    fodselsnr = "05.12.2018",
                    adresse = "Osloveien7",
                    tlf = "12345678",
                    epost = "ali@gmail.com"
                };

                var test1 = new Test
                {
                    dato = "23.05.2019",
                    resultat = "påvist Covid19"
                };
                var test2 = new Test
                {
                    dato = "23.10.2022",
                    resultat = "Ikke påvist Covid19"
                };

                var nyeTester = new List<Test>();
                nyeTester.Add(test1);
                nyeTester.Add(test2);
                nyPerson.tester = nyeTester;

                context.personer.Add(nyPerson);
                context.SaveChanges();*/

                /*var person1 = new Personene
                {
                    fornavn = "Ali",
                    etternavn = "Alsaid",
                    fodselsnr = "05129122018",
                    adresse = "Osloveien7",
                    tlf = "12345678",
                    epost = "ali@gmail.com,"
                    //passord = "ali"

                };
                var test1 = new Test
                {
                    dato = "23.05.2019",
                    resultat = "påvist Covid19",
                    personid = 1
                };
                var test2 = new Test
                {
                    dato = "23.10.2022",
                    resultat = "Ikke påvist Covid19",
                    personid = 1
                };*/
                /*var person2 = new Person
                {
                    fornavn = "Basem",
                    etternavn = "Alsaid",
                    fodselsnr = "05122018098",
                    adresse = "Lillestrøm7",
                    tlf = "12345678",
                    epost = "basem@gmail.com"
                };
                var test3 = new Test
                {
                    dato = "23.05.2019",
                    resultat = "påvist Covid19",
                    personid = 2
                };
                var test4 = new Test
                {
                    dato = "23.10.2022",
                    resultat = "Ikke påvist Covid19",
                    personid = 2
                };*/

                //context.personene.Add(person1);
                //context.tester.Add(test1);
                //context.tester.Add(test2);
                //context.personer.Add(person2);
                //context.tester.Add(test3);
                //context.tester.Add(test4);
                //context.SaveChanges();
                var person = new Personene();
                person.fornavn = "Omar";
                person.etternavn = "M";
                person.fodselsnr = "12345678900";
                person.adresse = "Asker";
                person.tlf = "12345678";
                person.epost = "Omar";
                string passord = "Omar5";
                byte[] salt = PersonRepository.LagSalt();
                byte[] hash = PersonRepository.LagHash(passord, salt);
                person.passord = hash;
                person.salt = salt;
                db.personene.Add(person);

                db.SaveChanges();


                // lag en påoggingsbruker
                var bruker = new Brukere();
                bruker.Brukernavn = "Jeg";
                string Passord = "Jeg5";
                byte[] Salt = PersonRepository.LagSalt();
                byte[] Hash = PersonRepository.LagHash(Passord, Salt);
                bruker.Passord = Hash;
                bruker.Salt = Salt;
                db.brukere.Add(bruker);

                db.SaveChanges();

                // lag en påoggingsPerson
                /*var person = new Personene();
                person.epost = "ali";
                string Passord = "ali";
                byte[] saltPerson = PersonRepository.LagSalt();
                byte[] hashPerson = PersonRepository.LagHash(Passord, saltPerson);
                person.passord = hashPerson;
                person.salt = saltPerson;
                db.personene.Add(person);

                db.SaveChanges();*/
            }
        }
    }
}

