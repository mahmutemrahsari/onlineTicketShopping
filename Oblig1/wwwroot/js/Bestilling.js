﻿function lagreBestilling() {
    const reise = {
        navn: $("#navn").val(),
        telefonnr: $("#telfonnr").val(),
        strekning: $("#strekning").val(),
        avganger: $("#avganger").val(),
        // noe: $("input:radio[name=noe]:checked").val(),
        priser: $("#priser").val()
    };
    const url = "/NorWay/SettInn";
    $.post(url, reise, function () {
        window.location.href = "index.html";
    });
};