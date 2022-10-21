function regPerson() {
    const person = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val()
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
        //"<td> <a class='btn btn-primary' href='endre.html?id=" + person.id + "'>Endre</a></td>" +
        //"<td> <button class='btn btn-success' onclick='slettKunde(" + person.id + ")'>Slett</button></td>" +
        "<td> <a class='btn btn-primary' href='index.html'>Tilbake</a> </td > " +
        "<td> <button class='btn btn-success' onclick='bekreftReg()'>Bekreft</button> </td>" +
        "</tr>" +
        "</table>";

    $("#personInfo").html(ut);
}

function bekreftReg() {
    const person = {

        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        fodselsnr: $("#fodselsnr").val(),
        adresse: $("#adresse").val(),
        tlf: $("#tlf").val(),
        epost: $("#epost").val()
    }

    const url = "Person/Lagre";
    $.post(url, person, function (OK) {
        if (OK) {

            hentPersonen();

            $("#fornavn").val("");
            $("#etternavn").val("");
            $("#fodselsnr").val("");
            $("#adresse").val("");
            $("#tlf").val("");
            $("#epost").val("");
            
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

function hentAllePersoner() {
    $.get("Person/HentAlle", function (personer) {
        formaterPersoner(personer);
    });
}

function formaterPersoner(personer) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Fornavn</th><th>Etternavn</th><th>Fødselsnr</th><th>Adresse</th><th>Tlf</th><th>Epost</th><th></th><th></th>" +
        "</tr>";
    for (let person of personer) {
        ut += "<tr>" +
            "<td>" + person.fornavn + "</td>" +
            "<td>" + person.etternavn + "</td>" +
            "<td>" + person.fodselsnr + "</td>" +
            "<td>" + person.adresse + "</td>" +
            "<td>" + person.tlf + "</td>" +
            "<td>" + person.epost + "</td>" +
            "<td> <a class='btn btn-primary' href='endre.html?id=" + person.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettPerson(" + person.id + ")'>Slett</button></td>" +
            "</tr>";
        
    }
    ut += "</table>";

    $("#personene").html(ut);
    
}


function slettPerson(id) {
    const url = "Person/Slett?id=" + id;
    $.get(url, function (OK) {
        if (OK) {
            window.location.href = 'index1.html';
            //hentAllePersoner();
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }

    });
};



function visOpplysninger() {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Fornavn</th><th>Etternavn</th><th>Fødselsnr</th><th>Adresse</th><th>Tlf</th><th>Epost</th><th></th><th></th>" +
        "</tr>";
    ut += "<tr>" +
        "<td>" + $("#fornavn").val() + "</td>" +
        "<td>" + $("#etternavn").val() + "</td>" +
        "<td>" + $("#fodselsnr").val() + "</td>" +
        "<td>" + $("#adresse").val() + "</td>" +
        "<td>" + $("#tlf").val() + "</td>" +
        "<td>" + $("#epost").val() + "</td>" +
        
        //"<td> <a class='btn btn-primary' href='endre.html?id=" + person.id + "'>Endre</a></td>" +
        //"<td> <button class='btn btn-success' onclick='slettKunde(" + person.id + ")'>Slett</button></td>" +
        "<td> <button class='btn btn-primary' onclick='slettVisEnPerson()'>Endre</button> </td>" +
        "<td> <button class='btn btn-danger' onclick='slettVisEnPerson()'>Slett</button>" +
        "<td> <a class='btn btn-success' href='main.html'> Bekeft</a> </td > " +
        "</tr>";
    ut += "</table>";
    $("#personene").html(ut);
}

function slettVisEnPerson() {
    $("#personene").html("");
}






function slettAlle() {
    $.get("/slettAlle", function () {
        hentAlle();
    });
}








/*<script>
    function register () {
        const forNavn = $("#fornavn").val();
        const etterNavn= $("#etternavn").val();
        const fødselsnr = $("#fødselsnr").val();
        const adresse = $("#adresse").val();
        const tlf= $("#tlf").val();
        const epost= $("#epost").val();

        let forNavn = true;
        let riktigetterNavn = true;
        let riktigfødselsnr = true;
        let riktigadresse = true;
        let riktigtlf = true;
        let riktigepost= true;
  
        //Hvis alt fungere skal jeg legge til dette inn i min tabell og pushe det også
        if (riktigforNavn && riktigetterNavn && riktigfødselsnr && riktigadresse && riktigtlf && riktigepost)
        {
            let personInfo = {
                forNavn : fornavn,
                etterNavn : etternavn,
                fødselsnr : fødselsnr,
                adresse : adresse,
                tlf : tlf,
                epost : epost,

            };
            $.post("/lagre", personInfo, function(){
                hentAlle();
            });

            //tømmer alle input feltene

            $("forNavn").val("");
            $("etterNavn").val("");
            $("fødselsnr").val("");
            $("adresse").val("");
            $("tlf").val("");
            $("epost").val("");

        }
    }
    function hentAlle(){
        $.get("/hentAlle", function (data){
            formaterData(data);
        });
    }
    function formaterData(personData){
        let ut ="";
        ut += "<table class='table table-striped table-bordered'>";
        ut+="<tr>" +
            "<th>forNavn</th>" +
            "<th>etterNavn</th>" +
            "<th>fødselsnr</th>" +
            "<th>adresse</th>" +
            "<th>tlf</th>" +
            "<th>epost</th>" +
            "</tr>";
        for (let p of personData) {
            ut += "<tr>" +
                "<td>" + p.forNavn + "</td>"+
                "<td>" + p.etterNavn + "</td>" +
                "<td>" + p.fødselsnr  + "</td>" +
                "<td>" + p.adresse + "</td>" +
                "<td>" + p.tlf + "</td>" +
                "<td>" + p.epost + "</td>" +
                "</tr>";
        }
        ut += "</table>";
        $("#PersonInfo").html(ut);
    }
    function SlettAlleInfo() {
        $.post("/slettAlle", function () {
            hentAlle();
        });
    }

</script>*/