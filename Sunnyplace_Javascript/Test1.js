function initMap() {
  // Define the coordinates of the building footprint and the shaded footprint
  var buildingFootprintCoords = [
    {lat: 37.7749, lng: -122.4194},
    {lat: 37.7749, lng: -122.4189},
    {lat: 37.7754, lng: -122.4189},
    {lat: 37.7754, lng: -122.4194}
  ];
  var shadedFootprintCoords = [
    {lat: 37.7746, lng: -122.4196},
    {lat: 37.7746, lng: -122.4188},
    {lat: 37.7753, lng: -122.4188},
    {lat: 37.7753, lng: -122.4196}
  ];

  // Create the map and add the building footprint and shaded footprint polygons
  var map = new google.maps.Map(document.getElementById('map'), {
    center: {lat: 37.7749, lng: -122.4194},
    zoom: 18
  });
  var buildingFootprintPolygon = new google.maps.Polygon({
    paths: buildingFootprintCoords,
    strokeColor: '#FF0000',
    strokeOpacity: 0.8,
    strokeWeight: 2,
    fillColor: '#FF0000',
    fillOpacity: 0.35
  });
  buildingFootprintPolygon.setMap(map);
  var shadedFootprintPolygon = new google.maps.Polygon({
    paths: shadedFootprintCoords,
    strokeColor: '#00FF00',
    strokeOpacity: 0.8,
    strokeWeight: 2,
    fillColor: '#00FF00',
    fillOpacity: 0.35
  });
    shadedFootprintPolygon.setMap(map);
    %.ajax call method. 
}
