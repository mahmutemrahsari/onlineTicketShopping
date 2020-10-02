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


    //endre valges
    $("#endreT").click(function () {
        $("#destinasjon").val("");
        $("#til-org").css("display", "none");
        $("#til-valg").css("display", "block");
    });
});



function fraStop() {
    var fra = $("#avgang").val();

    if (fra == $("#destinasjon").val()) {
        alert("Du må velge forskjell fra stede og til sted");
        $("#avgang").val("");
        $("#destinasjon").val("");
    } else {
        $("#fraUt").html("<span>" + fra + "</span>");
        $("#avgang").val(fra);
        //$("#til-org").css("display", "block");
        //$("#til-valg").css("display", "none");
        
    }
}

function tilStop() {
    var til = $("#destinasjon").val();

    //Feilhåntering - bruker kan ikke velg sammen stop for avgang og destinasjon
    if (til == $("#avgang").val()) {
        alert("Du må velge forskjell fra stede og til sted");
        $("#avgang").val("");
        $("#destinasjon").val("");
    } else {
        $("#tilUt").html("<span>" + til + "</span>");
        $("#destinasjon").val(til);
    }
}

function test() {
    fra = $("#avgang").val();
    til = $("#destinasjon").val();
    dato = $("#date1").val();
    type = $("#antallType").val();
    if (fra == null || til == null || dato == null || type == null) {
        alert("REEEE");
        return;
    }
    //Går til Rute.js og hente ut busser informasjon
    settRute()
    /*
    fra = $("#avgang").val();
    til = $("#destinasjon").val();
    if (fra == null || til == null) {
        alert("REEEE");
    }*/
}



