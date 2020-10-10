function validerBrukernavn(brukernavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,10}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet Feil, det må består av 2 til 20 bokstaver");
        return false;
    } else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function validerPassord(passord) {
    const regexp = /^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{6,}$/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet Feil, det må består av minst 6 tegn(en bokstav og et tall)");
        return false;
    } else {
        $("#feilPassord").html("");
        return true;
    }
}