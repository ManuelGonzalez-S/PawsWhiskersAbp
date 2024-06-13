$(document).ready(async function () {
    // Muestra el GIF de carga y oculta la lista de productos al cargar la página
    $('#loadingGif').show();
    $('#listaPedidos').hide();
    $('#btn-comprar').hide();

    // Espera hasta que los productos estén cargados en el modelo
    await waitForOrdersToLoad();

    await new Promise(resolve => setTimeout(resolve, 2000));

    // Oculta el GIF de carga y muestra la lista de productos una vez que se hayan cargado
    $('#loadingGif').hide();
    $('#listaPedidos').show();
    $('#btn-comprar').show();
});

// Función para esperar hasta que los productos estén cargados en el modelo
function waitForOrdersToLoad() {
    return new Promise((resolve) => {
        const checkProductsLoaded = () => {
            if (document.getElementById('listaPedidos').children.length > 0) {
                resolve();
            } else {
                setTimeout(checkProductsLoaded, 100);
            }
        };
        checkProductsLoaded();
    });
}
