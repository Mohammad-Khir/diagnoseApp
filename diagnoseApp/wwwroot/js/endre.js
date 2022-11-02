$(function () {       // denne kjøres når dokumentet er ferdig lastet
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
    })
    .fail(function () {
        $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
    });
}

function validerOgEndrePerson() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const fodselsnrOK = validerFodselsnr($("#fodselsnr").val());
    const adresseOK = validerAdresse($("#adresse").val());
    const tlfOK = validerTlf($("#tlf").val());
    const epostOK = validerEpost($("#epost").val());
    if (fornavnOK && etternavnOK && fodselsnrOK && adresseOK && tlfOK && epostOK) {
        endrePerson();
    }
}

function endrePerson() { //Endre person-opplysninger til personen som ble hentet i hentEn() og lagre endringene
    const person = {
        id: $("#id").val(), // må ha med denne som ikke har blitt endret for å vite hvilken person som skal endres
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val()

    }
    $.post("Person/Endre", person, function () {
        
        hentPersonen();
    })
    .fail(function () {
        $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
    });
}

function hentPersonen() {   //Hente de endrede person-opplysningene fra DB og flytte til Velkommen side med ønskede person-id
    $.get("Person/HentAlle", function (personer) {
        for (let person of personer) {
            window.location.href = "velkommen.html?id=" + person.id;
        }
    })
    .fail(function () {
        $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
    });
}




