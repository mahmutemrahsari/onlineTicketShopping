$(function () {
    //kan ikke velge datoer tilbake i tid
    var dtToday = new Date();
    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();

    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + month + '-' + day;

    $('#date1').attr('min', maxDate);
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


function visBetalBox() {
    /*
    hentTypeOgAntall();

    if ($("#antallTicket").val() == 0) {
        alert("Du må valge antall billett");
        return;
    } else if (!($("#ruteTB tr").hasClass("highlight"))) {
        alert("Du må velge en buss ! ");
        return;
    } else if($("#returnCheck").is(':checked')) {
        if (!($("#ruteReturnTB tr").hasClass("returnHighlight"))) {
            alert("Du må velge en return buss ! ");
            return;
        }
    }*/
    $("#Betaling").css("display", "block");
    
}


