﻿$(function () {
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
/*for (var i in id) {
    ut_2 += '<input type="button" name="minus" value="-" id="minus" onclick="antallBillet()" />'
    ut_2 += ' <span id=' + '"' + id[i] + '"' + '>0</span>'
    ut_2 += ' <input type="button" name="plus" value="+" id="plus" onclick="antallBillet()" />'
    ut_2 += '</br>'
    //var id = ["antall1", "antall2", "antall3", "antall4", "antall5","antall6","antall7"];
}*/
}


//hent ut tilpasse rute info
function settRute() {
    const dato = $("#date1").val();
    //const fra = $("#destinasjon").val();
    console.log(dato);
    const url = "NorWay/HentRute?dato=" + dato;
    //const url = "NorWay/HentRute?" + dato + fra + til;
    $.get(url, function (rutes) {
        formaterRute(rutes);
    });
}

function formaterRute(rutes) {
    let utHeading = "";
    for (let rute of rutes) {
        utHeading += "<span>" + rute.fraRute + "-->" + rute.tilRute + "<span>";
    }
    $("#heading").html(utHeading);
}

//lagering bestilling informasjon
function lagreBestilling() {
    var id = ["antall1", "antall2", "antall3", "antall4", "antall5", "antall6", "antall7"];
    for (var i in id) {

    }
    const reise = {
        Epost: $("#Epost").val(),
        Pris: $("#Pris").val(),
        Billettype: $("#billettType").val(),
        FraSted: $("#avgang").val(),
        TilSted: $("#destinasjon").val(),
        AvgangersDato: $("#date1").val(),
        ReturDato: $("#date2").val(),
        //Antall: $("#antall").val()
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
};

