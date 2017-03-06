namespace _10.HospitalDatabaseModification.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Visitation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateOfVisitation { get; set; }

        public string Comments { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}