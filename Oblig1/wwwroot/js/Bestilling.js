$(function () {
    henteAlleBillett();
});

function henteAlleBillett() {
    $.get("NorWay/HentAlle", function (bestillinger) {
        formaterBestillinger(bestillinger);
    });
}

function formaterBestillinger(bestillinger) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Epost</th><th>Billettype</th><th>Pris</th><th>Fra</th><th>Til</th>" +
        "<th>Dato</th><th>BussNr</th><th>Avgangstid</th><th>Ankomsttid</th>" +
        "</tr>";

    for (let bestilling of bestillinger) {
        ut += "<tr>" +
            "<td>" + bestilling.epost + "</td>" +
            "<td>" + bestilling.billettype + "</td>" +
            "<td>" + bestilling.pris + "</td>" +
            "<td>" + bestilling.fraSted + "</td>" +
            "<td>" + bestilling.tilSted + "</td>" +
            "<td>" + bestilling.avgangersDato + "</td>" +
            "<td>" + bestilling.bussNr + "</td>" +
            "<td>" + bestilling.avgangstid + "</td>" +
            "<td>" + bestilling.ankomsttid + "</td>" +
            "</tr>";

        if ((bestilling.returDato) != null) {
            formaterReturnBestillinger(bestillinger);
        } 
    }
    ut += "</table>";
    $("#BillettInfo").html(ut);
}

function formaterReturnBestillinger(bestillinger) {

    let utReturn = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Dato</th><th>BussNr</th><th>Avgangstid</th><th>Ankomsttid</th>" +
        "</tr>";

    for (let bestilling of bestillinger) {
        utReturn += "<tr>" +
            "<td>" + bestilling.returDato + "</td>" +
            "<td>" + bestilling.bussNrR + "</td>" +
            "<td>" + bestilling.avgangstidR + "</td>" +
            "<td>" + bestilling.ankomsttidR + "</td>" +
            "</tr>";
    }
    utReturn += "</table>";
    $("#ReturnBillettInfo").html(utReturn);
}