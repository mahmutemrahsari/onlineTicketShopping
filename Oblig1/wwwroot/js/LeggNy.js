﻿function lagreSted() {
    const sted = { stedNavn: $("#nysted").val() };

    $.post("NorWay/LagreSted", sted, function () {
        window.location.href = 'loggInn.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil - Avgangen kunne ikke lagres");
            }
        });
}

function lagrePris() {
    const pris = {
        type: $("#nytype").val(),
        pris: $("#nypris").val()
    };

    $.post("NorWay/LagrePris", pris, function () {
        window.location.href = 'loggInn.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil - BillettType kunne ikke lagres");
            }
        });
}

function lagreRute() {
    const rute = {
        bussNR: $("#nybussNr").val(),
        fraRute: $("#nyfSted").val(),
        tilRute: $("#nytSted").val(),
        dato: $("#nydato").val(),
        avgangsTid: $("#nyavTid").val(),
        ankomstTid: $("#nyanTid").val()
    };

    $.post("NorWay/LagreRute", rute, function () {
        window.location.href = 'loggInn.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}