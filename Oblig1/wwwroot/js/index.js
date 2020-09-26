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
        ut += '<input type="button" name="minus" value="-" id="minus" onclick="antallBillet()" />'
        ut += ' <span id="antall">0</span>'
        ut += ' <input type="button" name="plus" value="+" id="plus" onclick="antallBillet()" />'
        ut += '</br>'
    }
    $("#billettType").html(ut);
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
    let antall = parseInt($("#antall").html());

    $('#plus').click(function () {
        antall += 1;
        $('#antall').html(antall);
    });

    $('#minus').click(function () {
        while (antall > 0) {
            antall -= 1;
        }

        $('#antall').html(antall);

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

