$(function () {
    //hent ut alle stops og pris i db til html
    settPris();
    liste();
    formateAn();
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
            ut += "<span" + " " +"class=" +'"'+"pris"+'"'+ ">" + priser.pris + "</span>"
            ut += "<br>"
    }
    $("#Pris").html(ut);
}


//lagering bestilling informasjon
function lagreBestilling() {
    hentTypeOgAntall();
    an();

    /*
    if ($("#antallTicket").val() == 0) {
        alert("Du må valge antall billett");
        return;
    }*/

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
    })
    .fail(function () {
        $("#feil").html("Feil på server - prøv igjen senere");
    });
};

/*
$(document).ready(function () {
    $(function () {
        var dtToday = new Date();

        var month = dtToday.getMonth() + 1;
        var day = dtToday.getDate();
        var year = dtToday.getFullYear();

        if (month < 10)
            month = '0' + month.toString();
        if (day < 10)
            day = '0' + day.toString();

        var maxDate = year + '-' + month + '-' + day;

        $('#date1').attr('min', maxDate);
    });
})*/







