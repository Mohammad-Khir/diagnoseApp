function diagnosere() { // for å velge aktuelle symptomer og bestemme resultatet

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



    let result = "";
    if (count >= 3) {
        result += "Mest sannsynlig smittet";
    } else {
        result += "Mest sannsynlig ikke smittet";
    }
    //alert(result);
    //get data from dateInputField
    let dato = $("#dato").val();
    const test = {
        personid: window.location.search.substring(1),
        dato: dato,
        resultat: result
    };

    const url = "Person/LagreTest";

    var params = {},
        uriparams = location.search.substr(1).split('&');
    for (var i = 0; i < uriparams.length; i++) {
        var parameter = uriparams[i].split('=');
        params[parameter[0]] = parameter[1];
    }
    let personid = params.id;

    $.post(url, test, function (testid) {

        // alert(ok);
        window.location.href = "result.html?id=" + personid + '&testid=' + testid;

    })
        .fail(function (feil) {
            if (feil.status == 401) {  // ikke logget inn, redirect til loggInn.html
                window.location.href = 'loggInn.html';
            } else {
                $("#feil").html("Obs! det oppstod en feil på server, prøv gjerne igjen senere");
            }
        });

}

function loggUt() {
    $.get("Person/LoggUt", function () {
        window.location.href = 'home.html';
    });
}
