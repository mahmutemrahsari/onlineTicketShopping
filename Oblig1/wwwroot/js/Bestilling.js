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
        "<th>Epost</th><th>Telefonnr</th><th>Billettype</th><th>Pris</th><th>Frasted</th>" +
        "<th>AvgangersDato</th><th>TilSted</th><th>ReturDato</th><th>Antall</th>" +
        "</tr>";
    for (let bestilling of bestillinger) {
        ut += "<tr>" +
            "<td>" + bestilling.epost + "</td>" +
            "<td>" + bestilling.telefonnr + "</td>" +
            "<td>" + bestilling.billettype + "</td>" +
            "<td>" + bestilling.pris + "</td>" +
            "<td>" + bestilling.fraSted + "</td>" +
            "<td>" + bestilling.avgangersDato + "</td>" +
            "<td>" + bestilling.tilSted + "</td>" +
            "<td>" + bestilling.returDato + "</td>" +
            "<td>" + bestilling.antall + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#BillettInfo").html(ut);
}