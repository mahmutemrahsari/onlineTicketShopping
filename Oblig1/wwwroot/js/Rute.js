//hent ut tilpasse buss infomasjoner
function settRute() {
    const info = {
        dato: $("#date1").val(),
        fSted: $("#avgang").val(),
        tSted: $("#destinasjon").val()
    }

    const url = "NorWay/HentTilpasseRute";

    let utHeading = "<span>" + info.fSted + "-->" + info.tSted + "<span>" + "<br>" +
        "<span>" + info.dato + "<span>";
    $("#heading").html(utHeading);

    $.get(url, info, function (rutes) {
        formaterRute(rutes);
    });
}

//Viser tilgjengelig busser informasjon til HTML, hvis det finnes noe.
function formaterRute(rutes) {
    let utMain;
    if (rutes.length != 0) {
        utMain = "<thead class='thead-dark'>" + "<tr>" +
            "<th>BussNR</th><th>Avgangstid</th><th>Ankomsttid</th>" +
            "</tr>" + "</thead>" + "<tbody>";


        for (let rute of rutes) {
            utMain += "<tr>" +
                "<td>" + rute.bussNR + "</td>" +
                "<td>" + rute.avgangsTid + "</td>" +
                "<td>" + rute.ankomstTid + "</td>" +
                "</tr>";
        }
    } else {
        utMain = "Ingen avganger tilgjengelig";
    }



    utMain += "</tbody>";
    $("#ruteTB").html(utMain);
}


$(function () {
    //når bruker kliker en rad i tbody
    $('#ruteTB').on('click', 'tbody tr', function () {
        $("#ruteTB tr").removeClass("highlight");
        $(this).toggleClass("highlight");

        //sjekk hvis bruker skal ha return reise (return reise checkbox er checked)
        //hvis det er checked, så viser tilgjengelig return busser informasjon
        if ($("#returnCheck").is(':checked')) {
            settReturnRute();
        }
    });
});



//hent ut tilpasse return buss infomasjoner
function settReturnRute() {
    const info = {
        dato: $("#date2").val(),
        tSted: $("#avgang").val(),
        fSted: $("#destinasjon").val()
    }

    const url = "NorWay/HentTilpasseRute";

    let utHeading = "<span>" + info.fSted + " → " + info.tSted + "<span>" + "<br>" +
        "<span>" + info.dato + "<span>";
    $("#headingRE").html(utHeading);

    $.get(url, info, function (rutes) {
        formaterRuteReturn(rutes);
    });
}

//Viser tilgjengelig busser informasjon til HTML, hvis det finnes noe.
function formaterRuteReturn(rutes) {
    let utMain;
    if (rutes.length != 0) {
        utMain = "<thead class='thead-dark'>" + "<tr>" +
            "<th>BussNR</th><th>Avgangstid</th><th>Ankomsttid</th>" +
            "</tr>" + "</thead>" + "<tbody>";
        for (let rute of rutes) {
            utMain += "<tr>" +
                "<td>" + rute.bussNR + "</td>" +
                "<td>" + rute.avgangsTid + "</td>" +
                "<td>" + rute.ankomstTid + "</td>" +
                "</tr>";
        }

    } else {
        utMain = "Ingen avganger tilgjengelig";
    }

    utMain += "</tbody>";
    $("#ruteReturnTB").html(utMain);
}


$(function () {
    $('#ruteReturnTB').on('click', 'tbody tr', function () {
        $("#ruteReturnTB tr").removeClass("returnHighlight");
        $(this).toggleClass("returnHighlight");
    });
});

