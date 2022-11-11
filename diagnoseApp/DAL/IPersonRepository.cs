using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace diagnoseApp.DAL
{
    public interface IPersonRepository
    {
        Task<bool> Lagre(Person innPerson);
        Task<List<Person>> HentAlle();
        Task<bool> Slett(int id);
        Task<Person> HentEn(int id);
        Task<bool> Endre(Person endrePerson);
        Task<int> LagreTest(Test innTest);
        Task<Result> HentEnTest(Test test);
        Task<List<Test>> HentAlleTester();
        Task<bool> LoggInn(Bruker bruker);
        Task<bool> LoggInnPerson(Person person);

    }
}
