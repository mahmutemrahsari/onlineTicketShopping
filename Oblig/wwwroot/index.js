$(function () {
    $.get("(fyll inn med navn på klassen i models)/HentAlle", function (bestillinger) {
        formaterBestillinger(bestillinger);
    });
});

function formaterBestillinger(bestillinger) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Navn</th><th>Telefonnr</th>Strekninger<th></th><th>Avganger</th>" +
        "<th>Priser</th>" +
        "</tr>";
    for (let bestilling of bestillinger) {
        ut += "<tr>" +
            "<td>" + bestilling.navn + "</td>" +
            "<td>" + bestilling.telefonnr + "</td>" +
            "<td>" + bestilling.strekning + "</td>" +
            "<td>" + bestilling.avganger + "</td>" +
            "<td>" + bestilling.priser + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#bestillinger").html(ut);
}