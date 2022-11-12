function validerOgRegPerson() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const fodselsnrOK = validerFodselsnr($("#fodselsnr").val());
    const adresseOK = validerAdresse($("#adresse").val());
    const tlfOK = validerTlf($("#tlf").val());
    const epostOK = validerEpost($("#epost").val());
    const passordOK = validerPassord($("#passord").val());
    if (fornavnOK && etternavnOK && fodselsnrOK && adresseOK && tlfOK && epostOK && passordOK) {
        regPerson();
    }
}

function regPerson() {      // metode for registrering og formatering av ny person/pasient opplysninger uten lagring i DB
    const person = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val(),
        passord: $("#passord").val()
    }

    let ut = "<h3> Hei " + person.fornavn + ", vil du bekrefte dine opplysninger? </h3>" +
        "<table class='table table-striped' style='background-color: aqua'>" +
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
        "</tr>" +
        "<tr>" + "</tr>" +
        "</table>" +

        "<table>" +
        "<tr>" +
        "<td> <a class='btn btn-primary' href='index.html'>Tilbake</a> </td > " +
        "<td> <button class='btn btn-success' onclick='bekreftReg()'>Bekreft</button> </td>" +
        "</tr>" +
        "</table>";

    $("#personInfo").html(ut);
}

function bekreftReg() {     // lagre Person-opplysninger i DB (Bekreft knapp)
    const person = {

        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val(),
        passord: $("#passord").val()
    }

    const url = "Person/Lagre";
    $.post(url, person, function () {
        hentPersonen();

        $("#fornavn").val("");
        $("#etternavn").val("");
        $("#fodselsnr").val("");
        $("#adresse").val("");
        $("#tlf").val("");
        $("#epost").val("");
        $("#passord").val("");
   
    })
    .fail(function (feil) {
        if (feil.status == 401) {  
            window.location.href = 'loggInnPerson.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}

function hentPersonen() {       // Hente person-opplysninger fra DB og flytte til Velkommen side med ønskede person-id
    $.get("Person/HentAlle", function (personer) {
        for (let person of personer) {
            window.location.href = "velkommen.html?id=" + person.id;
        }
    })
    .fail(function (feil) {
        if (feil.status == 401) {  
            window.location.href = 'loggInnPerson.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}    
