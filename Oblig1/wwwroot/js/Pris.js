//Antall billet kan endres + og - buttonner
function antallBillet() {
    let antall = parseInt($("#antall1").html());

    $('#plus1').click(function () {
        antall += 1;
        $('#antall1').html(antall);
    });

    $('#minus1').click(function () {
        if (antall > 0) {
            antall -= 1;
        }
        $('#antall1').html(antall);
    });
};

function antall() {
    //Hente pris fra database
    var p = document.getElementsByClassName("pris");
    var VoksenPris = p[0].innerHTML;
    var BarnPris = p[1].innerHTML;
    var StudentPris = p[2].innerHTML;
    var UngdomPris = p[3].innerHTML;
    var HonnorPris = p[4].innerHTML;
    var VernepliktPris = p[5].innerHTML;
    var LedsagerPris = p[6].innerHTML;

    //regne ut total pris
    var cont1 = document.getElementById("antall1").innerHTML;
    var cont2 = document.getElementById("antall2").innerHTML;
    var cont3 = document.getElementById("antall3").innerHTML;
    var cont4 = document.getElementById("antall4").innerHTML;
    var cont5 = document.getElementById("antall5").innerHTML;
    var cont6 = document.getElementById("antall6").innerHTML;
    var cont7 = document.getElementById("antall7").innerHTML;
    //Feilhåntering
    if (cont1 == 0 && cont2 == 0 && cont3 == 0 && cont4 == 0 && cont5 == 0 && cont6 == 0 && cont7 == 0) {
        alert("Du må valge antall billett")
    } else {
        $("#Voksen").html(VoksenPris * cont1);
        $("#Barn").html(BarnPris * cont2);
        $("#Student").html(StudentPris * cont3);
        $("#Ungdom").html(UngdomPris * cont4);
        $("#Honnor").html(HonnorPris * cont5);
        $("#Verneplikt").html(VernepliktPris * cont6);
        $("#Ledsager").html(LedsagerPris * cont7);
        var cont_1 = document.getElementById("Voksen").innerHTML;
        var cont_2 = document.getElementById("Barn").innerHTML;
        var cont_3 = document.getElementById("Student").innerHTML;
        var cont_4 = document.getElementById("Ungdom").innerHTML;
        var cont_5 = document.getElementById("Honnor").innerHTML;
        var cont_6 = document.getElementById("Verneplikt").innerHTML;
        var cont_7 = document.getElementById("Ledsager").innerHTML;
        var total = Number(cont_1) + Number(cont_2) + Number(cont_3) +
            Number(cont_4) + Number(cont_5) + Number(cont_6) + Number(cont_7);
        $("#TotalPris").val(total)

    }
}

function type() {

}



//Sett BillettType
function typeBillett() {
    $("#billett").on("click", function (e) {
        $("#BillettType").show();

        $(document).one("click", function () {
            $("#BillettType").hide();
        });

        e.stopPropagation();
    });
    $("#BillettType").on("click", function (e) {
        e.stopPropagation();
    });
}

function antallBillet2() {
    let antall = parseInt($("#antall2").html());

    $('#plus2').click(function () {
        antall += 1;
        $('#antall2').html(antall);
        $('#tall').html(antall);
        $('#typeAv').html(" Barn");

    });

    $('#minus2').click(function () {
        if (antall > 0) {
            antall -= 1;
        }
        $('#antall2').html(antall);
        $('#tall').html(antall);
        $('#typeAv').html(" Barn");
        if (antall == 0) {
            $("#tall").html("")
            $('#typeAv').html("Velg type av billett");
        }

    });
};
function antallBillet3() {
    let antall = parseInt($("#antall3").html());

    $('#plus3').click(function () {
        antall += 1;
        $('#antall3').html(antall);
    });

    $('#minus3').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall3').html(antall);

    });
};
function antallBillet4() {
    let antall = parseInt($("#antall4").html());

    $('#plus4').click(function () {
        antall += 1;
        $('#antall4').html(antall);
    });

    $('#minus4').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall4').html(antall);

    });
};

function antallBillet5() {
    let antall = parseInt($("#antall5").html());

    $('#plus5').click(function () {
        antall += 1;
        $('#antall5').html(antall);
    });

    $('#minus5').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall5').html(antall);

    });
};

function antallBillet6() {
    let antall = parseInt($("#antall6").html());

    $('#plus6').click(function () {
        antall += 1;
        $('#antall6').html(antall);
    });

    $('#minus6').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall6').html(antall);

    });
};

function antallBillet7() {
    let antall = parseInt($("#antall7").html());

    $('#plus7').click(function () {
        antall += 1;
        $('#antall7').html(antall);
    });

    $('#minus7').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall7').html(antall);

    });
};