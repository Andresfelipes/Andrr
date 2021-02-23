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

$(document).on('keydown', '.validate-character', function (evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && (charCode < 96 || charCode > 105)) {
        jQuery(this).next("div").remove();
        jQuery(this).css("border", "");
        jQuery(this).css("background", "");
        jQuery(this).css("border", "1px solid #eb340a");
        jQuery(this).css("background", "#faebe7");
        jQuery(this).after('<div class="validation-custom validation-advice" id="advice-required-entry-' + name + '">Solo se permiten números.</div>');
        return false
    } else {
        jQuery(this).next("div").remove();
        jQuery(this).css("border", "");
        jQuery(this).css("background", "");
    }
    return true
});
jQuery(document).on('keydown', '.validate-number', function (event) {
    switch (event.keyCode) {
        case 9:  // Tab
        case 13: // Enter
        case 37: // Left
        case 38: // Up
        case 39: // Right
        case 40: // Down
            break;
        default:
            var regex = new RegExp("[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ_\s]+$");
            var key = event.key;
            if (!regex.test(key)) {
                event.preventDefault();
                jQuery(this).next("div").remove();
                jQuery(this).css("border", "");
                jQuery(this).css("background", "");
                jQuery(this).css("border", "1px solid #eb340a");
                jQuery(this).css("background", "#faebe7");
                jQuery(this).after('<div class="validation-custom validation-advice" id="advice-required-entry-' + name + '">Solo se permiten letras.</div>');
                return false;
            } else {
                jQuery(this).next("div").remove();
                jQuery(this).css("border", "");
                jQuery(this).css("background", "");
            }
            break;
    }
});




jQuery(document).ready(function () {
    var maxlen = 6;
    $('.validate-longitud').keypress(function (k) {
        if ($(this).val().length > maxlen) {
            k.preventDefault();
            jQuery(this).after('<div class="validation-custom validation-advice" id="advice-required-entry-' + name + '">Caracteres máximos 7.</div>');

        }

    });
});

jQuery(document).ready(function () {
    var maxlen = 13;
    $('.validate-longitudd').keypress(function (k) {
        if ($(this).val().length > maxlen) {
            k.preventDefault();
            jQuery(this).after('<div class="validation-custom validation-advice" id="advice-required-entry-' + name + '">Caracteres máximos 14.</div>');

        }
    });
});




//jQuery(document).click(function () {
//    if ($(".validar-email").val().indexOf('@', 0) == -1 || $(".validar-email").val().indexOf('.', 0) == -1) {
//        jQuery(this).after('<div class="validation-custom validation-advice" id="advice-required-entry-' + name + '">email.</div>');
//        return false;
//    }
//    return false;
//});


jQuery(document).click(function () {
    //Utilizamos una expresion regular 
    var texto = $('.validar-email').val();
    var reg = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    //Se utiliza la funcion test() nativa de JavaScript 
    if (reg.test(texto)) {

        document.getElementById("resultado").innerHTML = 'Resultado <br>' + texto + ' Email valido';
    } else {
        document.getElementById("resultado").innerHTML = '<br>Email no valido';
    }


});
