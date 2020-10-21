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

$("#Epost").click(function () {
    //hva som skjer når elementet blir klikket 
    const regxp = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    const ok = regxp.test(destinasjon);
    if (!ok) {
        $("#feilepost").html("E-mail ble skrevet in feil");
        return false;
    } else {
        $("#feilEpost").html("");
        return true;
    }
});

$("#kortNummer").click(function () {
    //hva som skjer når elementet blir klikket 
    const regxp = /^ (?: 4[0 - 9]{ 12 } (?: [0 - 9]{ 3 })?| [25][1 - 7][0 - 9]{ 14 }| 6(?: 011 | 5[0 - 9][0 - 9])[0 - 9]{ 12 }| 3[47][0 - 9]{ 13 }| 3(?: 0[0 - 5] | [68][0 - 9])[0 - 9]{ 11 }| (?: 2131 | 1800 | 35\d{ 3 }) \d{ 11 }) $/;
    const ok = regxp.test(destinasjon);
    if (!ok) {
        $("#feilKortnummer").html("Skriv inn konrtnummeret ditt, med 16 tall");
        return false;
    } else {
        $("#feilKortnummer").html("");
        return true;
    }
});

$("#cvvInput").click(function () {
    //hva som skjer når elementet blir klikket 
    const regxp = /^[1-9\.\ \-]{3}/;
    const ok = regxp.test(destinasjon);
    if (!ok) {
        $("#feilcvv").html("Skriv in de tre tallene bak på kortet ditt ved signaturen din");
        return false;
    } else {
        $("#feilcvv").html("");
        return true;
    }
});

$("#måned").click(function () {
    //hva som skjer når elementet blir klikket 
    const regxp = /^[1-12\.\ \-]{2}/;
    const ok = regxp.test(destinasjon);
    if (!ok) {
        $("#feilMåned").html("skriv in måneden kortet ditt går ut");
        return false;
    } else {
        $("#feilMåned").html("");
        return true;
    }
});

$("#år").click(function () {
    //hva som skjer når elementet blir klikket 
    const regxp = /^[1-99\.\ \-]{2}/;
    const ok = regxp.test(destinasjon);
    if (!ok) {
        $("#feilÅr").html("skriv in året kortet ditt går ut");
        return false;
    } else {
        $("#feilÅr").html("");
        return true;
    }
});







