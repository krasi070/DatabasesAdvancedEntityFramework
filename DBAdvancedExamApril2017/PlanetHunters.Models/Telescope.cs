namespace PlanetHunters.Models
{
    using Attributes;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Telescope
    {
        public const int NameMaxLength = 255;
        public const int LocationMaxLength = 255;
        public const int MirrorDiameterMinSize = 0;

        public Telescope()
        {
            this.Discoveries = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(LocationMaxLength)]
        public string Location { get; set; }

        [CustomValidation(typeof(PositiveAttribute), "IsValuePositive")]
        public decimal? MirrorDiameter { get; set; }

        public virtual ICollection<Discovery> Discoveries { get; set; }
    }
}