$(function () {           // denne kjøres når dokumentet er ferdig lastet
    hentEnPerson();
});

function hentEnPerson() {
    // hent personen med person-id fra url og vis denne

    const id = window.location.search.substring(1);
    const url = "Person/HentEn?" + id;
    $.get(url, function (person) {

        formaterEn(person);
    })
    .fail(function (feil) {
        if (feil.status == 401) {  
            window.location.href = 'loggInnPerson.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}


function formaterEn(person) {   // formatere Person-opplysninger og vis de til brukeren
    let ut = "<h2>Velkommen " + person.fornavn +" hos Dr.Health!</h2>" +
        "<table class='table table-striped' style='background-color: orange'>" +
        "<tr>" +
        "<th>Fornavn</th><th>Etternavn</th><th>Fødselsnr</th><th>Adresse</th><th>Tlf</th><th>Epost</th><th></th><th></th>" +
        "</tr>" +
        "<tr>" +
        "<td>" + person.fornavn + "</td>" +
        "<td>" + person.etternavn + "</td>" +
        "<td>" + person.fodselsnr + "</td>" +
        "<td>" + person.adresse + "</td>" +
        "<td>" + person.tlf + "</td>" +
        "<td>" + person.epost + "</td>" +
        "<td> <a class='btn btn-primary' href='endre.html?id=" + person.id + "'>Endre</a></td>" +
        "<td> <button class='btn btn-danger' onclick='slettPerson(" + person.id + ")'>Slett</button></td>" +
        "</tr>" +
        "</table>" + "<br />" +
        "<a class='btn btn-primary' href='main.html?id=" + person.id + "'>Diagnoser</a>";
    $("#personene").html(ut);
}

function slettPerson(id) {  // slette person-opplysningerfra DB ved hjelp av id (Slett knapp)
    const url = "Person/Slett?id=" + id;
    $.get(url, function () {

        $("#personene").html("<h3 style='color: red'>Alle opplysninger er slettet!</h3>");
    })
    .fail(function (feil) {
        if (feil.status == 401) {  
            window.location.href = 'loggInnPerson.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}
function loggUt() {
    $.get("Person/LoggUt", function () {
        window.location.href = 'home.html';
    });
}

