//Her finnes alle skrivEndret of endre metoder
/*
function skrivEndretSted(sid, sted) {
    $("#endreSted").css("display", "block");

    $("#sNr").html(sid);
    $("#sted").val(sted);
    $("#sid").val(sid);
}

function endreSted() {
    const sted = {
        sId: $("#sid").val(),
        stedNavn: $("#sted").val()
    };
    $.post("NorWay/EndreStop", sted, function () {
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}

function skrivEndretPris(tid, type, pris) {
    $("#endrePris").css("display", "block");

    $("#pris").val(pris);
    $("#type").val(type);
    $("#tid").val(tid);
}

function endrePris() {
    const pris = {
        tId: $("#tid").val(),
        type: $("#type").val(),
        pris: $("#pris").val()
    };
    $.post("NorWay/EndrePris", pris, function () {
        window.location.href = 'admin.html';
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}*/