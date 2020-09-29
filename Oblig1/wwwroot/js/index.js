$(function () {
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
        formaterPris(pris);
    });
}

function formaterPris(pris) {
    let ut = "";
    for (let priser of pris) {
        ut += "<span>" + priser.type + "</span>"
        ut += '<br>'
    }
    $("#Plass_1").html(ut);
}


//hent ut tilpasse buss infomasjoner
function settRute() {
    const info = {
        dato: $("#date1").val(),
        fSted: $("#avgang").val(),
        tSted: $("#destinasjon").val()
    }

    const url = "NorWay/HentRute";

    let utHeading = "<span>" + info.fSted + "-->" + info.tSted + "<span>" + "<br>" +
        "<span>" + info.dato + "<span>";
    $("#heading").html(utHeading);
    
    $.get(url, info, function (rutes) {
        formaterRute(rutes);
    });
}

//Viser tilgjengelig busser informasjon til HTML, hvis det finnes noe.
function formaterRute(rutes) {
    let utMain = "<thead class='thead-dark'>" + "<tr>" +
        "<th>BussNR</th><th>Avgangstid</th><th>Ankomsttid</th><th>Velge</th>" +
        "</tr>" + "</thead>" + "<tbody>";
    for (let rute of rutes) {
        utMain += "<tr>" +
            "<td>" + rute.bussNR + "</td>" +
            "<td>" + rute.avgangsTid + "</td>" +
            "<td>" + rute.ankomstTid + "</td>" +
            "<td><input type='checkbox' calss='checkRute'/></td>" +
            "</tr>";
    }

    utMain += "</tbody>";
    $("#ruteTB").html(utMain);
}

$(function () {
    //når bruker kliker en rad i tbody
    $('#ruteTB').on('click', 'tbody tr', function () {
        //alert($(this).html());
        $("#ruteTB tr").removeClass("highlight");
        $(this).toggleClass("highlight");

        //hvis det finnes ingen checkbox er checked(ingen bussen er velgte)
        //så sett checkBox i denne raden til checked
        if ($("td :checkbox:checked").length == 0) {
            $(this).closest('tr').find('input').prop('checked', true);
        } else {
            $("td :checkbox:checked").prop("checked", false);
            $(this).closest('tr').find('input').prop('checked', true);
        }

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

    const url = "NorWay/HentRute";

    let utHeading = "<span>" + info.fSted + "-->" + info.tSted + "<span>" + "<br>" +
        "<span>" + info.dato + "<span>";
    $("#headingRE").html(utHeading);

    $.get(url, info, function (rutes) {
        formaterRuteReturn(rutes);
    });
}

//Viser tilgjengelig busser informasjon til HTML, hvis det finnes noe.
function formaterRuteReturn(rutes) {
    let utMain = "<thead class='thead-dark'>" + "<tr>" +
        "<th>BussNR</th><th>Avgangstid</th><th>Ankomsttid</th><th>Velge</th>" +
        "</tr>" + "</thead>" + "<tbody>";
    for (let rute of rutes) {
        utMain += "<tr class='none'>" +
            "<td>" + rute.bussNR + "</td>" +
            "<td>" + rute.avgangsTid + "</td>" +
            "<td>" + rute.ankomstTid + "</td>" +
            "<td><input type='checkbox' calss='checkRute'/></td>" +
            "</tr>";
    }

    utMain += "</tbody>";
    $("#ruteReturnTB").html(utMain);
}

$(function () {
    $('#ruteReturnTB').on('click', 'tbody tr', function () {
        $("#ruteReturnTB tr").removeClass("highlightTB");
        $(this).toggleClass("highlightTB");

        //hvis det finnes ingen checkbox er checked(ingen bussen er velgte)
        //så sett checkBox i denne raden til checked
        if ($("td :checkbox:checked").length == 0) {
            $(this).closest('tr').find('input').prop('checked', true);
        } else {
            $("td :checkbox:checked").prop("checked", false);
            $(this).closest('tr').find('input').prop('checked', true);
        }
    });
});


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

//Antall billet kan endres + og - buttonner
function antallBillet() {
    let antall = parseInt($("#antall1").html());

    $('#plus1').click(function () {
        antall += 1;
        $('#antall1').html(antall);
    });

    $('#minus1').click(function () {
        if (antall > 0) {
            antall -= 1;
        }
        $('#antall1').html(antall);
    });
};


//Sett BillettType
function typeBillett() {
    $("#billett").on("click", function (e) {
        $("#BillettType").show();

        $(document).one("click", function () {
            $("#BillettType").hide();
        });

        e.stopPropagation();
    });
    $("#BillettType").on("click", function (e) {
        e.stopPropagation();
    });
}

function antallBillet2() {
    let antall = parseInt($("#antall2").html());

    $('#plus2').click(function () {
        antall += 1;
        $('#antall2').html(antall);
    });

    $('#minus2').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall2').html(antall);

    });
};
function antallBillet3() {
    let antall = parseInt($("#antall3").html());

    $('#plus3').click(function () {
        antall += 1;
        $('#antall3').html(antall);
    });

    $('#minus3').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall3').html(antall);

    });
};
function antallBillet4() {
    let antall = parseInt($("#antall4").html());

    $('#plus4').click(function () {
        antall += 1;
        $('#antall4').html(antall);
    });

    $('#minus4').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall4').html(antall);

    });
};

function antallBillet5() {
    let antall = parseInt($("#antall5").html());

    $('#plus5').click(function () {
        antall += 1;
        $('#antall5').html(antall);
    });

    $('#minus5').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall5').html(antall);

    });
};

function antallBillet6() {
    let antall = parseInt($("#antall6").html());

    $('#plus6').click(function () {
        antall += 1;
        $('#antall6').html(antall);
    });

    $('#minus6').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall6').html(antall);

    });
};

function antallBillet7() {
    let antall = parseInt($("#antall7").html());

    $('#plus7').click(function () {
        antall += 1;
        $('#antall7').html(antall);
    });

    $('#minus7').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall7').html(antall);

    });
}

