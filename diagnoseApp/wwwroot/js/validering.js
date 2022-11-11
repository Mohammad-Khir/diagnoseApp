function validerFornavn(fornavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(fornavn);
    if (!ok) {
        $("#feilFornavn").html("Feil fornavn");
        return false;
    }
    else {
        $("#feilFornavn").html("");
        return true;
    }
}

function validerEtternavn(etternavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(etternavn);
    if (!ok) {
        $("#feilEtternavn").html("Feil i etternavn");
        return false;
    }
    else {
        $("#feilEtternavn").html("");
        return true;
    }
}
function validerFodselsnr(fodselsnr) {
    var regexp = /^[0-9.]{11}$/;
    var ok = regexp.test(fodselsnr);
    if (!ok) {
        $("#feilFodselsnr").html("Fodslesnr må fylles på riktig form");
        return false;
    }
    else {
        $("#feilFodselsnr").html("");
        return true;
    }
}
function validerAdresse(adresse) {
    var regexp = /^[0-9a-zA-ZæøåÆØÅ\ \.\-]{2,50}$/;
    var ok = regexp.test(adresse);
    if (!ok) {
        $("#feilAdresse").html("Adressen må bestå av 2 til 50 bokstaver og tall");
        return false;
    }
    else {
        $("#feilAdresse").html("");
        return true;
    }
}


function validerTlf(tlf) {
    var regexp = /^[0-9]{8}$/;
    var ok = regexp.test(tlf);
    if (!ok) {
        $("#feilTlf").html("Telefunnr må skrives på riktig form");
        return false;
    }
    else {
        $("#feilTlf").html("");
        return true;
    }
}

function validerEpost(epost) {
    var regexp = /^[0-9a-zA-ZæøåÆØÅ\:\@\_\.\-]{2,50}$/;
    var ok = regexp.test(epost);
    if (!ok) {
        $("#feilEpost").html("Epost adresse må skrives på riktig form");
        return false;
    }
    else {
        $("#feilEpost").html("");
        return true;
    }
}

function validerBrukernavn(brukernavn) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(brukernavn);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av 2 til 20 bokstaver");
        return false;
    }
    else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function validerPassord(passord) {
    const regexp = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4,}$/;
    const ok = regexp.test(passord);
    if (!ok) {
        $("#feilPassord").html("Passordet må bestå minimum 4 tegn, minst en bokstav og et tall");
        return false;
    }
    else {
        $("#feilPassord").html("");
        return true;
    }
}






