function an() {
    //Hente pris fra database
    var pris = document.getElementsByClassName("pris");
    var antall = document.getElementsByClassName("a1");
    var Billett = document.getElementsByClassName("typeBillett");
    let typeB = "";
    let ut = 0;
    for (var i = 0; i < pris.length; i++) {
        if (antall[i].innerHTML != 0) {
            Billett[i].innerText = Number(antall[i].innerHTML) * Number(pris[i].innerHTML);
            ut += Number(Billett[i].innerHTML);
        }
    }
    $("#TotalPris").val(ut);
}

function getElementsById(ids) {
    var idList = ids.split(" ");
    var results = [], item;
    for (var i = 0; i < idList.length; i++) {
        item = document.getElementById(idList[i]);
        if (item) {
            results.push(item);
        }
    }
    return (results);
}




//function som skal hente alle type til database.
function hentTypeOgAntall() {
    var type = document.getElementsByClassName("typeAv");
    var tall = document.getElementsByClassName("tall");
    let antallB = tallB = "";
    let antallTall = 0;


    for (var i = 0; i < type.length; i++) {
        tallB += Number(tall[i].innerHTML);
        antallTall += Number(tall[i].innerHTML);
        if (Number(tall[i].innerHTML) != 0) {
            antallB += Number(tall[i].innerHTML) + type[i].innerHTML + "";
        }
    }

    if (tallB != 0) {
        $("#antallType").val(antallB);
        $("#antallTicket").val(antallTall);
    }

}



//Antall billet kan endres + og - buttonner

function antallBillet() {
    //var a = antallB(antall[0]);
    let p = document.getElementsByClassName("pristype");
    let plus = antall = tall = type = minus = "";
    for (var i = 0; i < p.length; i++) {

        plusminus("#plus" + i, "#antall" + i, "#tall" + i, "#typeAv" + i, "#minus" + i, p[i].innerHTML);

    }
}




function docReady(fn) {
    // see if DOM is already available
    if (document.readyState === "complete" || document.readyState === "interactive") {
        // call on next available tick
        setTimeout(fn, 1);
    } else {
        document.addEventListener("DOMContentLoaded", fn);
    }
}

docReady(function () {
    var ek = [];
    $('.pristype').each(function () { ek.push($(this).val()); });
    $("#Plass_2").html(ek);
});


function formateAn() {
    $.get("Norway/HentPrisType", function (pris) {
        let ut = ut1 = "";
        let ut2 = "";
        for (var i = 0; i < pris.length; i++) {
            ut += "<input type=" + '"' + "button" + '"' + "value=" + '"' + "-" + '"' + "id=" + '"' + "minus" + i + '"' + " " +
                "onclick=" + '"' + "antallBillet()" + '"' + "/> " + "<span id=" + '"' + "antall" + i + '"' + "class=" + '"' + "a1" + '"' + ">" + 0
                + "</span>" + "<input type=" + '"' +
                "button" + '"' + "value=" + '"' + "+" + '"' + "id=" + '"' + "plus" + i + '"' + " " +
                "onclick=" + '"' + "antallBillet()" + '"' + "/> " + "<br />"

            ut2 += "<span id=" + '"' + "tall" + i + '"' + "class=" + '"' + "tall" + '"' + "></span>"
                + "<span id=" + '"' + "typeAv" + i + '"' + "class=" + '"' + "typeAv" + '"' + "></span>";
        }
        $("#Plass_2").html(ut);
        for (var i = 0; i < pris.length; i++) {
            ut1 += "<div class=" + '"' + "typeBillett" + '"' + "></div>"
        }
        $("#Total").html(ut1);
        $("#b1").html(ut2);
    });
}

function plusminus(pl, antall, tall, type, mi, typebillet) {
    let an = parseInt($(antall).html());
    $(pl).click(function () {
        an += 1;
        $(antall).html(an);
        $(tall).html(an);
        $(type).html(" " + typebillet);
        $('#title').html("");
    });
    $(mi).click(function () {
        if (an > 0) {
            an -= 1;
        }
        $(antall).html(an);
        if (an == 0) {
            hentypefeilhåntering();
            $(type).html("");
            $(tall).html("");
        } else {
            $(tall).html(an);
            $(type).html(" " + typebillet);
        }
    });
}

function hentypefeilhåntering() {
    var type = document.getElementsByClassName("tall");
    if (type[0].innerHTML == 0 && type[1].innerHTML == 0 && type[2].innerHTML == 0 && type[3].innerHTML == 0 &&
        type[4].innerHTML == 0 && type[5].innerHTML == 0 && type[6].innerHTML == 0) {
        $("#title").html("Velg type av billett");
    }
}