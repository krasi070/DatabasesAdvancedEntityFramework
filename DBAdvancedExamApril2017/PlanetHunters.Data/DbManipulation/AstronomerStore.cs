namespace PlanetHunters.Data.DbManipulation
{
    using DTOs;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AstronomerStore
    {
        public static void AddAstronomers(IEnumerable<AstronomerDto> astronomers)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var astronomer in astronomers)
                {
                    if (astronomer.FirstName == null || 
                        astronomer.LastName == null ||
                        astronomer.FirstName.Length > Astronomer.FirstNameMaxLength ||
                        astronomer.LastName.Length > Astronomer.LastNameMaxLength)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        Astronomer newAstronomer = new Astronomer()
                        {
                            FirstName = astronomer.FirstName,
                            LastName = astronomer.LastName
                        };

                        context.Astronomers.Add(newAstronomer);
                        Console.WriteLine($"Record {astronomer.FirstName} {astronomer.LastName} successfully imported.");
                    }
                }

                context.SaveChanges();
            }
        }

        public static Astronomer GetAstronomerByFirstAndLastName(string firstName, string lastName, PlanetHuntersContext context)
        {
            return context.Astronomers.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);
        }

        //public static ICollection<AstronomerExportDto> GetAstronomers(string starSystem)
        //{
        //    using (var context = new PlanetHuntersContext())
        //    {
        //        var starPioneers = context.StarSystems
        //            .First(s => s.Name == starSystem)
        //            .Stars
        //            .SelectMany(s => s.DiscoveriesIncludedIn)
        //            .SelectMany(a => a.Pioneers)
        //            .Select(a => new AstronomerExportDto()
        //            {
        //                Name = a.FirstName + " " + a.LastName,
        //                Role = "pioneer"
        //            });

        //        var starObservers = context.StarSystems
        //            .First(s => s.Name == starSystem)
        //            .Stars
        //            .SelectMany(s => s.DiscoveriesIncludedIn)
        //            .SelectMany(a => a.Observers)
        //            .Select(a => new AstronomerExportDto()
        //            {
        //                Name = a.FirstName + " " + a.LastName,
        //                Role = "observer"
        //            });
                
        //        var planetPioneers = context.StarSystems
        //            .First(s => s.Name == starSystem)
        //            .Planets
        //            .SelectMany(s => s.DiscoveriesIncludedIn)
        //            .SelectMany(a => a.Pioneers)
        //            .Select(a => new AstronomerExportDto()
        //            {
        //                Name = a.FirstName + " " + a.LastName,
        //                Role = "pioneer"
        //            });

        //        var planetObservers = context.StarSystems
        //            .First(s => s.Name == starSystem)
        //            .Planets
        //            .SelectMany(s => s.DiscoveriesIncludedIn)
        //            .SelectMany(a => a.Observers)
        //            .Select(a => new AstronomerExportDto()
        //            {
        //                Name = a.FirstName + " " + a.LastName,
        //                Role = "observer"
        //            });

        //        var list = new List<AstronomerExportDto>();
        //        list.AddRange(starPioneers);
        //        list.AddRange(planetPioneers);
        //        list.AddRange(starObservers);
        //        list.AddRange(planetObservers);

        //        list = list
        //            .Where(a => list.Where(a1 => a1.Name == a.Name).Count() > 1)
        //            .
        //            .ToList();
        //    }
        //}
    }
}