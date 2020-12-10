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

        //Fra her er test til oblig2 metoder

        //test LoggInn()

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

        //Test Sjekk()
        [Fact]
        public void SjekkLoggInnOK()
        {
            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = norwayController.Sjekk() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Admin er logget inn", resultat.Value);
        }

        [Fact]
        public void SjekkIkkeLoggInn()
        {
            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = norwayController.Sjekk() as UnauthorizedObjectResult;
            // Assert 
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test LoggUt()

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

        //Test EndreRute()

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
            var rute1 = new RuteInn
            {
                RId = 1,
                BussNR = "2",
                FraRute = "Oslo",
                TilRute = "Sandvika",
                Dato = "2020-10-05",
                AvgangsTid = "12:00",
                AnkomstTid = "15:50"
            };

            mockRep.Setup(k => k.EndreRute(rute1)).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("BussNR", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.EndreRute(rute1) as BadRequestObjectResult;

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
            mockRep.Setup(k => k.EndrePris(pris1)).ReturnsAsync(true);
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

        //Test SlettSted()

        [Fact]
        public async Task SlettStedLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettSted(It.IsAny<int>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettSted(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgangen slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettStedLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettSted(It.IsAny<int>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettSted(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av avgangen ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SletteStedIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettSted(It.IsAny<int>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettSted(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test SlettPris()

        [Fact]
        public async Task SlettPrisLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettPris(It.IsAny<int>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettPris(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Prisen slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettPrisLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettPris(It.IsAny<int>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettPris(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av prisen ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettePrisIkkeLoggetInn()
        {
            mockRep.Setup(k => k.SlettPris(It.IsAny<int>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.SlettPris(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test LagreSted()

        [Fact]
        public async Task LagreStedLoggetInnOK()
        {
            //Arrange

            mockRep.Setup(s => s.LagreSted(It.IsAny<StedInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreSted(It.IsAny<StedInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgang lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreStedLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.LagreSted(It.IsAny<StedInn>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreSted(It.IsAny<StedInn>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Avgangen kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreStedLoggetInnFeilModel()
        {
            // Arrange
            var sted1 = new StedInn
            {
                SId = 1,
                StedNavn = ""
            };

            mockRep.Setup(k => k.LagreSted(sted1)).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("StedNavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreSted(sted1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreStedIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagreSted(It.IsAny<StedInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreSted(It.IsAny<StedInn>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test LagreRute()

        [Fact]
        public async Task LagreRuteLoggetInnOK()
        {
            //Arrange

            mockRep.Setup(s => s.LagreRute(It.IsAny<RuteInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreRute(It.IsAny<RuteInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Rute lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.LagreRute(It.IsAny<RuteInn>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreRute(It.IsAny<RuteInn>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Ruten kunne ikke lagres", resultat.Value);
        }

        
        [Fact]
        public async Task LagreRuteLoggetInnFeilModel()
        {
            // Arrange
            // Kunden er indikert feil med tomt fornavn her.
            // det har ikke noe å si, det er introduksjonen med ModelError under som tvinger frem feilen
            // kunnde også her brukt It.IsAny<Kunde>
            var rute1 = new RuteInn
            {
                RId = 1,
                BussNR = "2",
                FraRute = "Oslo",
                TilRute = "Sandvika",
                Dato = "2020-10-05",
                AvgangsTid = "12:00",
                AnkomstTid = "15:50"
            };
            mockRep.Setup(k => k.LagreRute(rute1)).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("BussNR", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreRute(rute1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreRuteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagreRute(It.IsAny<RuteInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagreRute(It.IsAny<RuteInn>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test LagrePris()

        [Fact]
        public async Task LagrePrisLoggetInnOK()
        {
            //Arrange
            mockRep.Setup(s => s.LagrePris(It.IsAny<PrisInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagrePris(It.IsAny<PrisInn>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Pris lagret", resultat.Value);
        }

        [Fact]
        public async Task LagrePrisLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.LagrePris(It.IsAny<PrisInn>())).ReturnsAsync(false);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagrePris(It.IsAny<PrisInn>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Prisen kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagrePrisLoggetInnFeilModel()
        {
            // Arrange
            var pris1 = new PrisInn
            {
                TId = 1,
                Type = "",
                Pris = 0
            };

            mockRep.Setup(k => k.LagrePris(pris1)).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            norwayController.ModelState.AddModelError("Type", "Feil i inputvalidering på server");
            norwayController.ModelState.AddModelError("Pris", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagrePris(pris1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagrePrisIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagrePris(It.IsAny<PrisInn>())).ReturnsAsync(true);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.LagrePris(It.IsAny<PrisInn>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test HentRute()
        [Fact]
        public async Task HentRuteLoggetInnOK()
        {
            // Arrange
            var rute1 = new Rute
            {
                RId = 1,
                BussNR = "230",
                FraRute = "Oslo",
                TilRute = "Sandvika",
                Dato = "2020-10-05",
                AvgangsTid = "12:00",
                AnkomstTid = "15:50"
            };
            var rute2 = new Rute
            {
                RId = 2,
                BussNR = "130",
                FraRute = "Sandvika",
                TilRute = "Oslo",
                Dato = "2020-10-30",
                AvgangsTid = "14:00",
                AnkomstTid = "12:30"
            };
            var rute3 = new Rute
            {
                RId = 3,
                BussNR = "320",
                FraRute = "Lysaker",
                TilRute = "Storting",
                Dato = "2020-11-05",
                AvgangsTid = "09:30",
                AnkomstTid = "12:00"
            };

            var ruteListe = new List<Rute>();
            ruteListe.Add(rute1);
            ruteListe.Add(rute2);
            ruteListe.Add(rute3);

            mockRep.Setup(k => k.HentRute()).ReturnsAsync(ruteListe);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.HentRute() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Rute>>((List<Rute>)resultat.Value, ruteListe);
        }

        [Fact]
        public async Task HentRuteIkkeLoggetInn()
        {
            // Arrange

            mockRep.Setup(k => k.HentRute()).ReturnsAsync(It.IsAny<List<Rute>>());

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.HentRute() as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        //Test HentEnRute() 

        [Fact]
        public async Task HentEnRuteLoggetInnOK()
        {
            // Arrange
            var rute1 = new Rute
            {
                RId = 1,
                BussNR = "230",
                FraRute = "Oslo",
                TilRute = "Sandvika",
                Dato = "2020-10-05",
                AvgangsTid = "12:00",
                AnkomstTid = "15:50"
            };

            mockRep.Setup(k => k.HentEnRute(It.IsAny<int>())).ReturnsAsync(rute1);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.HentEnRute(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Rute>(rute1, (Rute)resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteLoggetInnIkkeOK()
        {
            // Arrange
            mockRep.Setup(k => k.HentEnRute(It.IsAny<int>())).ReturnsAsync(() => null); // merk denne null setting!

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.HentEnRute(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke ruten", resultat.Value);
        }

        [Fact]
        public async Task HentEnRuteIkkeLoggetInn()
        {
            mockRep.Setup(k => k.HentEnRute(It.IsAny<int>())).ReturnsAsync(() => null);

            var norwayController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            norwayController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await norwayController.HentEnRute(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
    }
}