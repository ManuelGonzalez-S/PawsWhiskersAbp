$(function () {
    abp.log.debug('Index.js initialized!');

    var map = L.map('map').setView([40.40011, -3.6950], 13);

    L.control.scale().addTo(map);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map);


    var marker = L.marker([40.402329, -3.698598]).addTo(map);

    var circle = L.circle([51.508, -0.11], {
        color: 'red',
        fillColor: '#f03',
        fillOpacity: 0.5,
        radius: 500
    }).addTo(map);


    var polygon = L.polygon([
        [51.509, -0.08],
        [51.503, -0.06],
        [51.51, -0.047]
    ]).addTo(map);


    marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();
    circle.bindPopup("I am a circle.");
    polygon.bindPopup("I am a polygon.");

    var popup = L.popup();

    function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("You clicked the map at " + e.latlng.toString())
            .openOn(map);
    }

    map.on('click', onMapClick);






    var LeafIcon = L.Icon.extend({
        options: {
            shadowUrl: 'leaf-shadow.png',
            iconSize: [38, 95],
            shadowSize: [50, 64],
            iconAnchor: [22, 94],
            shadowAnchor: [4, 62],
            popupAnchor: [-3, -76]
        }
    });

    L.marker([51.5, -0.09], { icon: greenIcon }).addTo(map);

    L.marker([51.5, -0.09], { icon: greenIcon }).addTo(map).bindPopup("I am a green leaf.");
    L.marker([51.495, -0.083], { icon: redIcon }).addTo(map).bindPopup("I am a red leaf.");
    L.marker([51.49, -0.1], { icon: orangeIcon }).addTo(map).bindPopup("I am an orange leaf.");



    

});
