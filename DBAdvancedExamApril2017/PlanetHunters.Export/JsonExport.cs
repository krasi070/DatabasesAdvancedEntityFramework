namespace PlanetHunters.Export
{
    using Data.DbManipulation;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonExport
    {
        public static void ExportPlanets(string telescope)
        {
            string planetsJson = JsonConvert.SerializeObject(PlanetStore.GetPlanetsByTelescope(telescope), Formatting.Indented);
            Console.WriteLine(planetsJson);
            File.WriteAllText($"../../exported-data/planets-by-{telescope}.json", planetsJson);
        }
    }
}