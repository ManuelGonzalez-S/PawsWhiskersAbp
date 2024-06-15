$(document).ready(async function () {
    // Muestra el GIF de carga y oculta la lista de productos al cargar la página
    $('#loadingGif').show();
    $('#listaProductos').hide();
    $('#btn-notification').hide();

    // Espera hasta que los productos estén cargados en el modelo
    await waitForProductsToLoad();

    await new Promise(resolve => setTimeout(resolve, 2000));

    // Oculta el GIF de carga y muestra la lista de productos una vez que se hayan cargado
    $('#loadingGif').hide();
    $('#listaProductos').show();
    $('#btn-notification').show();
});

// Función para esperar hasta que los productos estén cargados en el modelo
function waitForProductsToLoad() {
    return new Promise((resolve) => {
        const checkProductsLoaded = () => {
            if (document.getElementById('listaProductos').children.length > 0) {
                resolve();
            } else {
                setTimeout(checkProductsLoaded, 100);
            }
        };
        checkProductsLoaded();
    });
}

function modificarCantidadCarrito(element, idProducto) {
    var cantidadCarrito = document.getElementById('notification-count');
    var cantidad = parseInt(cantidadCarrito.textContent);

    $(element).toggleClass('active');

    if ($(element).hasClass('active')) {
        cantidad++;
        abp.libs.datatables.createAjax(cesta.pedidos.pedido.createAsyncGuidProducto(idProducto));
        $(element).html('<i class="fas fa-check"></i> Agregado');
    } else {
        cantidad--;
        abp.libs.datatables.createAjax(cesta.pedidos.pedido.deleteByProductoUserId(idProducto));
        $(element).html('<i class="fa-solid fa-cart-arrow-down"></i> Agregar a la cesta');
    }

    cantidadCarrito.textContent = cantidad;
}
