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

function validerFraSted(avgang) {
    const regxp = /^[a-zA-ZæøåÆØÅ\.\ \-]{4,15}$/;
    const ok = regxp.test(avgang);
    if (!ok) {
        $("#feilFraSted").html("Stedet du reiser fra er skrevet feil, må bestå av 4 til 20 bokstaver");
        return false;
    } else {
        $("#feilFraSted").html("");
        return true;
    }
}

function validerTilSted(destinasjon) {
    const regxp = /^[a-zA-ZæøåÆØÅ\.\ \-]{4,20}$/;
    const ok = regxp.test(destinasjon);
    if (!ok) {
        $("#feilTilSted").html("Stedet du reiser fra er skrevet feil, må bestå av 4 til 20 bokstaver");
        return false;
    } else {
        $("#feilTilSted").html("");
        return true;
    }
}

