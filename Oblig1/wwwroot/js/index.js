$(function () {
    //hent ut alle stops i db
    settStop();
});

function settStop() {
    $.get("NorWay/HentStop", function (stops) {
        formaterBestillinger(stops);
    });
}

function formaterBestillinger(bestillinger) {
    //<option value="Sandefjord lufthavn"></option>
    let ut = "";
    for (let stop of stops) {
        ut += "<option>" + stop.avgang + "</option>";
    }
    $("#stops").html(ut);
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