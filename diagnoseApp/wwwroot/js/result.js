$(function () {           // denne kjøres når dokumentet er ferdig lastet
    hentEnTest();
});

function hentEnTest() {

    var params = {},
        uriparams = location.search.substr(1).split('&');
    for (var i = 0; i < uriparams.length; i++) {
        var parameter = uriparams[i].split('=');
       params[ parameter[0]] = parameter[1];
    }
    let personid = params.id;
    let testid = params.testid;
    console.log('id -- ' + personid + ' , testid -- ' + testid);

    //const url = "Person/HentEnTest?id=" + id + "&testid=" + testid;
    const url = "Person/HentEnTest";
    const urlparams = {
        personid: personid,
        id: testid
    }
    $.get(url, urlparams, function (result) {

        formaterEn(result);
    })
    .fail(function (feil) {
        if (feil.status == 401) {  // ikke logget inn, redirect til loggInn.html
            window.location.href = 'loggInnPerson.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}

let ut = "";
function formaterEn(result) {
    ut = "<h2>The result</h2>" +
        "<table class='table table-striped' style='background-color: orange'>" +
        "<tr>" +
        "<th>Fornavn</th><th>Etternavn</th><th>Fødselsnr</th><th>Adresse</th><th>Tlf</th><th>Epost</th><th>Dato</th><th>Resultat</th><th></th><th></th>" +
        "</tr>" +
        "<tr>" +
        "<td>" + result.fornavn + "</td>" +
        "<td>" + result.etternavn + "</td>" +
        "<td>" + result.fodselsnr + "</td>" +
        "<td>" + result.adresse + "</td>" +
        "<td>" + result.tlf + "</td>" +
        "<td>" + result.epost + "</td>" +
        "<td>" + result.dato + "</td>" +
        "<td>" + result.resultat + "</td>" +
        "</tr>" +
        "</table>" + "<br />";
    ut += "<div><button class='btn btn-danger' onclick='hentAlleTester()'>Se dine tidligere tester</button></div>";
    console.log('ut -- ' + ut);
    $("#result").html(ut);
}


function hentAlleTester() {       
    $.get("Person/HentAlleTester", function (tester) {
        
        formaterTester(tester);
        
    })
    .fail(function (feil) {
        if (feil.status == 401) {  // ikke logget inn, redirect til loggInn.html
            window.location.href = 'loggInnPerson.html';
        } else {
            $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
        }
    });
}

function formaterTester(tester) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>TestID</th><th>Dato</th><th>Resutat</th>" +
        "</tr>";
    for (let test of tester) {
        ut += "<tr>" +
            "<td>" + test.id + "</td>" +
            "<td>" + test.dato + "</td>" +
            "<td>" + test.resultat + "</td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#kundene").html(ut);
    console.log('ut -- ' + ut);
    $("#tidligereTest").html(ut);
}

function loggUt() {
    $.get("Person/LoggUt", function () {
        window.location.href = 'home.html';
    });
}

