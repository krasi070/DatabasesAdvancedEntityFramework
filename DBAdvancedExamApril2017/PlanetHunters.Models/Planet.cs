namespace PlanetHunters.Models
{
    using Attributes;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public const int NameMaxLength = 255;
        public const int MassMinSize = 0;

        public Planet()
        {
            this.DiscoveriesIncludedIn = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [CustomValidation(typeof(PositiveAttribute), "IsValuePositive")]
        public decimal Mass { get; set; }

        public int HostStarSystemId { get; set; }

        public virtual StarSystem HostStarSystem { get; set; }

        public virtual ICollection<Discovery> DiscoveriesIncludedIn { get; set; }
    }
}