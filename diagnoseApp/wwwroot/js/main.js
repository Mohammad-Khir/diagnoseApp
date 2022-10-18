$(function () {
    hentAllePersoner();
});

function hentAllePersoner() {
    $.get("person/hentAlle", function (personer) {
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
            "<td> <a class='btn btn-primary' href='endre.html?id=" + kunde.id + "'>Endre</a></td>" +
            "<td> <button class='btn btn-danger' onclick='slettKunde(" + kunde.id + ")'>Slett</button></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#personene").html(ut);
}