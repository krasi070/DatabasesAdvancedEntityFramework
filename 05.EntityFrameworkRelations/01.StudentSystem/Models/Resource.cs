namespace _01.StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Resource
    {
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(video|presentation|document|other)$")]
        public string Type { get; set; }

        [Required]
        public string Url { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
    }
}