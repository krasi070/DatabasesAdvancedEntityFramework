namespace PlanetHunters.Data.DbManipulation
{
    using DTOs;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TelescopeStore
    {
        public static void AddTelescopes(IEnumerable<TelescopeDto> telescopes)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var telescope in telescopes)
                {
                    if (telescope.Name == null ||
                        telescope.Location == null ||
                        telescope.Name.Length > Telescope.NameMaxLength ||
                        telescope.Location.Length > Telescope.LocationMaxLength ||
                        (telescope.MirrorDiameter <= Telescope.MirrorDiameterMinSize && telescope.MirrorDiameter != null))
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        Telescope newTelescope = new Telescope()
                        {
                            Name = telescope.Name,
                            Location = telescope.Location,
                            MirrorDiameter = telescope.MirrorDiameter
                        };

                        context.Telescopes.Add(newTelescope);
                        Console.WriteLine($"Record {telescope.Name} successfully imported.");
                    }
                }

                context.SaveChanges();
            }
        }

        public static Telescope GetTelescopeByName(string name, PlanetHuntersContext context)
        {
            return context.Telescopes.FirstOrDefault(t => t.Name == name);
        }
    }
}