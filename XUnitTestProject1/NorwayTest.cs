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

        private const string _loggetInn = "Logget inn";
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
        public async Task Endrerute()
        {

            // Arrange

            mockRep.Setup(k => k.EndreRute(It.IsAny<Rute>())).ReturnsAsync(true);

            var kundeController = new NorWayController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.EndreRute(It.IsAny<Rute>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde endret", resultat.Value);
        }

        [Fact]
        public async Task SlettRuten()
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
            Assert.Equal("Sletting av Kunden ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task Lagre()
        {
            //Arrange
            var innKunde = new NorWay
            {                    
                Epost = "noe@gmail.no",
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

            var mock = new Mock<INorwayReposatory>();
            mock.Setup(n => n.Lagre(innKunde)).ReturnsAsync(true);

            var mock1 = new Mock<ILogger<NorWayController>>();
            var norwayController = new NorWayController(mock.Object,mock1.Object);


            //Act            
            var resultat = await norwayController.Lagre(innKunde) as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.OK,resultat.StatusCode);

           
        }
    }
}
