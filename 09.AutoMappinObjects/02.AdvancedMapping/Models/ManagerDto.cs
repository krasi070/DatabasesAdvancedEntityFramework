namespace _02.AdvancedMapping.Models
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> Subordinates { get; set; }

        public int CountOfSubordinates { get; set; }
    }
}