$(function () {
    //Layout
    $("#bekreft").click(function () {
        window.location.href = "bestill.html";
    });

    $("#returnCheck").click(function () {
        if ($("#returnCheck").is(":checked")) {
            $("#returnBox").css("display", "block");
        }
        else {
            $("#returnBox").css("display", "none");
        }
    });

    $("#betal-btn").click(function () {
        $("#betal").toggle();
    });

    $("#visTid").click(function () {
        $("#infoBox").toggle();
    });

});



function fraStop() {
    var fra = $("#avgang").val();

    if (fra == $("#destinasjon").val()) {
        alert("Du må velge forskjell fra stede og til sted");
        $("#avgang").val("");
        $("#destinasjon").val("");
        return;
    } else {
        $("#fraUt").html("<span>" + fra + "</span>");
        $("#avgang").val(fra);
    }
}


    






function tilStop() {
    var til = $("#destinasjon").val();

    //Feilhåntering - bruker kan ikke velg sammen stop for avgang og destinasjon
    if (til == $("#avgang").val()) {
        alert("Du må velge forskjell fra stede og til sted");
        $("#avgang").val("");
        $("#destinasjon").val("");
        return;
    } else {
        $("#tilUt").html("<span>" + til + "</span>");
        $("#destinasjon").val(til);
    }
}






function test() {

    fra = $("#avgang").val()
    til = $("#destinasjon").val()
    datoF = $("#date1").val()

    //Feilhåntering - går ikke videre hvis det er tom input
    if (fra == "" || til == "" || datoF == "") {
        alert("Du må fyller ut alle informasjon!!");
        return;
    } else {
        //Går til Rute.js og hente ut busser informasjon
        settRute()
        $("#ruteBox").css("display", "block");
    }

}



