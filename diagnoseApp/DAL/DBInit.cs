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
            }
        }
    }
}

