function loggInn() {

    const epostOK = validerEpost($("#epost").val());
    const passordOK = validerPassord($("#passord").val());

    if (epostOK && passordOK) {
        const person = {
            epost: $("#epost").val(),
            passord: $("#passord").val()
        }
        $.post("Person/LoggInnPerson", person, function (OK) {
            if (OK) {
                hentPersonen();
            }
            else {
                $("#feil").html("Feil brukernavn eller passord");
            }
        })
        .fail(function (feil) {
            $("#feil").html("Obs! Feil på server" + feil.responseText + " : " + feil.status + " : " + feil.statusText);
        });
    }
}
function hentPersonen() {
    $.get("Person/HentAlle", function (personer) {
        for (let person of personer) {
            window.location.href = "velkommen.html?id=" + person.id;
        }
    })
    .fail(function () {
        $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
    });
}
