function loggInn() {
	const brukernavnOK = validerBrukernavn(("#brukernavn").val());
	const passordOK = validerPassord(("#passord").val());

	if (brukernavnOK && passordOK) {
		const bruker = {
			brukernavn: $("#brukernavn").val(),
			passord: ("#passord").val()
		}
		const url = "NorWay/LoggInn";
		$.post(url, bruker, function (OK) {
			if (OK) {
				window.location.href = "index.html";
			} else {
				$("#feil").html("Feil brukernavn eller passord, sjekker hvis du er admin");
			}
		})
		.fail(function () {
			$("#feil"), html("Feil på server, prøve igjen senere!");
		});
	}
}