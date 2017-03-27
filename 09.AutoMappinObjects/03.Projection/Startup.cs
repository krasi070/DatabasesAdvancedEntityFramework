namespace _03.Projection
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>()
                    .ForMember(dto => dto.ManagerLastName,
                    opt => opt.MapFrom(src => src.Manager.LastName));
            });

            EmployeesContext context = new EmployeesContext();
            context.Database.Initialize(true);
            
            var employeeDtos = context.Employees
                .Where(e => e.Birthday.Year < 1990)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeDto>()
                .ToList();
            foreach (var dto in employeeDtos)
            {
                string manager = dto.ManagerLastName == null ? "[no manager]" : dto.ManagerLastName;
                Console.WriteLine($"{dto.FirstName} {dto.LastName} {dto.Salary:F2} - Manager: {manager}");
            }
        }
    }
}