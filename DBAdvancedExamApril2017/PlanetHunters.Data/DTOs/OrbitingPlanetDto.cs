namespace PlanetHunters.Data.DTOs
{
    using System.Collections.Generic;

    public class OrbitingPlanetDto
    {
        public string Name { get; set; }

        public decimal Mass { get; set; }

        public ICollection<string> Orbiting { get; set; }
    }
}