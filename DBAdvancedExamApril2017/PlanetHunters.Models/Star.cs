namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Star
    {
        public const int NameMaxLength = 255;
        public const int TemperatureMinValue = 2400;

        public Star()
        {
            this.DiscoveriesIncludedIn = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Range(TemperatureMinValue, int.MaxValue)]
        public int Temperature { get; set; }

        public int HostStarSystemId { get; set; }

        public virtual StarSystem HostStarSystem { get; set; }

        public virtual ICollection<Discovery> DiscoveriesIncludedIn { get; set; }
    }
}