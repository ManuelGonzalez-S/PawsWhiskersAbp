$(function () {
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
                                    <button class="btnAgregarCesta"> <i class="fas fa-shopping-basket"></i> Agregar a la cesta</button>
                                </div>
                            </div>
                        </div>
                    `;
            cardContainer.append(card);
        });

    }

    function loadCards() {
        var params = {
            skipCount: 0,
            maxResultCount: 10,
            sorting: 'name asc'
        };

        cesta.productos.producto.getList(params).then(function (result) {
            renderCards(result);
        });
    }

    loadCards(); // Inicializa la carga de cards

    const myButton = document.getElementById('btn-notification');
    myButton.addEventListener('click', () => {
        alert('Button clicked!');
        // Aquí puedes añadir la función que deseas ejecutar
    });

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

        console.log(boton.id);
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

});