

function antall() {
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
    //var a = antallB(antall[0]);
    let p = document.getElementsByClassName("pristype");
    //let Voksen = p[0].innerHTML;
    if (p[0].innerHTML == "Voksen") {
        plusminus("#plus1", "#antall1", "#tall0", "#typeAv0", "#minus1", p[0].innerHTML);
    }
    if (p[1].innerHTML == "Barn") {
        plusminus("#plus2", "#antall2", "#tall1", "#typeAv1", "#minus2", p[1].innerHTML);
    }
    if (p[2].innerHTML == "Student") {
        plusminus("#plus3", "#antall3", "#tall2", "#typeAv2", "#minus3", p[2].innerHTML);
    }
    if (p[3].innerHTML == "Ungdom") {
        plusminus("#plus4", "#antall4", "#tall3", "#typeAv3", "#minus4", p[3].innerHTML);
    }
    if (p[4].innerHTML == "Honnor") {
        plusminus("#plus5", "#antall5", "#tall4", "#typeAv4", "#minus5", p[4].innerHTML);
    }
    if (p[5].innerHTML == "Verneplikt") {
        plusminus("#plus6", "#antall6", "#tall5", "#typeAv5", "#minus6", p[5].innerHTML);
    }
    if (p[6].innerHTML == "Ledsager") {
        plusminus("#plus7", "#antall7", "#tall6", "#typeAv6", "#minus7", p[6].innerHTML);
    }
}

function plusminus(pl, antall, tall, type,mi,typebillet) {
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