﻿$(function () {
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
    const dato = $("#date1").val();
    const fSted = $("#avgang").val();
    const tSted = $("#destinasjon").val();
    const url = "NorWay/HentRute?dato=" + dato;
    /*
    $.ajax({
        type: 'POST',
        url: 'NorWay/HentStop',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ dato: dato, fSted: fSted, tSted: tSted }),
        dataType: 'json',
        success: function (rutes) {
            formaterRute(rutes);
        },
        error: 'console.log("feil")'
    });*/
    
    $.get(url, function (rutes) {
        formaterRute(rutes);
    });
}

function formaterRute(rutes) {
    let utHeading = "";
    for (let rute of rutes) {
        utHeading += "<span>" + rute.fraRute + "-->" + rute.tilRute + "<span>";
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