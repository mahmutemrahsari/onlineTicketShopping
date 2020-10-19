
$(function () {
    //skjekk hvis man er logget inn før siden viser
    $.get("Norway/Sjekk", function () {
    })
    .fail(function (feil) {
         if (feil.status == 401) {
             window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
         }
    });
});

function loggUt() {
    $.get("Norway/LoggUt", function () {
        window.location.href = "index.html";
    });
}

function endling(id) {
    $("#form-group-new").css("display", "none");
    $("#form-group-change").css("display", "block");
    //skjekk hvilken endre/slett button i table ble klikket
    //alle hent() metoden finnes i endreOgSlett.js
    if (id == "avgangES") {
        hentStop();
        $("#endrePris").css("display", "none");
        $("#endreRute").css("display", "none");
    }
    else if (id == "prisES") {
        hentPris();
        $("#endreSted").css("display", "none");
        $("#endreRute").css("display", "none");
    }
    else if (id == "ruteES") {
        hentRute();
        $("#endrePris").css("display", "none");
        $("#endreSted").css("display", "none");
    } else {
        $("#showInfo").css("display", "none");
    }
};

function leggNy(id) {
    $("#form-group-change").css("display", "none");
    $("#form-group-new").css("display", "block");

    if (id == "avgangNy") {
        $("#nySted").css("display", "block");

        $("#nyPris").css("display", "none");
        $("#nyRute").css("display", "none");
    }
    else if (id == "prisNy") {
        $("#nyPris").css("display", "block");

        $("#nySted").css("display", "none");
        $("#nyRute").css("display", "none");
    }
    else if (id == "ruteNy") {
        $("#nyRute").css("display", "block");

        $("#nyPris").css("display", "none");
        $("#nySted").css("display", "none");
    } 
}




