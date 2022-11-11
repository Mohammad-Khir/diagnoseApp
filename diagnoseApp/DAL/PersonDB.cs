using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using diagnoseApp.Model;
using System.Diagnostics.CodeAnalysis;

namespace diagnoseApp.DAL
{
    [ExcludeFromCodeCoverage]
    public class Personene
     {
         public int id { get; set; }
         public string fornavn { get; set; }
         public string etternavn { get; set; }
         public string fodselsnr { get; set; }
         public string adresse { get; set; }
         public string tlf { get; set; }
         public string epost { get; set; }
         public byte[] passord { get; set; }
         public byte[] salt { get; set; }

    }


    [ExcludeFromCodeCoverage]
    public class Brukere
     {
         public int Id { get; set; }
         public string Brukernavn { get; set; }
         public byte[] Passord { get; set; }
         public byte[] Salt { get; set; }
     }

    [ExcludeFromCodeCoverage]
    public class PersonDB : DbContext
    {
        public PersonDB(DbContextOptions<PersonDB> options) : base(options)
        {
            // denne brukes for å opprette databasen fysisk dersom den ikke er opprettet
            // dette er uavhenig av initiering av databasen (seeding)
            // når man endrer på strukturen på PersonContxt her er det fornuftlig å slette denne fysisk før nye kjøringer
            Database.EnsureCreated();
        }

        //public DbSet<Person> personer { get; set; }
        public DbSet<Personene> personene { get; set; }
        public DbSet<Test> tester { get; set; }

        public DbSet<Brukere> brukere { get; set; }

        //public virtual DbSet<Person> personer { get; set; }
        //public virtual DbSet<Test> tester { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere pakken Microsoft.EntityFrameworkCore.Proxies
            // og legge til"viritual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }*/
    }
}
