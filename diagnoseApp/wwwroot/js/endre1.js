$(function () {      
    hentEn();
});

function hentEn() {

    const id = window.location.search.substring(1);
    const url = "Person/HentEn?" + id;
    $.get(url, function (person) {
        $("#id").val(person.id); 
        $("#fornavn").val(person.fornavn);
        $("#etternavn").val(person.etternavn);
        $("#fodselsnr").val(person.fodselsnr);
        $("#adresse").val(person.adresse);
        $("#tlf").val(person.tlf);
        $("#epost").val(person.epost);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInn.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
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

function endrePerson() { 
    const person = {
        id: $("#id").val(), 
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val()

    }
    $.post("Person/Endre", person, function () {
        
        window.location.href = 'index1.html';
    })
    .fail(function (feil) {
        if (feil.status == 401) { 
            window.location.href = 'loggInn.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}





