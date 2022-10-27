function diagnosere() {

    let count = 0;

    let f = $("input:radio[name=feber]:checked").val();
    let h = $("input:radio[name=hoste]:checked").val();
    let hp = $("input:radio[name=hp]:checked").val();
    let s = $("input:radio[name=slapphet]:checked").val();
    let tp = $("input:radio[name=tp]:checked").val();
    let ut = "pasienten har : "

    if (f != null) ut += f + " , ";
    if (f == "feber") count++;

    if (h != null) ut += h + " , ";
    if (h == "hoste") count++;

    if (hp != null) ut += hp + " , ";
    if (hp == "hodepine") count++;

    if (s != null) ut += s + " , ";
    if (s == "slapphet") count++;

    if (tp != null) ut += tp + " , ";
    if (tp == "tungpustehet") count++;


    ut += ". Nedsatt på : ";
    const miste = $("input:checkbox[name=miste]:checked");
    if (miste.length == 1) count++;
    if (miste.length == 2) count += 2;
    for (let sans of miste) {
        ut += sans.defaultValue + " ";
    }

    if (miste.length == 0) ut += "ingen nedsatt.";
    else ut += ".";



    ut += " <br/>Andre symptomer  : ";
    const andre = $("input:checkbox[name=andre]:checked");
    if (andre.length == 1) count++;
    if (andre.length == 2) count += 2;
    for (let en of andre) {
        ut += en.defaultValue + " ";
    }
    if (andre.length == 0) ut += "ingen andre symptomer.";
    else ut += ".";



    //alert(count);
    //alert(ut);


    //$("#result").html(ut);


    //desied the result 


    let result = "";
    if (count >= 5) {
        result += "Pasienten er mestsansenelig smitta";
    } else {
        result += "Pasienten er mestsansenelig ikke smitta";
    }
    return result;
    
    
}



function lagreTest() {
    const test = {
        dato: $("#dato").val(),
        resultat: diagnosere()
    }
    person.tester = test;
    const url = "Person/Lagre";
    $.post(url, person.tester, function (OK) {
        if (OK) {

            window.location.href = "result.html?id=" + test.id;
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}

function hentResultat(personer) {
    let ut = "";
    for (let person of personer) {
        ut += person.fornavn + " " + person.etternavn + "<br>";
        for (let test of person.tester) {
            ut += test.dato + "<br>";
            ut += test.resultat;
        }
    }
    $("#result").html(ut);
}
