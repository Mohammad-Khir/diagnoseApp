using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using diagnoseApp.DAL;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using Bruker = diagnoseApp.Model.Bruker;

namespace diagnoseApp.Controllers
{

    [Route("[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private IPersonRepository _db;

        private ILogger<PersonController> _log;

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        public PersonController(IPersonRepository db, ILogger<PersonController> log)
        {
            _db = db;
            _log = log;
        }
        public async Task<ActionResult> Lagre(Person innPerson)
        {
            /*if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }*/
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Lagre(innPerson);
                if (!returOK)
                {
                    _log.LogInformation("Personen kunne ikke lagres!");
                    return BadRequest("Personen kunne ikke lagres");
                }
                return Ok("Personen er lagret");
            }
            _log.LogInformation("Inputvaliderings feil");
            return BadRequest("Inputvaliderings feil");

        }

        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            List<Person> allePersoner = await _db.HentAlle();
            return Ok(allePersoner); // returnerer alltid OK, null ved tom DB
        }

        public async Task<ActionResult> Slett(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Personen kunne ikke slettes!");
                return NotFound("Personen kunne ikke slettes!");
            }
            return Ok("Personen er slettet");
        }

        public async Task<ActionResult> HentEn(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            Person personen = await _db.HentEn(id);
            if (personen == null)
            {
                _log.LogInformation("Personen ikke funnet!");
                return NotFound("Personen ikke funnet!");
            }
            return Ok(personen);
        }

        public async Task<ActionResult> Endre(Person endrePerson)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(endrePerson);
                if (!returOK)
                {
                    _log.LogInformation("Endringen av personen kunne ikke utføres");
                    return NotFound("Endringen av personen kunne ikke utføres");
                }
                return Ok("Personen er endret");
            }
            _log.LogInformation("Inputvaliderings feil");
            return BadRequest("Inputvaliderings feil");
        }
        public async Task<int> LagreTest(Test innTest)
        {
            return await _db.LagreTest(innTest);
            /*int testid = await _db.LagreTest(innTest);
            if (testid == 0)
            {
                _log.LogInformation("Testen kunne ikke lagres!");
                return BadRequest("Testen kunne ikke lagres");
            }
            return Ok("Testen er lagret");*/
        }
        public async Task<ActionResult> HentEnTest(Test test)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            Result result = await _db.HentEnTest(test);
            if (result == null)
            {
                _log.LogInformation("Resultatet ikke funnet!");
                return NotFound("Resultatet ikke funnet!");
            }
            return Ok(result);
        }
        public async Task<ActionResult> HentAlleTester()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Test> alleTester = await _db.HentAlleTester();
            return Ok(alleTester);
        }

        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, _loggetInn);
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
        }

        public async Task<ActionResult> LoggInnPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInnPerson(person);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }


    }
}

