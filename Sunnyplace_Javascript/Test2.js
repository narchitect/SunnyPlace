const map = L.map("map").setView([48.1351, 11.5820], 18);

L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
  attribution:
    '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
}).addTomap;

function fetchBuildingFootprint(lat, lng) {
  const overpassApiUrl = "https://overpass-api.de/api/interpreter";
  const query = `[out:json];way[building](around:20,${lat},${lng});out geom;`;

  fetch(overpassApiUrl, {
    method: "POST",
    body: `data=${encodeURIComponent(query)}`,
    headers: { "Content-Type": "application/x-www-form-urlencoded" },
  })
    .then((response) => response.json())
    .then((data) => {
      const buildingFootprints = data.elements.map((element) => {
        return element.geometry.map((point) => {
          return [point.lat, point.lon];
        });
      });

      displayBuildingFootprints(buildingFootprints);
    });
}

function displayBuildingFootprints(footprints) {
  footprints.forEach((footprint) => {
    const buildingFootprint = L.polygon(footprint, { color: "blue" }).addTo(map);
    // Here, you can call your shadow calculation method and display the shadow on the map
  });
}

function calculateShadow(buildingVertices, buildingHeight, latitude, longitude, time) {
    const sunPosition = SunCalc.getPosition(time, latitude, longitude);
    const shadowLength = buildingHeight / Math.tan(sunPosition.altitude);
    const shadowOffset = {
      x: shadowLength * Math.sin(sunPosition.azimuth),
      y: shadowLength * Math.cos(sunPosition.azimuth),
    };
  
    return buildingVertices.map((vertex) => {
      return {
        x: vertex.x + shadowOffset.x,
        y: vertex.y + shadowOffset.y,
      };
    });
  }
 
  function displayBuildingFootprints(footprints) {
    footprints.forEach((footprint) => {
      const buildingFootprint = L.polygon(footprint, { color: "blue" }).addTo(map);
  
      // Use an estimated building height or find an alternative source for building heights
      const buildingHeight = 20;
  
      // Replace these with the actual latitude and longitude of the building
      const latitude = 48.1351;
      const longitude = 11.5820;
  
      const currentTime = new Date();
      const shadowVertices = calculateShadow(footprint, buildingHeight, latitude, longitude, currentTime);
  
      const shadowPolygon = L.polygon(shadowVertices, { color: "gray", opacity: 0.5 }).addTo(map);
    });
  }
  
map.on("click", (e) => {
  const { lat, lng } = e.latlng;
  fetchBuildingFootprint(lat, lng);
});
