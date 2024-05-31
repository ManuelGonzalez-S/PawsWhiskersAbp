$(async function () {
    var l = abp.localization.getResource('Cesta');

    function renderCards(data) {
        var cardContainer = $('#cardContainer');
        cardContainer.empty();

        data.items.forEach(function (record) {
            var card = `
                <div class="col-md-4">
                    <div class="card mb-4">
                        <div class="card-body cardProducto">
                            <img class="imagenProducto" src="data:image/png;base64,${record.imageBase64}" class="card-img-top" alt="${record.name}">
                            <h5 class="card-title">${record.name}</h5>
                            <p class="card-text">Descripcion: ${record.description}</p>
                            <p class="card-text">Para: ${l('Enum:MascotaType.' + record.mascotaType)}</p>
                            <p class="card-text">Tipo: ${l('Enum:ProductoType.' + record.productoType)}</p>
                            <p class="card-text">Precio: ${record.price}&euro;</p>
                            <button class="btnAgregarCesta"> <i class="fas fa-shopping-basket"></i> Agregar a la cesta</button>
                        </div>
                    </div>
                </div>
            `;
            cardContainer.append(card);
        });
    }

    async function loadCards() { // Hacemos loadCards asíncrona
        var params = {
            skipCount: 0,
            maxResultCount: 10,
            sorting: 'name asc'
        };

        // Muestra el GIF de carga
        $('#loadingGif').show();
        $('#btn-notification').hide();

        // Espera de 2 segundos (2000 ms)
        await new Promise(resolve => setTimeout(resolve, 2500));

        cesta.productos.producto.getList(params).then(function (result) {
            renderCards(result);
            // Oculta el GIF de carga
            $('#loadingGif').hide();
            $('#btn-notification').show();
        });
    }

    await loadCards();


    $(document).on('click', '.btnAgregarCesta', function () {
        var cantidadElement = document.getElementById('notification-count');
        var cantidadPopUp = document.getElementById('notification-popup');

        // Obtener el valor actual de la cantidad
        var cantidad = parseInt(cantidadElement.textContent);

        // Alternar clase para cambiar estilo
        $(this).toggleClass('active');

        // Cambiar el texto del botón y la cantidad de elementos según el estado del botón
        if ($(this).hasClass('active')) {
            // Si el botón está activo, sumar uno a la cantidad
            cantidad++;
            $(this).html('<i class="fas fa-check"></i> Agregado');
        } else {
            // Si el botón no está activo, restar uno a la cantidad
            cantidad--;
            $(this).html('<i class="fas fa-shopping-basket"></i> Agregar a la cesta');
        }

        // Actualizar el texto mostrando la nueva cantidad
        cantidadElement.textContent = cantidad;
        cantidadPopUp.textContent = cantidad;
    });

    
    $(document).on('click', '#btn-notification', function () {
        var popup = $('#notification-popup');

        // Muestra la ventana emergente y anima su aparición
        popup.stop().slideDown();

        // Oculta la ventana emergente después de 2 segundos (2000 ms)
        setTimeout(function () {
            popup.stop().slideUp();
        }, 2000);
    });




});
