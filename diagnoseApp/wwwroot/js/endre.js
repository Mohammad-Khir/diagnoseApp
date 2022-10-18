$(function () {
    hentEn();
});

function hentEn() {
    // hent personen med person-id fra url og vis denne i skjemaet

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

function endrePerson() {
    const person = {
        id: $("#id").val(), // må ha med denne som ikke har blitt endret for å vite hvilken person som skal endres
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val()
      
    }
    $.post("Person/Endre", person, function (OK) {
        if (OK) {
            hentPersonen();
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}

function hentPersonen() {
    $.get("Person/HentAlle", function (personer) {
        for (let person of personer) {
            window.location.href = "velkommen.html?id=" + person.id;
        }
    });
}




