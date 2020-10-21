using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Oblig.Controllers;
using Oblig.Models;
using Oblig1.DAL;
using Oblig1.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class NorwayTest
    {

        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<INorwayReposatory> mockRep = new Mock<INorwayReposatory>();
        private readonly Mock<ILogger<NorWayController>> mockLog = new Mock<ILogger<NorWayController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task HentAlleLoggetInnOk()
        {
            //Arrange
            var kunde1 = new NorWay
            {
                Epost = "noe1@gmail.no",
                Billettype = "Voksen",
                Pris = 123,
                FraSted = "Oslo",
                AvgangersDato = "2020 - 10 - 05",
                TilSted = "Sandvika",
                ReturDato = "2020 - 10 - 20",
                Antall = 3,
                Avgangstid = "14:00",
                Ankomsttid = "17:00",
                BussNr = "230",
                AvgangstidR = "15:00",
                AnkomsttidR = "19:25",
                BussNrR = "120"
            };

            var kunde2 = new NorWay
            {
                Epost = "noe2@gmail.no",
                Billettype = "Barn",
                Pris = 200,
                FraSted = "Sandvika",
                AvgangersDato = "2020 - 10 - 20",
                TilSted = "Sandvika",
                ReturDato = "2020 - 10 - 05",
                Antall = 5,
                Avgangstid = "15:00",
                Ankomsttid = "19:25",
                BussNr = "230",
                AvgangstidR = "14:00",
                AnkomsttidR = "17:00",
                BussNrR = "130"
            };

            var kundeListe = new List<NorWay>();
            kundeListe.Add(kunde1);
            kundeListe.Add(kunde2);

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(kundeListe);

            var norwayController1 = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController1.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await norwayController1.HentAlle() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<NorWay>>((List<NorWay>)resultat.Value, kundeListe);
        }

        [Fact]
        public async Task HentAlleLoggetInnOKFeilDB()
        {
            var kundeListe = new List<NorWay>();

            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(() => null);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await norwayController.HentAlle() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }


        [Fact]
        public async Task LagreLoggetInnOK()
        {

            // Arrange

            mockRep.Setup(k => k.Lagre(It.IsAny<NorWay>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.Lagre(It.IsAny<NorWay>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde lagret", resultat.Value);
        }


        [Fact]
        public async Task LoggInnOK()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Admin>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LoggInn(It.IsAny<Admin>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Admin>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LoggInn(It.IsAny<Admin>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }


        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Admin>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LoggInn(It.IsAny<Admin>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            norwayController.LoggUt();

            // Assert
            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }


        [Fact]
        public async Task EndreRuteLoggetInnOK()
        {

            // Arrange

            mockRep.Setup(k => k.EndreRute(It.IsAny<RuteInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreRute(It.IsAny<RuteInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute endret", resultat.Value);
        }

        [Fact]
        public async Task EndreRuteLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndreRute(It.IsAny<RuteInn>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreRute(It.IsAny<RuteInn>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av ruten kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreRuteLoggetInnFeilModel()
        {     
            // Arrange
            mockRep.Setup(k => k.EndreRute(It.IsAny<RuteInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            //norwayController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreRute(It.IsAny<RuteInn>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreRuteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreRute(It.IsAny<RuteInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreRute(It.IsAny<RuteInn>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test EndreStop() 

        [Fact]
        public async Task EndreStopLoggetInnOK()
        {

            // Arrange
            mockRep.Setup(k => k.EndreStop(It.IsAny<StedInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreStop(It.IsAny<StedInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Steden endret", resultat.Value);
        }

        [Fact]
        public async Task EndreStopLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndreStop(It.IsAny<StedInn>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreStop(It.IsAny<StedInn>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av steden kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreStopLoggetInnFeilModel()
        {
            var sted1 = new StedInn
            {
                SId = 1,
                StedNavn = "",
            };
            // Arrange
            mockRep.Setup(k => k.EndreStop(sted1)).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("StedNavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreStop(sted1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreStopIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndreStop(It.IsAny<StedInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreStop(It.IsAny<StedInn>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test EndrePris() 

        [Fact]
        public async Task EndrePrisLoggetInnOK()
        {

            // Arrange
            mockRep.Setup(k => k.EndrePris(It.IsAny<PrisInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndrePris(It.IsAny<PrisInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Prisen endret", resultat.Value);
        }

        [Fact]
        public async Task EndrePrisLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndrePris(It.IsAny<PrisInn>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndrePris(It.IsAny<PrisInn>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endringen av prisen kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndrePrisLoggetInnFeilModel()
        {
            var pris1 = new PrisInn
            {
                TId = 1,
                Pris = 0,
                Type = ""
            };
            // Arrange

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("Pris", "Feil i inputvalidering på server");
            norwayController.ModelState.AddModelError("Type", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndrePris(pris1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndrePrisIkkeLoggetInn()
        {
            mockRep.Setup(k => k.EndrePris(It.IsAny<PrisInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndrePris(It.IsAny<PrisInn>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test SlettRute()

        [Fact]
        public async Task SlettRutenLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettRute(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("ruten slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettRuteLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettRute(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av ruten ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SletteRuteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettRute(It.IsAny<int>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettRute(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task LagreStedLoggetInnOK()
        {
            //Arrange

            mockRep.Setup(s => s.LagreSted(It.IsAny<StedInn>())).ReturnsAsync(true);

            var kundeController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.LagreSted(It.IsAny<StedInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgang lagret", resultat.Value);


        }
    }
}