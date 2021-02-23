function validacion() {
    var id = document.getElementById('Identificacion').value;
    //var cont = document.getElementById('2').value;
    //var vcont = document.getElementById('v2').value;
    //var email = document.getElementById('3').value;
    //var telef = document.getElementById('4').value;
    var ape = document.getElementById('Apellido1').value;

    if (id == null || id.length == 0 || /^\s+$/.test(nom)) {
        alert("Campo nombre esta vacio");
        return false;
    }
    if (ape == null || ape.length == 0 || /^\s+$/.test(cont)) {
        alert("Campo contraseña esta vacio");
        return false;
    }
    //if (vcont == null || vcont.length == 0 || /^\s+$/.test(vcont)) {
    //    alert("Campo Confirmar Contraseña esta vacio");
    //    return false;
    //}
    //if (vcont != cont) {
    //    alert("Contraseña no coinciden");
    //    return false;
    //}
    //if (email == null || email.length == 0 || /^\s+$/.test(email)) {
    //    alert("Campo e-mail esta vacio");
    //    return false;
    //}
    //if (telef == null || telef.length == 0 || /^\s+$/.test(telef)) {
    //    alert("Campo teléfono esta vacio");
    //    return false;
    //}
    //if (direc == null || direc.length == 0 || /^\s+$/.test(direc)) {
    //    alert("Campo Dirección esta vacio");
    //    return false;
    //}

}