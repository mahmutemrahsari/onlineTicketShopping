function lagreBestilling() {
    const reise = {
        //Epost: $("#navn").val(),
        //telefonnr: $("#telfonnr").val(),
        Epost: $("#Epost").val(),
        Pris: $("#Pris").val(),
        Billettype: $("#billettType").val(),
        FraSted: $("#avgang").val(),
        TilSted: $("#destinasjon").val(),
        AvgangersDato: $("#date1").val(),
        ReturDato: $("#date2").val(),
        Antall: $("#antall").val()
        // noe: $("input:radio[name=noe]:checked").val(),
        //priser: $("#priser").val()
    }
    const url = "NorWay/Lagre";
    $.post(url, reise, function () {
        window.location.href = "bestill.html";
    });
};