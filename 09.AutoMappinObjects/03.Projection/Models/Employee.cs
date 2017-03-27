namespace _03.Projection.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }

        public virtual Employee Manager { get; set; }
    }
}