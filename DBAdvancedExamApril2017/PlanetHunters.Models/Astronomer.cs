namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Astronomer
    {
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;

        public Astronomer()
        {
            this.PioneeringDiscoveries = new HashSet<Discovery>();
            this.ObservedDiscoveries = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength)]
        public string LastName { get; set; }

        public virtual ICollection<Discovery> PioneeringDiscoveries { get; set; }

        public virtual ICollection<Discovery> ObservedDiscoveries { get; set; }
    }
}