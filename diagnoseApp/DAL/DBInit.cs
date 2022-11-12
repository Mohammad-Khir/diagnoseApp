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

                
                // lag en påoggings Person2
                var person = new Personene();
                person.fornavn = "Omar";
                person.etternavn = "Sory";
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



                // lag en påoggings Admin
                var bruker = new Brukere();
                bruker.Brukernavn = "Jeg";
                string Passord = "Jeg5";
                byte[] Salt = PersonRepository.LagSalt();
                byte[] Hash = PersonRepository.LagHash(Passord, Salt);
                bruker.Passord = Hash;
                bruker.Salt = Salt;
                db.brukere.Add(bruker);

                db.SaveChanges();
            }
        }
    }
}

