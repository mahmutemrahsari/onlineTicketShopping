$(function () {
    //hent ut alle stops i db til html
    settStop();
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


//hent ut tilpasse rute info
function settRute() {
    const info = {
        dato: $("#date1").val(),
        fSted: $("#avgang").val(),
        tSted: $("#destinasjon").val()
    }

    const url = "NorWay/HentRute";

    let utHeading = "<span>" + info.fSted + "-->" + info.tSted + "<span>" + "<br>" +
        "<span>" + info.dato + "<span>";
    $("#returnRute heading").html(utHeading);
    
    $.get(url, info, function (rutes) {
        formaterRute(rutes);
    });
}

function formaterRute(rutes) {
    let utMain = "<table class='table table-striped' id='ruteTB'>" +
        "<tr>" +
        "<th>BussNR</th><th>Avgangstid</th><th>Ankomsttid</th>" +
        "</tr>";
    for (let rute of rutes) {
        utMain += "<tr>" +
            "<td>" + rute.bussNR + "</td>" +
            "<td>" + rute.avgangsTid + "</td>" +
            "<td>" + rute.ankomstTid + "</td>" +
            "</tr>";
    }

    utMain += "</table>";
    $("#avganger").html(utMain);
}

//lagering bestilling informasjon
function lagreBestilling() {
    const reise = {
        Epost: $("#Epost").val(),
        Pris: $("#Pris").val(),
        Billettype: $("#billettType").val(),
        FraSted: $("#avgang").val(),
        TilSted: $("#destinasjon").val(),
        AvgangersDato: $("#date1").val(),
        ReturDato: $("#date2").val(),
        Antall: $("#antall").val()
    }
    const url = "NorWay/Lagre";
    $.post(url, reise, function () {
        window.location.href = "bestill.html";
    });
};