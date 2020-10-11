$(function () {
    //hent ut alle stops og pris i db til html
    settPris();
    liste();
});

function liste() {
    $.get("NorWay/HentStop", function (stops) {
        formaterListe(stops);
    });
}

function formaterListe(stops) {
    let ut = "";
    for (let stop of stops) {
        ut += "<option value='" + stop.stedNavn + "'>";
    }
    $("#liste").html(ut);
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
        ut += "<span" + " " + "class=" + '"' + "pris" + '"' + ">" + priser.pris + "</span>"
        ut += "<br>"
    }
    $("#Pris").html(ut);
}


//lagering bestilling informasjon
function lagreBestilling() {
    hentTypeOgAntall();
    antall();

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
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
};
