namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StarSystem
    {
        public const int NameMaxLength = 255;

        public StarSystem()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        public ICollection<Star> Stars { get; set; }

        public ICollection<Planet> Planets { get; set; }
    }
}