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
