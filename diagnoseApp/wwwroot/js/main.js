function diagnosere() {
    

    let ut = "pasienten har : " + $("input:radio[name=feber]:checked").val() +
        "og har " + $("input:radio[name=hoste]:checked").val() +
        "og har " + $("input:radio[name=hp]:checked").val() +
        "og har " + $("input:radio[name=slapphet]:checked").val() +
        "og har " + $("input:radio[name=tp]:checked").val();

    ut += "<br/>og nedsatt på : ";
    const miste = $("input:checkbox[name=miste]:checked");
    for (let sans of miste) {
        ut += sans.misteValue + "og ";
    }

    ut += "<br/>og andre symptomer  : ";
    const andre = $("input:checkbox[name=andre]:checked");
    for (let en of andre) {
        ut += en.andreValue + "og ";
    }

    $("#result").html(ut);

    const url = "/lagresymptomer";
    $.post(url, symptomer, function () {
        
    });

}

function hentSymtomene() {
    $.get("Symptom/HentAlle", function (symptomer) {
        for (let symptom of symptomer) {
            window.location.href = "result.html?id=" + symptom.id;
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

function formaterResultat() {

}
