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
    var stop = $(".input-stop").val();
    $("#fraUt").html("<span>" + stop + "</span>");
    $(".input-stop").val("");
    $("#fra-org").css("display", "block");
    $("#fra-valg").css("display", "none");
}

function tilStop() {
    var stop = $(".input-stop").val();
    $("#tilUt").html("<span>" + stop + "</span>");
    $(".input-stop").val("");
    $("#til-org").css("display", "block");
    $("#til-valg").css("display", "none");
}