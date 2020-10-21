//regex/inputvalidering for loggInn 
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

//regex/inputvalidering for index.html, som sjekker hva kunder skrives inn
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


//regex/inputvalidering for admin.html, som sjekker hva adminer skrives inn
function validerAdminSted(destinasjon,id) {
    const regxp = /^[a-zA-ZæøåÆØÅ\.\ \-]{4,20}$/;
    const ok = regxp.test(destinasjon);
    const utId = id;
    if (!ok) {
        $(utId).html("Stedet du reiser fra er skrevet feil, må bestå av 4 til 20 bokstaver");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}

function validerAdminPris(destinasjon, id) {
    const regxp = /^[0-9]{2,3}$/;
    const ok = regxp.test(destinasjon);
    const utId = id;
    if (!ok) {
        $(utId).html("Stedet du reiser fra er skrevet feil, må bestå av 4 til 20 bokstaver");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}

