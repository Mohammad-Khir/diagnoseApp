$(function () {
    hentAllePersoner();
});

function hentAllePersoner() {
    $.get("Person/hentAlle", function (personer) {
        formaterPersoner(personer);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInnAdmin.html';
        }
        else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}


function formaterPersoner(personer) {
    let ut = "<table class='table table-striped' style='background-color: orange'>" +
            "<tr>" +
        "<th>Fornavn</th><th>Etternavn</th><th>Fødselsnr</th><th>Adresse</th><th>Tlf</th><th>Epost</th><th></th><th></th><th></th>" +
            "</tr>";
    for (let person of personer) {
        ut += "<tr>" +
            "<td>" + person.fornavn + "</td>" +
            "<td>" + person.etternavn + "</td>" +
            "<td>" + person.fodselsnr + "</td>" +
            "<td>" + person.adresse + "</td>" +
            "<td>" + person.tlf + "</td>" +
            "<td>" + person.epost + "</td>" +
            "<td> <a class='btn btn-primary' href='endreAdmin.html?id=" + person.id + "'>Endre</a></td>" +
            "<td> <a class='btn btn-success' href='index.html'>Lagre ny</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettPerson(" + person.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#personene").html(ut);
}

function slettPerson(id) {  
    const url = "Person/Slett?id=" + id;
    $.get(url, function () {

        window.location.href = 'indexAdmin.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInnAdmin.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}
function loggUtAdmin() {
    $.get("Person/LoggUt", function () {
        window.location.href = 'home.html';
    });
}
