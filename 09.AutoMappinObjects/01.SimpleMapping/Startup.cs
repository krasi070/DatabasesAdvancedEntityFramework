namespace _01.SimpleMapping
{
    using System;
    using AutoMapper;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());

            Employee employee = new Employee()
            {
                FirstName = "Josh",
                LastName = "Blackwater",
                Salary = 23000.00m,
                Birthday = new DateTime(1972, 7, 12),
                Address = "Fake str. No 11"
            };

            EmployeeDto employeeDto = Mapper.Map<EmployeeDto>(employee);
            Console.WriteLine($"{employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary:F2}$");
        }
    }
}