function lagreBestilling() {
    const reise = {
        //Epost: $("#navn").val(),
        //telefonnr: $("#telfonnr").val(),
        Epost: $("#Epost").val(),
        Telefonnr: $("#Telefonnr").val(),
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
    const url = "NorWay/SettInn";
    $.post(url, reise, function () {
        window.location.href = "index1.html";
    });
};