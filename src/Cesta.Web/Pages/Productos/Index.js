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
                            <p class="card-text">Para: ${l('Enum:MascotaType.' + record.mascotaType)}</p>
                            <p class="card-text">Tipo: ${l('Enum:ProductoType.' + record.productoType)}</p>
                            <p class="card-text">Precio: ${record.price}€</p>
                            <button class="btnAgregarCesta" onclick="sumarNotificationCount(this)"> <i class="fas fa-shopping-basket"></i> Agregar a la cesta</button>
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

        // Espera de 2 segundos (2000 ms)
        await new Promise(resolve => setTimeout(resolve, 10000));

        cesta.productos.producto.getList(params).then(function (result) {
            renderCards(result);
            // Oculta el GIF de carga
            $('#loadingGif').hide();
        });
    }

    await loadCards(); // Usa await para esperar a que loadCards se complete

    // Simula la actualización del número en el botón
    function updateNotificationCount(count) {
        const notificationCount = document.getElementById('notification-count');
        notificationCount.textContent = count;
    }

    // Llama a la función para actualizar el número (ejemplo: 5)
    updateNotificationCount(5);

    sumarNotificationCount();

    function sumarNotificationCount(boton) {
        var cantidadElement = document.getElementById('notification-count');

        console.log(cantidadElement);

        // Obtener el valor actual de la cantidad
        var cantidad = parseInt(cantidadElement.textContent);
        // Sumar uno a la cantidad
        cantidad++;
        // Actualizar el texto mostrando la nueva cantidad
        cantidadElement.textContent = cantidad;
    }

    function restarNotificationCount(boton) {
        var cantidadElement = document.getElementById('notification-count');
        // Obtener el valor actual de la cantidad
        var cantidad = parseInt(cantidadElement.textContent);
        // Verificar que la cantidad sea mayor que cero antes de restar
        if (cantidad > 0) {
            // Restar uno a la cantidad
            cantidad--;
            // Actualizar el texto mostrando la nueva cantidad
            cantidadElement.textContent = cantidad;
        }
    }

    const myButton = document.getElementById('btn-notification');
    myButton.addEventListener('click', () => {
        alert('Button clicked!');
        // Aquí puedes añadir la función que deseas ejecutar
    });

    // Asegúrate de que los botones sean seleccionados después de que las tarjetas se hayan renderizado
    $(document).on('click', '.btnAgregarCesta', function () {
        var cantidadElement = document.getElementById('notification-count');

        console.log(cantidadElement);

        // Obtener el valor actual de la cantidad
        var cantidad = parseInt(cantidadElement.textContent);
        // Sumar uno a la cantidad
        cantidad++;
        // Actualizar el texto mostrando la nueva cantidad
        cantidadElement.textContent = cantidad;

        // Alternar clase para cambiar estilo
        $(this).toggleClass('active');

        // Cambiar el texto del botón (opcional)
        if ($(this).hasClass('active')) {
            $(this).html('<i class="fas fa-check"></i> Agregado');
        } else {
            $(this).html('<i class="fas fa-shopping-basket"></i> Agregar a la cesta');
        }
    });
});
