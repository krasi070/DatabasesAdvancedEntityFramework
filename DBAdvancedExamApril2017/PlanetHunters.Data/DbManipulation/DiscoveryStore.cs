namespace PlanetHunters.Data.DbManipulation
{
    using DTOs;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DiscoveryStore
    {
        public static void AddDiscoveries(IEnumerable<DiscoveryDto> discoveries)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var discovery in discoveries)
                {
                    // 1753 - datetime in SQL can only hold years after 1753
                    if (discovery.DateMade == null ||
                        discovery.DateMade.Year < 1753)
                    {
                        Console.WriteLine("Invalid data format!");
                    }
                    else
                    {
                        var telescope = TelescopeStore.GetTelescopeByName(discovery.Telescope, context);
                        if (telescope == null)
                        {
                            Console.WriteLine("Invalid data format!");
                            continue;
                        }

                        var stars = GetStars(discovery, context);
                        var planets = GetPlanets(discovery, context);
                        var pioneers = GetAstronomes(discovery.Pioneers, context);
                        var observers = GetAstronomes(discovery.Observers, context);

                        Discovery newDiscovery = new Discovery()
                        {
                            DateMade = discovery.DateMade,
                            TelescopeUsedId = telescope.Id,
                            Stars = stars,
                            Planets = planets,
                            Pioneers = pioneers,
                            Observers = observers
                        };

                        context.Discoveries.Add(newDiscovery);
                        int year = discovery.DateMade.Year;
                        string month = discovery.DateMade.Month.ToString().PadLeft(2, '0');
                        string day = discovery.DateMade.Day.ToString().PadLeft(2, '0');
                        Console.WriteLine($"Discovery ({year}/{month}/{day}-{discovery.Telescope}) with " +
                            $"{discovery.Stars.Count} star(s), {discovery.Planets.Count} planet(s), {discovery.Pioneers.Count} pioneer(s) and " +
                            $"{discovery.Observers.Count} observer(s) successfully imported.");
                    }
                }

                context.SaveChanges();
            }
        }

        private static ICollection<Star> GetStars(DiscoveryDto discovery, PlanetHuntersContext context)
        {
            var stars = new HashSet<Star>();
            foreach (var star in discovery.Stars)
            {
                var currStar = StarStore.GetStarByName(star, context);
                if (currStar != null)
                {
                    stars.Add(currStar);
                }
            }

            return stars;
        }

        private static ICollection<Planet> GetPlanets(DiscoveryDto discovery, PlanetHuntersContext context)
        {
            var planets = new HashSet<Planet>();
            foreach (var planet in discovery.Planets)
            {
                var currPlanet = PlanetStore.GetPlanetByName(planet, context);
                if (currPlanet != null)
                {
                    planets.Add(currPlanet);
                }
            }

            return planets;
        }

        private static ICollection<Astronomer> GetAstronomes(ICollection<AstronomerDto> astronomerDtos, PlanetHuntersContext context)
        {
            var astronomers = new HashSet<Astronomer>();
            foreach (var astronomer in astronomerDtos)
            {
                var currAstronomer = AstronomerStore.GetAstronomerByFirstAndLastName(astronomer.FirstName, astronomer.LastName, context);
                if (currAstronomer != null)
                {
                    astronomers.Add(currAstronomer);
                }
            }

            return astronomers;
        }
    }
}