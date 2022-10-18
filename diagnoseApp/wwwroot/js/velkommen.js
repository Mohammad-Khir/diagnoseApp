$(function () {
    hentEnPerson();
});

function hentEnPerson() {
    // hent kunden med kunde-id fra url og vis denne i skjemaet

    const id = window.location.search.substring(1);
    const url = "Person/HentEn?" + id;
    $.get(url, function (person) {

        formaterEn(person);
    });
}


function formaterEn(person) {
    let ut = "<table class='table table-striped' style='background-color: orange'>" +
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
        "</table>";

    $("#personene").html(ut);
}

/*function hentOgSlett() {
    $.get("Person/HentAlle", function (personer) {
        for (let person of personer) {
            slettPerson(person.id);
        }
    });
}*/
function slettPerson(id) {
    const url = "Person/Slett?id=" + id;
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = 'index1.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }

    });
};


function hentEn() {
    // hent kunden med kunde-id fra url og vis denne i skjemaet

    const id = window.location.search.substring(1);
    const url = "Person/HentEn?" + id;
    $.get(url, function (person) {
        $("#id").val(person.id); // må ha med id inn skjemaet, hidden i html
        $("#fornavn").val(person.fornavn);
        $("#etternavn").val(person.etternavn);
        $("#fodselsnr").val(person.fodselsnr);
        $("#adresse").val(person.adresse);
        $("#tlf").val(person.tlf);
        $("#epost").val(person.epost);
    });
}

