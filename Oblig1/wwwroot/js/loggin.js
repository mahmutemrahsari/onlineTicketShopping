﻿function loggInn() {
	const brukernavnOK = validerBrukernavn($("#brukernavn").val());
	const passordOK = validerPassord($("#passord").val());

	if (brukernavnOK && passordOK) {
		const bruker = {
			brukernavn: $("#brukernavn").val(),
			passord: $("#passord").val(),
		}
		const url = "Norway/LoggInn";
		$.post(url, bruker, function (OK) {
			if (OK) {
				window.location.href = "admin.html";
			} else {
				$("#feil").html("Feil brukernavn eller passord, sjekker hvis du er admin");
			}
		})
			.fail(function (feil) {
				$("#feil").html("Feil på server - prøv igjen senere: " + feil.responseText + " : " + feil.status + " : " + feil.statusText);
		});
	}
}