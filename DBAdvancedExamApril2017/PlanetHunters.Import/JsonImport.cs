namespace PlanetHunters.Import
{
    using Data.DbManipulation;
    using Data.DTOs;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public class JsonImport
    {
        private const string astronomersDataPath = "../../PlanetHuntersDb-Data/astronomers.json";
        private const string planetsDataPath = "../../PlanetHuntersDb-Data/planets.json";
        private const string telescopesDataPath = "../../PlanetHuntersDb-Data/telescopes.json";

        public static void ImportAstronomers()
        {
            string astronomersJson = File.ReadAllText(astronomersDataPath);
            var astronomers = JsonConvert.DeserializeObject<IEnumerable<AstronomerDto>>(astronomersJson);
            AstronomerStore.AddAstronomers(astronomers);
        }

        public static void ImportTelescopes()
        {
            string telescopesJson = File.ReadAllText(telescopesDataPath);
            var telescopes = JsonConvert.DeserializeObject<IEnumerable<TelescopeDto>>(telescopesJson);
            TelescopeStore.AddTelescopes(telescopes);
        }

        public static void ImportPlanets()
        {
            string planetsJson = File.ReadAllText(planetsDataPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDto>>(planetsJson);
            PlanetStore.AddPlanets(planets);
        }
    }
}