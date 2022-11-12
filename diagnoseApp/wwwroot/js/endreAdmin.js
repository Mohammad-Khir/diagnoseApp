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
        $("#passord").val(person.passord);
    })
    .fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInnAdmin.html';
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
    const passordOK = validerPassord($("#passord").val());
    if (fornavnOK && etternavnOK && fodselsnrOK && adresseOK && tlfOK && epostOK && passordOK) {
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
        epost: $("#epost").val(),
        passord: $("#passord").val()

    }
    $.post("Person/Endre", person, function () {
        
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





