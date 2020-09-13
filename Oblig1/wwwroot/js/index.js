$(function (){
    henteAlleBillett();
});

$(function henteAlleBillett() {
    $.get("NorWay/HentAlle", function (bestillinger) {
        formaterBestillinger(bestillinger);
    });
});

function formaterBestillinger(bestillinger) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Epost</th><th>Telefonnr</th><th>Billettype</th> <th>Pris</th><th>Frasted</th>" +
        "<th>AvgangersDato</th><th>TilSted</th><th>ReturDato</th>" +
        "</tr>";
    for (let bestilling of bestillinger) {
        ut += "<tr>" +
            "<td>" + bestilling.Epost+ "</td>" +
            "<td>" + bestilling.Telefonnr + "</td>" +
            "<td>" + bestilling.Billettype + "</td>" +
            "<td>" + bestilling.Pris + "</td>" +
            "<td>" + bestilling.FraSted + "</td>" +
            "<td>" + bestilling.AvgangersDato + "</td>" +
            "<td>" + bestilling.TilSted+ "</td>" +
            "<td>" + bestilling.ReturDato + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#bestillinger").html(ut);
}