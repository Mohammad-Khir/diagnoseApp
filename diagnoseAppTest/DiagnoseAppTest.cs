using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using diagnoseApp.Controllers;
using diagnoseApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using diagnoseApp.DAL;
using DiagnoseAppTest;


namespace DiagniseAppTest
{
    public class DiagnoseAppTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IPersonRepository> mockRep = new Mock<IPersonRepository>();
        private readonly Mock<ILogger<PersonController>> mockLog = new Mock<ILogger<PersonController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

       
        [Fact]
        public async Task LagreOK()
        {
            //var person1 = new Person {id = 1,fornavn = "Ali",etternavn = "Alsaid",fodselsnr = "1234567890",adresse = "Oslo",tlf = "12345678",epost = "Ali@gmail.com",passord = "Ali91"};
            // Arrange

            mockRep.Setup(p => p.Lagre(It.IsAny<Person>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Lagre(It.IsAny<Person>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Person er lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreIkkeOK()
        {
            // Arrange

            mockRep.Setup(p => p.Lagre(It.IsAny<Person>())).ReturnsAsync(false);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Lagre(It.IsAny<Person>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Person kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreFeilModel()
        {
            // Arrange
            var person1 = new Person
            {
                id = 1,
                fornavn = "",
                etternavn = "Alsaid",
                fodselsnr = "1234567890",
                adresse = "Oslo",
                tlf = "12345678",
                epost = "Ali@gmail.com",
                passord = "Ali91"
            };

            mockRep.Setup(p => p.Lagre(person1)).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            personController.ModelState.AddModelError("fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Lagre(person1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        /*[Fact]
        public async Task LagreIkkeLoggetInn()
        {
            mockRep.Setup(p => p.Lagre(It.IsAny<Person>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Lagre(It.IsAny<Person>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }*/

        [Fact]
        public async Task HentAlleLoggetInnOK()
        {
            // Arrange
            var person1 = new Person
            {
                id = 1,
                fornavn = "Ali",
                etternavn = "Alsaid",
                fodselsnr = "1234567890",
                adresse = "Oslo",
                tlf = "12345678",
                epost = "Ali@gmail.com",
                passord = "Ali91"
            };
            var person2 = new Person
            {
                id = 2,
                fornavn = "Line",
                etternavn = "Olsen",
                fodselsnr = "0987654321",
                adresse = "Lillestøm",
                tlf = "09876543",
                epost = "Line@gmail.com",
                passord = "Line91"
            };
            var person3 = new Person
            {
                id = 3,
                fornavn = "Ole",
                etternavn = "Dal",
                fodselsnr = "0980985321",
                adresse = "Drammen",
                tlf = "92764014",
                epost = "Ole@gmail.com",
                passord = "Ole91"
            };

            var personListe = new List<Person>();
            personListe.Add(person1);
            personListe.Add(person2);
            personListe.Add(person3);

            mockRep.Setup(p => p.HentAlle()).ReturnsAsync(personListe);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentAlle() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Person>>((List<Person>)resultat.Value, personListe);
        }

       [Fact]
       public async Task HentAlleIkkeLoggetInn()
       {
            // Arrange

            //var tomListe = new List<Person>();
            mockRep.Setup(p => p.HentAlle()).ReturnsAsync(It.IsAny<List<Person>>());

           var personController = new PersonController(mockRep.Object, mockLog.Object);

           mockSession[_loggetInn] = _ikkeLoggetInn;
           mockHttpContext.Setup(s => s.Session).Returns(mockSession);
           personController.ControllerContext.HttpContext = mockHttpContext.Object;

           // Act
           var resultat = await personController.HentAlle() as UnauthorizedObjectResult;

           // Assert 
           Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
           Assert.Equal("Ikke logget inn", resultat.Value);
       }

        

        [Fact]
        public async Task SlettLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(p => p.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Slett(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Personen er slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(p => p.Slett(It.IsAny<int>())).ReturnsAsync(false);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Slett(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SletteIkkeLoggetInn()
        {
            mockRep.Setup(p => p.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Slett(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnOK()
        {
            // Arrange
            var person1 = new Person
            {
                id = 1,
                fornavn = "Ali",
                etternavn = "Alsaid",
                fodselsnr = "1234567890",
                adresse = "Oslo",
                tlf = "12345678",
                epost = "Ali@gmail.com",
                passord = "Ali91"
            };

            mockRep.Setup(k => k.HentEn(It.IsAny<int>())).ReturnsAsync(person1);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentEn(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Person>(person1, (Person)resultat.Value);
        }

        [Fact]
        public async Task HentEnLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(p => p.HentEn(It.IsAny<int>())).ReturnsAsync(() => null); // merk denne null setting!

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentEn(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke personen", resultat.Value);
        }

        [Fact]
        public async Task HentEnIkkeLoggetInn()
        {
            mockRep.Setup(p => p.HentEn(It.IsAny<int>())).ReturnsAsync(() => null);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentEn(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(p => p.Endre(It.IsAny<Person>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Endre(It.IsAny<Person>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Personen er endret", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(p => p.Lagre(It.IsAny<Person>())).ReturnsAsync(false);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Endre(It.IsAny<Person>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreLoggetInnFeilModel()
        {
            // Arrange
            // personen er indikert feil med tomt fornavn her.
            // det har ikke noe å si, det er introduksjonen med ModelError under som tvinger frem feilen
            // person også her brukt It.IsAny<Person>
            var person1 = new Person
            {
                id = 1,
                fornavn = "",
                etternavn = "Alsaid",
                fodselsnr = "1234567890",
                adresse = "Oslo",
                tlf = "12345678",
                epost = "Ali@gmail.com",
                passord = "Ali91"
            };

            mockRep.Setup(p => p.Endre(person1)).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            personController.ModelState.AddModelError("fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Endre(person1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            mockRep.Setup(p => p.Endre(It.IsAny<Person>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.Endre(It.IsAny<Person>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnPersonOK()
        {
            mockRep.Setup(p => p.LoggInnPerson(It.IsAny<Person>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LoggInnPerson(It.IsAny<Person>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnPersonFeilPassordEllerEpost()
        {
            mockRep.Setup(p => p.LoggInnPerson(It.IsAny<Person>())).ReturnsAsync(false);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LoggInnPerson(It.IsAny<Person>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnPersonInputFeil()
        {
            mockRep.Setup(p => p.LoggInnPerson(It.IsAny<Person>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            personController.ModelState.AddModelError("Epost", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LoggInnPerson(It.IsAny<Person>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(p => p.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            mockRep.Setup(p => p.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep.Setup(p => p.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            personController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            personController.LoggUt();

            // Assert
            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }



        // Testing til den andre class (Test)


        /*[Fact]
        public async Task LagreTestLoggetInnOK()
        {
            var test1 = new Test
            {
                id = 1,
                dato = "20.05.2022",
                resultat = "Påvist",
                personid = 1
            };

            // Arrange
            mockRep.Setup(t => t.LagreTest(It.IsAny<Test>())).ReturnsAsync(test1.id);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LagreTest(It.IsAny<Test>());

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat);
            Assert.Equal("Testen er lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreTestLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(t => t.LagreTest(It.IsAny<Test>())).ReturnsAsync(0);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.LagreTest(It.IsAny<Test>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Testen kunne ikke lagres", resultat.Value);
        }*/

        [Fact]
        public async Task HentEnTestLoggetInnOK()
        {
            // Arrange
            var result1 = new Result
            {
                id = 1,
                fornavn = "Ali",
                etternavn = "Alsaid",
                fodselsnr = "1234567890",
                adresse = "Oslo",
                tlf = "12345678",
                epost = "Ali@gmail.com",
                testid = 1,
                dato = "20.05.2022",
                resultat = "Påvist",
                personid = 1
            };

            mockRep.Setup(t => t.HentEnTest(It.IsAny<Test>())).ReturnsAsync(result1);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentEnTest(It.IsAny<Test>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Result>(result1, (Result)resultat.Value);
        }

        [Fact]
        public async Task HentEnTestLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(t => t.HentEnTest(It.IsAny<Test>())).ReturnsAsync(() => null);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentEnTest(It.IsAny<Test>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke testens resultat", resultat.Value);
        }

        [Fact]
        public async Task HentEnTestIkkeLoggetInn()
        {
            mockRep.Setup(t => t.HentEnTest(It.IsAny<Test>())).ReturnsAsync(() => null);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentEnTest(It.IsAny<Test>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentAlleTesterLoggetInnOK()
        {
            var test1 = new Test
            {
                id = 1,
                dato = "20.05.2022",
                resultat = "Påvist",
                personid = 1
            };
            var test2 = new Test
            {
                id = 2,
                dato = "07.12.2022",
                resultat = " ikke Påvist",
                personid = 1
            };

            var testListe = new List<Test>();
            testListe.Add(test1);
            testListe.Add(test2);

            mockRep.Setup(t => t.HentAlleTester()).ReturnsAsync(testListe);

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentAlleTester() as OkObjectResult;
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Test>>((List<Test>)resultat.Value, testListe);
        }

        [Fact]
        public async Task HentAlleTesterIkkeLoggetInn()
        {

            mockRep.Setup(t => t.HentAlleTester()).ReturnsAsync(It.IsAny<List<Test>>());

            var personController = new PersonController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            personController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await personController.HentAlleTester() as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

    }
}
