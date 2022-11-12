function loggInn() {

    const brukernavnOK = validerBrukernavn($("#brukernavn").val());
    const passordOK = validerPassord($("#passord").val());

    if (brukernavnOK && passordOK) {
        const bruker = {
            brukernavn: $("#brukernavn").val(),
            passord: $("#passord").val()
        }
        $.post("Person/LoggInn", bruker, function (OK) {
            if (OK) {
                window.location.href = "indexAdmin.html";
            }
            else {
                $("#feil").html("Feil brukernavn eller passord");
            }
        })
        .fail(function (feil) {
            $("#feil").html("Feil på server - prøv igjen senere: " + feil.responseText + " : " + feil.status + " : " + feil.statusText);
        });
    }
}
/*function hentPersonen() {
    $.get("Person/HentAlle", function (personer) {
        for (let person of personer) {
            window.location.href = "velkommen.html?id=" + person.id;
        }
    })
    .fail(function () {
        $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
    });
}*/
