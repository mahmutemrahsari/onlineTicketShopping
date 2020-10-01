﻿$(function () {
    //hent ut alle stops og pris i db til html
    settStop();
    settPris();
});

function settStop() {
    $.get("NorWay/HentStop", function (stops) {
        formaterStop(stops);
    });
}

function formaterStop(stops) {
    let ut = "";
    for (let stop of stops) {
        ut += "<option>" + stop.stedNavn + "</option>";
    }
    $("#avgang").html(ut);
    $("#destinasjon").html(ut);
}

function settPris() {
    $.get("Norway/HentPrisType", function (pris) {
        formaterPrisType(pris);
        formaterPris(pris);
    });
}

function formaterPrisType(pristype) {
    let ut = "";
    for (let pristyper of pristype) {
        ut += "<span" + " " + "class=" + '"' + "pristype" + '"' + ">" + pristyper.type + "</span>"
        ut += "<br>"
    }
    $("#Plass_1").html(ut);
}

function formaterPris(pris) {
    let ut = "";
    
    for (let priser of pris) {
            ut += "<span" + " " +"class=" +'"'+"pris"+'"'+ ">" + priser.pris + "</span>"
            ut += "<br>"
    }
    $("#Pris").html(ut);
}


//lagering bestilling informasjon
function lagreBestilling() {
    hentTypeOgAntall();
    antall();

    if ($("#ruteTB tr").hasClass("highlight")) {
        var bussNr = $(".highlight").find("td").eq(0).text();
        var avgangstid = $(".highlight").find("td").eq(1).text();
        var ankomsttid = $(".highlight").find("td").eq(2).text();
    } else {
        alert("Du må velge en buss ! ");
        return;
    }

   

    if ($("#returnCheck").is(':checked')) {
        if ($("#ruteReturnTB tr").hasClass("returnHighlight")) {
            var bussNrR = $(".returnHighlight").find("td").eq(0).text();
            var avgangstidR = $(".returnHighlight").find("td").eq(1).text();
            var ankomsttidR = $(".returnHighlight").find("td").eq(2).text();
        } else {
            alert("Du må velge en return buss ! ");
            return;
        }
    }
    
    const reise = {
        Epost: $("#Epost").val(),
        Pris: $("#TotalPris").val(),
        Billettype: $("#antallType").val(),
        FraSted: $("#avgang").val(),
        TilSted: $("#destinasjon").val(),
        AvgangersDato: $("#date1").val(),
        ReturDato: $("#date2").val(),
        Antall: $("#antallTicket").val(),
        BussNr: bussNr,
        Avgangstid: avgangstid,
        Ankomsttid: ankomsttid,
        BussNrR: bussNrR,
        AvgangstidR: avgangstidR,
        AnkomsttidR: ankomsttidR
    }
    const url = "NorWay/Lagre";
    $.post(url, reise, function () {
        window.location.href = "bestill.html";
    });
};




