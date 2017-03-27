namespace _02.AdvancedMapping
{
    using System;
    using System.Linq;
    using Models;
    using AutoMapper;
    using System.Collections.Generic;

    public class Startup
    {
        static void Main()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, ManagerDto>()
                    .ForMember(dto => dto.CountOfSubordinates, 
                    opt => opt.MapFrom(src => src.Subordinates.Count))
                    .ForMember(dto => dto.Subordinates,
                    opt => opt.MapFrom(src => src.Subordinates
                        .Select(s => new EmployeeDto()
                        {
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            Salary = s.Salary
                        })));
            });

            var managerDtos = Mapper.Map<List<Employee>, List<ManagerDto>>(GetTestEmployees());
            foreach (var managerDto in managerDtos)
            {
                Console.WriteLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.CountOfSubordinates}");
                foreach (var employeeDto in managerDto.Subordinates)
                {
                    Console.WriteLine($"- {employeeDto.FirstName} {employeeDto.LastName} {employeeDto.Salary:F2}");
                }
            }
        }

        private static List<Employee> GetTestEmployees()
        {
            Employee manager1 = new Employee()
            {
                FirstName = "Mike",
                LastName = "Lawson",
                Salary = 100234m,
                Birthday = new DateTime(1970, 11, 1),
                IsOnHoliday = false,
                Address = "Not real str. 1"
            };

            Employee manager2 = new Employee()
            {
                FirstName = "Bob",
                LastName = "Flanders",
                Salary = 50234m,
                Birthday = new DateTime(1975, 7, 6),
                IsOnHoliday = false,
                Address = "Not real str. 10"
            };

            Employee employee1 = new Employee()
            {
                FirstName = "John1",
                LastName = "Simpson1",
                Salary = 12123.00m,
                Birthday = new DateTime(1980, 12, 1),
                IsOnHoliday = false,
                Address = "Not real str. 2",
                Manager = manager1
            };

            Employee employee2 = new Employee()
            {
                FirstName = "John2",
                LastName = "Simpson2",
                Salary = 12123.00m,
                Birthday = new DateTime(1980, 12, 1),
                IsOnHoliday = true,
                Address = "Not real str. 3",
                Manager = manager2
            };

            Employee employee3 = new Employee()
            {
                FirstName = "John3",
                LastName = "Simpson3",
                Salary = 12123.00m,
                Birthday = new DateTime(1980, 12, 1),
                IsOnHoliday = false,
                Address = "Not real str. 4",
                Manager = manager1
            };

            manager1.Subordinates.Add(employee1);
            manager1.Subordinates.Add(employee3);
            manager2.Subordinates.Add(employee2);

            return new List<Employee>()
            {
                employee1,
                employee2,
                employee3,
                manager1,
                manager2
            };
        }
    }
}