//Her finnes alle metoder som funker/til etter klikke knapp endre/slett


function hentStop() {
    $.get("NorWay/HentStop", function (stops) {
        formaterStop(stops);
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
}

function formaterStop(stops) {
    let ut = "<h4>Her er alle avganger</h4>" +
        "<table class='table table-striped'>" + "</thead>" + "<tr>" +
        "<th>Avganger</th><th></th><th></th>"
    "</tr>" + "</thead>" + "<tbody>";
    for (let sted of stops) {
        ut += "<tr>" +
            "<td>" + sted.stedNavn + "</td>" +
            "<td> <input type='button' onclick='skrivEndretSted(" + sted.sId + "," + "\"" + sted.stedNavn + "\")' class='btn btn-primary' value='Endre'>" + "</td>" +
            "<td> <input type='button' class='btn btn-danger' value='Slett' onclick='slettSted(" + sted.sId + ")'>" + "</td>" +
            "</tr>";
    }

    ut += "</tbody>" + "</table>";
    $("#showInfo").html(ut);
}

function skrivEndretSted(sid, sted) {
    $("#endreSted").css("display", "block");

    $("#sNr").html(sid);
    $("#sted").val(sted);
    $("#sid").val(sid);

    $(".feilMelding").html("");
}

function validerOgEndreSted() {
    const stedOK = validerFraSted($("#sted").val());
    if (stedOK) {
        endreSted();
    }
}


function endreSted() {
    const sted = {
        sId: $("#sid").val(),
        stedNavn: $("#sted").val()
    };
    $.post("NorWay/EndreStop", sted, function () {
        alert("Stasjonen ble endret");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilSted").html("Feil på server - prøv igjen senere");
            }
        });
}


function slettSted(sid) {
    const url = "NorWay/SlettSted?sid=" + sid;

    $.get(url, function () {
        alert("Stasjonen ble slettet");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilSted").html("Feil på server - prøv igjen senere");
            }
        });
}

function validerOgEndreType() {
    const typeOK = validerAdminType($("#type").val());
    const prisOK = validerAdminPris($("#pris").val());
    if (typeOK && prisOK) {
        endrePris();
    }
}


function hentPris() {
    $.get("NorWay/HentPrisType", function (priser) {
        formaterPris(priser);
    });
}

function formaterPris(priser) {
    let ut = "<h4>Her er alle Pris og Pristype</h4>" +
        "<table class='table table-striped'>" + "</thead>" + "<tr>" +
        "<th>Pristype</th><th>pris</th><th></th><th></th>"
    "</tr>" + "</thead>" + "<tbody>";
    for (let pris of priser) {
        ut += "<tr>" +
            "<td>" + pris.type + "</td>" +
            "<td>" + pris.pris + "</td>" +
            "<td> <input type='button' onclick='skrivEndretPris(" + pris.tId + "," + "\"" + pris.type + "\"" + "," + "\"" + pris.pris + "\")' class='btn btn-primary' value='Endre'>" + "</td>" +
            "<td> <input type='button' class='btn btn-danger' value='Slett' onclick='slettPris(" + pris.tId + ")'>" + "</td>" +
            "</tr>";
    }

    ut += "</tbody>" + "</table>";
    $("#showInfo").html(ut);
}

function skrivEndretPris(tid, type, pris) {
    $("#endrePris").css("display", "block");

    $("#pris").val(pris);
    $("#type").val(type);
    $("#tid").val(tid);

    $(".feilMelding").html("");
}

function endrePris() {
    const pris = {
        tId: $("#tid").val(),
        type: $("#type").val(),
        pris: $("#pris").val()
    };
    $.post("NorWay/EndrePris", pris, function () {
        alert("Prisen ble endret");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilPris").html("Feil på server - prøv igjen senere");
            }
        });
}

function slettPris(tid) {
    const url = "NorWay/SlettPris?tid=" + tid;

    $.get(url, function () {
        alert("Prisen ble slettet");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilPris").html("Feil på server - prøv igjen senere");
            }
        });
}

function hentRute() {
    $.get("NorWay/HentRute", function (ruter) {
        formaterRute(ruter);
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';
            } else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}

function formaterRute(ruter) {
    let ut = "<h4>Her er alle ruter informasjoner</h4>" +
        "<table class='table table-striped'>" + "</thead>" + "<tr>" +
        "<th>BussNR</th><th>Fra</th><th>Til</th><th>Dato</th><th>Avgangstid</th><th>Ankomsttid</th><th></th><th></th>" +
        "</tr>" + "</thead>" + "<tbody>";
    for (let rute of ruter) {
        ut += "<tr>" +
            "<td>" + rute.bussNR + "</td>" +
            "<td>" + rute.fraRute + "</td>" +
            "<td>" + rute.tilRute + "</td>" +
            "<td>" + rute.dato + "</td>" +
            "<td>" + rute.avgangsTid + "</td>" +
            "<td>" + rute.ankomstTid + "</td>" +
            "<td> <input type='button' onclick='skrivEndretRute(" + rute.rId + ")' class='btn btn-primary' value='Endre'>" + "</td>" +
            "<td> <input type='button' class='btn btn-danger' value='Slett' onclick='slettRute(" + rute.rId + ")'>" + "</td>" +
            "</tr>";
    }

    ut += "</tbody>" + "</table>";
    $("#showInfo").html(ut);
}


//Hent ruten fra db og viser ut i html
function skrivEndretRute(rid) {
    const id = rid;
    const url = "NorWay/HentEnRute?rid=" + id;
    $.get(url, id, function (ruter) {
        $("#endreRute").css("display", "block");

        $("#rid").val(ruter.rId);
        $("#bussNr").val(ruter.bussNR);
        $("#fSted").val(ruter.fraRute);
        $("#tSted").val(ruter.tilRute);
        $("#dato").val(ruter.dato);
        $("#avTid").val(ruter.avgangsTid);
        $("#anTid").val(ruter.ankomstTid);
    });

    $(".feilMelding").html("");
}

function validerOgEndreRute() {
    const BussNrOK = validerAdminBusNr($("#bussNr").val());
    const fStedOK = validerFraSted($("#fSted").val());
    const tStedOK = validerTilSted($("#tSted").val());
    const datoOK = validerAdminDato($("#dato").val());
    const avTidOK = validerAdminAvgangstid($("#avTid").val());
    const anTidOK = validerAdminAnkomsttid($("#anTid").val());
    if (BussNrOK && fStedOK && tStedOK && datoOK && avTidOK && anTidOK) {
        endreRute();
    }
}

function endreRute() {
    const rute = {
        rId: $("#rid").val(),
        bussNR: $("#bussNr").val(),
        fraRute: $("#fSted").val(),
        tilRute: $("#tSted").val(),
        dato: $("#dato").val(),
        avgangsTid: $("#avTid").val(),
        ankomstTid: $("#anTid").val()
    };
    $.post("NorWay/EndreRute", rute, function () {
        alert("Ruten ble endret");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilRute").html("Feil på server - prøv igjen senere");
            }
        });
}

function slettRute(rid) {
    const url = "NorWay/SlettRute?rid=" + rid;

    $.get(url, function () {
        alert("Ruten ble sletted");
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                indow.location.href = 'loggInn.html'; // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feilRute").html("Feil på server - prøv igjen senere");
            }
        });
}
