using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SunnyPlace_ASP.NETCore.Models;
using SunnyPlace_ASP.NETCore;
using Vector2 = System.Numerics.Vector2;

namespace SunnyPlace.Pages
{
    public class MapModel : PageModel
    {
        public string ShadowCoordinatesJson { get; set; }

        public void OnGet()
        {
            var buildingVertices = new List<Vector2>
            {
                // Add your building's vertices here
            };

            var buildingHeight = 10.0f; // Replace with your building's height
            var currentTime = DateTime.Now;

            var shadowCalculator = new BuildingShadowCalculator();
            var shadowPositions = shadowCalculator.CalculateShadow(buildingVertices, buildingHeight, currentTime);

            ShadowCoordinatesJson = JsonConvert.SerializeObject(shadowPositions);
        }
    }
}

