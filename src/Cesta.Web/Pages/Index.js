$(function () {
    abp.log.debug('Index.js initialized!');

    var map = L.map('map').setView([40.40011, -3.6950], 13);

    L.control.scale().addTo(map);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);

    var mapIcon = L.Icon.extend({
        options: {
            iconUrl: '/Pages/Leaflet/images/store.png',
            shadowUrl: '/pages/Leaflet/images/marker-shadow.png',
            iconsize: [1, 1],
            shadowSize: [1, 1],
            iconAnchor: [1, 1],
            popupAnchor: [1, 1]
        }
    });


    var mapIcon = L.Icon.extend({
        options: {
            shadowUrl: '/pages/Leaflet/images/marker-shadow.png',
            iconsize: [1, 1],
            shadowSize: [1, 1],
            iconAnchor: [1, 1],
            popupAnchor: [0, 0]

        }
    });

    var tiendaIcon = new mapIcon({ iconUrl: '/Pages/Leaflet/images/store.png' });

    L.marker([40.402329, -3.698598], { icon: tiendaIcon }).addTo(map).bindPopup("Tienda IMF");
    L.marker([40.406859, -3.688683], { icon: tiendaIcon }).addTo(map).bindPopup("Tienda Atocha");
    L.marker([40.41445, -3.683776], { icon: tiendaIcon }).addTo(map).bindPopup("Tienda Retiro");
   

    function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("You clicked the map at " + e.latlng.toString())
            .openOn(map);
    }

    map.on('click', onMapClick);

});

function sendEmail() {
    window.location.href = "mailto:pawswhiskers69@gmail.com";
}

function showWarningAlert(message) {
    Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: message,
    });
}

function showErrorAlert(message) {
    Swal.fire({
        icon: 'error',
        title: 'Error',
        text: message,
    });
}

function showSuccessAlert(message) {
    Swal.fire({
        icon: 'success',
        title: 'Success',
        text: message,
    });
}

function showLoadingAlert(message, duration = null) {
    const swalParams = {
        title: 'Cargando...',
        text: message,
        allowOutsideClick: false,
        allowEscapeKey: false,
        didOpen: () => {
            Swal.showLoading();
        }
    };

    Swal.fire(swalParams);

    if (duration !== null) {
        setTimeout(() => {
            Swal.close();
        }, duration);
    }
}
