namespace PlanetHunters.Data.DbManipulation
{
    using DTOs;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StarStore
    {
        public static void AddStars(IEnumerable<StarDto> stars)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var star in stars)
                {
                    if (star.Name == null ||
                        star.StarSystem == null ||
                        star.Name.Length > Star.NameMaxLength ||
                        star.StarSystem.Length > StarSystem.NameMaxLength ||
                        star.Temperature < Star.TemperatureMinValue)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var starSystem = context.StarSystems.FirstOrDefault(s => s.Name == star.StarSystem);
                        if (starSystem == null)
                        {
                            starSystem = new StarSystem()
                            {
                                Name = star.StarSystem
                            };

                            context.StarSystems.Add(starSystem);
                            context.SaveChanges();
                        }

                        Star newStar = new Star()
                        {
                            Name = star.Name,
                            Temperature = star.Temperature,
                            HostStarSystemId = starSystem.Id
                        };

                        context.Stars.Add(newStar);
                        Console.WriteLine($"Record {star.Name} successfully imported.");
                    }
                }

                context.SaveChanges();
            }
        }

        public static Star GetStarByName(string name, PlanetHuntersContext context)
        {
            return context.Stars.FirstOrDefault(s => s.Name == name);
        }

        public static ICollection<Star> GetStars()
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Stars.ToList();
            }
        }
    }
}