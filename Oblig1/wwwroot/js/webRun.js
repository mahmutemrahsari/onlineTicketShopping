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
    $("#endreF").click(function () {
        $("#fra-org").css("display", "none");
        $("#fra-valg").css("display", "block");
    });
    $("#endreT").click(function () {
        $("#til-org").css("display", "none");
        $("#til-valg").css("display", "block");
    });
});



function fraStop() {
    var fra = $("#avgang").val();
    $("#fraUt").html("<span>" + fra + "</span>");
    $("#avgang").val(fra);
    $("#fra-org").css("display", "block");
    $("#fra-valg").css("display", "none");
    $("#til").css("display", "block");
}


function tilStop() {
    var til = $("#destinasjon").val();
    $("#tilUt").html("<span>" + til + "</span>");
    $("#destinasjon").val(til);
    $("#til-org").css("display", "block");
    $("#til-valg").css("display", "none");
}



$(function () {
    var liste = document.getElementById(destinasjon);
    $("#test").autocomplete({
        source: liste
    });
});

function blurSelect() {
    var element = $("destinasjon").find("option:selected").text();
    $("#test").val(element);
}


