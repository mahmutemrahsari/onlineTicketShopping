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

function validerBilletInfo() {
    const fraStedOK = validerFraSted($("#avgang").val());
    const tilStedOK = validerTilSted($("#destinasjon").val());
    const kortnummerOK = valideKortNummer($("#kortNr").val());
    const cvvOK = validercvv($("#cvv").val());
    const epostOK = validerepost($("#Epost").val());
    if (fraStedOK && tilStedOK && kortnummerOK && cvvOK && epostOK) {
        lagreBestilling();
    } else {
        $("#feil").html("Betaling er feil, skjekk alle input er riktig");
    }
}

//lagering bestilling informasjon
function lagreBestilling() {
    hentTypeOgAntall();
    an();

    if ($("#antallTicket").val() == 0) {
        alert("Du må valge antall billett");
        return;
    }

    if ($("#ruteTB tr").hasClass("highlight")) {
        var bussNr = $(".highlight").find("td").eq(0).text();
        var avgangstid = $(".highlight").find("td").eq(1).text();
        var ankomsttid = $(".highlight").find("td").eq(2).text();
        var dato = $("#date1").val();
    } else {
        alert("Du må velge en buss ! ");
        return;
    }

   

    if ($("#returnCheck").is(':checked')) {
        if ($("#ruteReturnTB tr").hasClass("returnHighlight")) {
            var bussNrR = $(".returnHighlight").find("td").eq(0).text();
            var avgangstidR = $(".returnHighlight").find("td").eq(1).text();
            var ankomsttidR = $(".returnHighlight").find("td").eq(2).text();
            var returnDato = $("#date2").val();
        } else {
            alert("Du må velge en return buss ! ");
            return;
        }
    } else {
        var bussNrR = "NO";
        var avgangstidR = "NO";
        var ankomsttidR = "NO";
        var returnDato = "NO";
    }
    
    const reise = {
        Epost: $("#Epost").val(),
        Pris: $("#TotalPris").val(),
        Billettype: $("#antallType").val(),
        FraSted: $("#avgang").val(),
        TilSted: $("#destinasjon").val(),
        AvgangersDato: dato,
        ReturDato: returnDato,
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









