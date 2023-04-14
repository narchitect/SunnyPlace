using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SunnyPlace_ASP.NETCore.Models;
using Vector2 = System.Numerics.Vector2;

namespace SunnyPlace.Pages
{
    public class MapModel : PageModel
    {
        public string ShadowCoordinatesJson { get; set; }

        public void OnGet()
        {
            var buildingCoordinates = new List<Vector2>
            {
                new Vector2(48.15031f, 11.56683f),
                new Vector2(48.14836f, 11.5656f),
                new Vector2(48.14772f, 11.56798f),
                new Vector2(48.14959f, 11.56914f)
            };

            var buildingHeight = 10.0f; // Replace with your building's height
            var currentTime = new DateTime(2023, 4, 9, 12, 0, 0);

            var shadowCalculator = new BuildingShadowCalculator();
            var shadowPositions = shadowCalculator.CalculateShadow(buildingCoordinates, buildingHeight, currentTime);
            Console.WriteLine("Calculated shadow positions: {0}", JsonConvert.SerializeObject(shadowPositions));

            Debug.WriteLine("Shadow positions:");
            foreach (var position in shadowPositions)
            {
                Debug.WriteLine($"({position.X}, {position.Y})");
            }

            ShadowCoordinatesJson = JsonConvert.SerializeObject(shadowPositions);
        }
    }
}

