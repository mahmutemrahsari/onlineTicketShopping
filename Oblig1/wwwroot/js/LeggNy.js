function lagreSted() {
    const sted = { stedNavn: $("#nysted").val() };

    $.post("NorWay/LagreSted", sted, function () {
        alert("Stasjonen er lagt inn");
        window.location.href = 'admin.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feilLagreSted").html("Feil - Avgangen kunne ikke lagres, prøv igjen senere eller med andre input");
        }
    });
}

function lagrePris() {
    const pris = {
        type: $("#nytype").val(),
        pris: $("#nypris").val()
    };

    $.post("NorWay/LagrePris", pris, function () {
        alert("Prisen er lagt inn");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilLagrePris").html("Feil - BillettType kunne ikke lagres, prøv igjen senere eller med andre input");
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
        alert("Ruten er lagt inn");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilLagreRute").html("Feil på server - prøv igjen senere");
            }
        });
}