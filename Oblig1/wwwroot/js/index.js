$(function () {
    //hent ut alle stops i db til html
    settStop();
});

function settStop() {
    $.get("NorWay/HentStop", function (stops) {
        formaterStop(stops);
    });
}

function formaterStop(stops) {
    let ut = "";
    for (let stop of stops) {
        ut += "<option>" + stop.stedNavn + "</option>";
    }
    $("#avgang").html(ut);
    $("#destinasjon").html(ut);
}


//hent ut tilpasse rute info
function settRute() {
    
    const info = {
        dato: $("#date1").val(),
        fSted: $("#avgang").val(),
        tSted: $("#destinasjon").val()
    }
    const url = "NorWay/HentRute";
    //const url = "NorWay/HentRute?info=" + info;
    /*
    const dato = $("#date1").val();
    const fSted = $("#avgang").val();
    const tSted = $("#destinasjon").val();
    //console.log(dato, fSted, tSted);
    
    $.ajax({
        type: 'POST',
        url: 'NorWay/HentRute',
        contentType: "application/json; charset=utf-8",
        //data: '{ "dato":' + dato + ', "fSted":' + fSted + '"}',
        data: JSON.stringify({ dato: dato, fSted: fSted, tSted: tSted }),
        dataType: 'json',
        success: function (rutes) {
            formaterRute(rutes);
            console.log("success" + dato + fSted + tSted);
        },
        error: 'console.log("feil")'
    });*/
    
    $.get(url, info, function (rutes) {
        formaterRute(rutes);
    });
}

function formaterRute(rutes) {
    let utHeading = "";
    for (let rute of rutes) {
        utHeading += "<span>" + rute.fraRute + "-->" + rute.tilRute + "<span>";
        break;
    }
    $("#heading").html(utHeading);
}

//lagering bestilling informasjon
function lagreBestilling() {
    const reise = {
        Epost: $("#Epost").val(),
        Pris: $("#Pris").val(),
        Billettype: $("#billettType").val(),
        FraSted: $("#avgang").val(),
        TilSted: $("#destinasjon").val(),
        AvgangersDato: $("#date1").val(),
        ReturDato: $("#date2").val(),
        Antall: $("#antall").val()
    }
    const url = "NorWay/Lagre";
    $.post(url, reise, function () {
        window.location.href = "bestill.html";
    });
};