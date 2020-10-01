

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

//function som skal hente alle type til database.
function hentTypeOgAntall() {
    var cont1 = $("#typeAv").html();
    var cont2 = $("#typeAv1").html();
    var cont3 = $("#typeAv2").html();
    var cont4 = $("#typeAv3").html();
    var cont5 = $("#typeAv4").html();
    var cont6 = $("#typeAv5").html();
    var cont7 = $("#typeAv6").html();
    var cont_1 = $("#tall").html();
    var cont_2 = $("#tall1").html();
    var cont_3 = $("#tall2").html();
    var cont_4 = $("#tall3").html();
    var cont_5 = $("#tall4").html();
    var cont_6 = $("#tall5").html();
    var cont_7 = $("#tall6").html();
    if (cont1 != "" || cont2 != "" || cont3 != "" || cont4 != "" || cont5 != "" || cont6 != "" || cont7 != "") {
        var total = cont_1 + "" + cont1 + " " + cont_2 + "" + cont2 + " " + cont_3 + "" + cont3 + " " +
            cont_4 + "" + cont4 + " " + cont_5 + "" + cont5 + " " + cont_6 + "" + cont6 + " " + cont_7 + "" + cont7;
        var totalBillett = Number(cont_1) + Number(cont_2) + Number(cont_3) +
            Number(cont_4) + Number(cont_5) + Number(cont_6) + Number(cont_7);
        $("#antallType").val(total);
        $("#antallTicket").val(totalBillett);
    }

}


function hentypefeilhåntering() {
    var cont_1 = document.getElementById("tall").innerHTML;
    var cont_2 = document.getElementById("tall1").innerHTML;
    var cont_3 = document.getElementById("tall2").innerHTML;
    var cont_4 = document.getElementById("tall3").innerHTML;
    var cont_5 = document.getElementById("tall4").innerHTML;
    var cont_6 = document.getElementById("tall5").innerHTML;
    var cont_7 = document.getElementById("tall6").innerHTML;
    if (cont_1 == 0 && cont_2 == 0 && cont_3 == 0 && cont_4 == 0 && cont_5 == 0 && cont_6 == 0 && cont_7 == 0) {
        $("#title").html("Velg type av billett");
    } else if (cont_1 != 0 || cont_2 != 0 || cont_3 != 0 || cont_4 != 0 || cont_5 != 0 || cont_6 != 0 || cont_7 != 0) {
        $("#title").html("");
    }
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

//Antall billet kan endres + og - buttonner
function antallBillet() {
    let antall = parseInt($("#antall1").html());
    let p = document.getElementsByClassName("pristype");
    let Voksen = p[0].innerHTML;
    $('#plus1').click(function () {
        antall += 1;
        $('#antall1').html(antall);
        $('#tall').html(antall);
        $('#typeAv').html(" " + Voksen);
        $('#title').html("");
    });

    $('#minus1').click(function () {
        if (antall > 0) {
            antall -= 1;
        }
        $('#antall1').html(antall);
        if (antall == 0) {
            $('#typeAv').html("");
            $('#tall').html("");
            hentypefeilhåntering()
        } else {
            $('#tall').html(antall);
            $('#typeAv').html(" " + Voksen);
        }
    });
};

function antallBillet2() {
    let antall = parseInt($("#antall2").html());
    let p = document.getElementsByClassName("pristype");
    let barn = p[1].innerHTML;
    $('#plus2').click(function () {
        antall += 1;
        $('#antall2').html(antall);
        $('#tall1').html(antall);
        $('#typeAv1').html(" " + barn);
        $('#title').html("");
    });

    $('#minus2').click(function () {
        if (antall > 0) {
            antall -= 1;
        }
        $('#antall2').html(antall);
        if (antall == 0) {
            $('#typeAv1').html("");
            $('#tall1').html("");
            hentypefeilhåntering()
        } else {
            $('#tall1').html(antall);
            $('#typeAv1').html(" " + barn);
        }

    });
};
function antallBillet3() {
    let antall = parseInt($("#antall3").html());
    let p = document.getElementsByClassName("pristype");
    let student = p[2].innerHTML;
    $('#plus3').click(function () {
        antall += 1;
        $('#antall3').html(antall);
        $('#tall2').html(antall);
        $('#typeAv2').html(" " + student);
    });

    $('#minus3').click(function () {
        if (antall > 0) {
            antall -= 1;
        }
        $('#antall3').html(antall);
        if (antall == 0) {
            $('#typeAv2').html("");
            $('#tall2').html("");
            hentypefeilhåntering()
        } else {
            $('#tall2').html(antall);
            $('#typeAv2').html(" " + student);
        }

    });
};
function antallBillet4() {
    let antall = parseInt($("#antall4").html());
    let p = document.getElementsByClassName("pristype");
    let Ungdom = p[3].innerHTML;
    $('#plus4').click(function () {
        antall += 1;
        $('#antall4').html(antall);
        $('#tall3').html(antall);
        $('#typeAv3').html(" " + Ungdom);
    });

    $('#minus4').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall4').html(antall);
        if (antall == 0) {
            $('#typeAv3').html("");
            $('#tall3').html("");
            hentypefeilhåntering()
        } else {
            $('#tall3').html(antall);
            $('#typeAv3').html(" " + Ungdom);
        }
    });
};

function antallBillet5() {
    let antall = parseInt($("#antall5").html());
    let p = document.getElementsByClassName("pristype");
    let Honnor = p[4].innerHTML;
    $('#plus5').click(function () {
        antall += 1;
        $('#antall5').html(antall);
        $('#tall4').html(antall);
        $('#typeAv4').html(" " + Honnor);
    });

    $('#minus5').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall5').html(antall);
        if (antall == 0) {
            $('#typeAv4').html("");
            $('#tall4').html("");
            hentypefeilhåntering()
        } else {
            $('#tall4').html(antall);
            $('#typeAv4').html(" " + Honnor);
        }
    });
};

function antallBillet6() {
    let antall = parseInt($("#antall6").html());
    let p = document.getElementsByClassName("pristype");
    let Verneplikt = p[5].innerHTML;
    $('#plus6').click(function () {
        antall += 1;
        $('#antall6').html(antall);
        $('#tall5').html(antall);
        $('#typeAv5').html(" " + Verneplikt);
    });

    $('#minus6').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall6').html(antall);
        if (antall == 0) {
            $('#typeAv5').html("");
            $('#tall5').html("");
            hentypefeilhåntering()
        } else {
            $('#tall5').html(antall);
            $('#typeAv5').html(" " + Verneplikt);
        }
    });
};

function antallBillet7() {
    let antall = parseInt($("#antall7").html());
    let p = document.getElementsByClassName("pristype");
    let Ledsager = p[6].innerHTML;
    $('#plus7').click(function () {
        antall += 1;
        $('#antall7').html(antall);
        $('#tall6').html(antall);
        $('#typeAv6').html(" " + Ledsager);
    });

    $('#minus7').click(function () {
        if (antall > 0) {
            antall -= 1;
        }

        $('#antall7').html(antall);
        if (antall == 0) {
            $('#typeAv6').html("");
            $('#tall6').html("");
            hentypefeilhåntering()
        } else {
            $('#tall6').html(antall);
            $('#typeAv6').html(" " + Ledsager);
        }
    });
};