namespace PlanetHunters.Data.DbManipulation
{
    using DTOs;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PlanetStore
    {
        public static void AddPlanets(IEnumerable<PlanetDto> planets)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var planet in planets)
                {
                    if (planet.Name == null ||
                        planet.StarSystem == null ||
                        planet.Name.Length > Planet.NameMaxLength ||
                        planet.Mass <= Planet.MassMinSize ||
                        planet.StarSystem.Length > StarSystem.NameMaxLength)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var starSystem = context.StarSystems.FirstOrDefault(s => s.Name == planet.StarSystem);
                        if (starSystem == null)
                        {
                            starSystem = new StarSystem()
                            {
                                Name = planet.StarSystem
                            };

                            context.StarSystems.Add(starSystem);
                            context.SaveChanges();
                        }

                        Planet newPlanet = new Planet()
                        {
                            Name = planet.Name,
                            Mass = planet.Mass,
                            HostStarSystemId = starSystem.Id
                        };

                        context.Planets.Add(newPlanet);
                        Console.WriteLine($"Record {planet.Name} successfully imported.");
                    }
                }

                context.SaveChanges();
            }
        }

        public static Planet GetPlanetByName(string name, PlanetHuntersContext context)
        {
            return context.Planets.FirstOrDefault(p => p.Name == name);
        }

        public static ICollection<OrbitingPlanetDto> GetPlanetsByTelescope(string telescope)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Telescopes
                    .SelectMany(t => t.Discoveries)
                    .SelectMany(d => d.Planets)
                    .Select(p => new OrbitingPlanetDto()
                    {
                        Name = p.Name,
                        Mass = p.Mass,
                        Orbiting = new List<string>()
                        {
                            p.HostStarSystem.Name
                        }
                    })
                    .OrderByDescending(p => p.Mass)
                    .ToList();
            }
        }
    }
}