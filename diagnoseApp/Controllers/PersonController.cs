using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using diagnoseApp.DAL;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace diagnoseApp.Controllers
{

    [Route("[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _db;

        public PersonController(IPersonRepository db)
        {
            _db = db;
        }
        public async Task<bool> Lagre(Person innPerson)
        {
            return await _db.Lagre(innPerson);
        }

        public async Task<List<Person>> HentAlle()
        {
            return await _db.HentAlle();
        }

        public async Task<bool> Slett(int id)
        {
            return await _db.Slett(id);
        }

        public async Task<Person> HentEn(int id)
        {
            return await _db.HentEn(id);
        }

        public async Task<bool> Endre(Person endrePerson)
        {
            return await _db.Endre(endrePerson);
        }
        public async Task<int> LagreTest(Test innTest)
        {
            return await _db.LagreTest(innTest);
        }
        public async Task<Result> HentEnTest(Test test)
        {
            return await _db.HentEnTest(test);
        }
        public async Task<List<Test>> HentAlleTester()
        {
            return await _db.HentAlleTester();
        }

    }
}
