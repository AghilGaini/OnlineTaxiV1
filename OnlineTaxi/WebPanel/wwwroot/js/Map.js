﻿var currentLat = 35.78023853564178;
var currentLng = 51.46740881600891;
var firstTime = true;
var secondTime = true;
var firstlatlng = {};
var secondlatlng = {};
var markerArray = new Array();

var map = L.map('map', {
    center: [currentLat, currentLng],
    minZoom: 10,
    maxZoom: 18,
    zoom: 17
});

var tile = L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png',
    { attribution: "&copy;عن تو این مملکت" }).addTo(map);

var circle = L.circle([currentLat, currentLng], {
    fillOpacity: 0.1,
    radius: 15
}).addTo(map);

var popup = L.popup();

var myIcon = L.Icon.extend({
    options: {
        iconSize: [40, 40]
    }
});

var firstIcon = new myIcon({ iconUrl: '/img/mapFirst.ico' });
var secondIcon = new myIcon({ iconUrl: '/img/mapSecond.ico' });

function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent('you clicked : ' + e.latlng.toString())
        .openOn(map);
    setMarker(e.latlng);
}

function setMarker(latlng) {

    if (firstTime == true) {
        firstlatlng = latlng;
        marker = L.marker([latlng.lat, latlng.lng], { icon: firstIcon, title: "مبدا" });
        firstTime = false;
    }
    else {
        secondlatlng = latlng;
        if (secondTime == false) {
            var remove = markerArray.pop();
            map.removeLayer(remove);
        }
        marker = L.marker([latlng.lat, latlng.lng], { icon: secondIcon, title: "مقصد" });
        secondTime = false;
    }
    map.addLayer(marker);
    markerArray.push(marker);
}

function clearLastMarker() {
    if (markerArray.length <= 0)
        return;

    var remove = markerArray.pop();
    map.removeLayer(remove);

    if (secondTime == false) {
        secondTime = true;
        secondlatlng = {};
    }
    else {
        firstTime = true;
        firstlatlng = {};
    }
}

function clearMarkers() {
    while (markerArray.length > 0) {
        var remove = markerArray.pop();
        map.removeLayer(remove);
    }

    firstTime = true;
    secondTime = true;
    firstlatlng = {};
    secondlatlng = {};
}


map.on('click', onMapClick);