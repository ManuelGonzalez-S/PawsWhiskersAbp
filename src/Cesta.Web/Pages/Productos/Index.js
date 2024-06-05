$(async function () {
    var l = abp.localization.getResource('Cesta');


    var listaProductos = document.getElementById('listaProductos');

    console.log("Antes");
    console.log(listaProductos);
    console.log("Despues");

    // Muestra el GIF de carga
    $('#loadingGif').show();
    $('#btn-notification').hide();
    listaProductos.style.display = "none";

    // Espera de 2 segundos (2000 ms)
    await new Promise(resolve => setTimeout(resolve, 2500));

    //Oculta el gif de carga
    $('#loadingGif').hide();
    $('#btn-notification').show();
    listaProductos.style.display = "block";

});

function modificarCantidadCarrito(element){

    var cantidadCarrito = document.getElementById('notification-count');

    var cantidad = parseInt(cantidadCarrito.textContent);

    $(element).toggleClass('active');

    if ($(element).hasClass('active')) {

        cantidad++;

        $(element).html('<i class="fas fa-check"></i> Agregado');


    } else {

        cantidad--;

        $(element).html('<i class="fas fa-shopping-basket"></i> Agregar a la cesta');

    }

    cantidadCarrito.textContent = cantidad;

}