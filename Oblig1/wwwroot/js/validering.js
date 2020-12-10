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

function validerepost(Epost) {
    const regxp = /\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    const ok = regxp.test(Epost);
    if (!ok) {
        $("#feilepost").html("Eposten din er ikke gyldig!");
        return false;
    } else {
        $("#feilepost").html("");
        return true;
    }
}

function valideKortNummer(kortNummer) {
    const regxp = /^(?:4[0-9]{12}(?:[0-9]{3})?|[25][1-7][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$/;
    const ok = regxp.test(kortNummer);
    if (!ok) {
        $("#feilKortnummer").html("Kortnummeret ditt er ikke gyldig!");
        return false;
    } else {
        $("#feilKortnummer").html("");
        return true;
    }
}


function validercvv(inncvv) {
    const regxp = /^[0-999]{3}$/;
    const ok = regxp.test(inncvv);
    if (!ok) {
        $("#feilcvv").html("Se på de siste tre tallene bak kortet ditt!");
        return false;
    } else {
        $("#feilcvv").html("");
        return true;
    }
}


//regex/inputvalidering for admin.html, som sjekker hva adminer skrives inn
function validerAdminSted(destinasjon, id) {
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

function validerAdminPris(pris, id) {
    const regxp = /^[0-9]+$/;
    const ok = regxp.test(pris);
    const utId = id;
    if (!ok) {
        $(utId).html("Prisen må være sifre");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}

function validerAdminType(type, id) {
    const regxp = /^[a-zA-ZæøåÆØÅ\.\ \-]{4,10}$/;
    const ok = regxp.test(type);
    const utId = id;
    if (!ok) {
        $(utId).html(" Type må være bokstaver");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}

function validerAdminBusNr(BusNr, id) {
    const regxp = /^[0-9]{2,3}$/;
    const ok = regxp.test(BusNr);
    const utId = id;
    if (!ok) {
        $(utId).html("Bussnummer må være 2 eller 3 sifre!");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}


function validerAdminDato(Dato, id) {
    const regxp = /^(19[5-9][0-9]|20[0-4][0-9]|2050)[-/](0?[1-9]|1[0-2])[-/](0?[1-9]|[12][0-9]|3[01])$/;
    const ok = regxp.test(Dato);
    const utId = id;
    var d = new Date(Dato);
    var now = new Date();
    yearNoW = now.getFullYear(); sel = d.getFullYear();
    monthNoW = now.getMonth(); selMonth = d.getMonth();
    dayNoW = now.getDay(); selDay = d.getDay();
    if (sel < yearNoW || selMonth < monthNoW || !ok) {
        $(utId).html("Dato er ikke riktig format!");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}

function validerAdminAvgangstid(Avtid, id) {
    const utId = id;
    if (Avtid == null) {
        $(utId).html("Tiden er tomt");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}

function validerAdminAnkomsttid(Antid, id) {
    const utId = id;
    if (Antid == null) {
        $(utId).html("Tiden er tomt");
        return false;
    } else {
        $(utId).html("");
        return true;
    }
}



