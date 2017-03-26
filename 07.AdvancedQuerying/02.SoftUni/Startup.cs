namespace _02.SoftUni
{
    using Data;
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using ViewModels;

    public class Startup
    {
        public static void Main()
        {
            PrintMaxSalariesByDepartment();
        }

        // Problem 17 - Call a Stored Procedure
        private static void PrintProjectsByEmployee()
        {
            Console.Write("Employee: ");
            string[] args = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            string firstName = args[0];
            string lastName = args[1];
            SqlParameter firstNameParam = new SqlParameter("@FirstName", firstName);
            SqlParameter lastNameParam = new SqlParameter("@LastName", lastName);

            using (var context = new SoftUniContext())
            {
                var projects = context.Database
                    .SqlQuery<ProjectViewModel>("EXEC usp_GetProjectsByEmployee @FirstName, @LastName", firstNameParam, lastNameParam);
                foreach (var project in projects)
                {
                    Console.WriteLine($"{project.Name} - {project.Description.Substring(0, 20)}... - {project.StartDate}");
                }
            }
        }

        // Problem 18 - Employees Maximum Salaries
        private static void PrintMaxSalariesByDepartment()
        {
            using (var context = new SoftUniContext())
            {
                var departments = context.Departments
                    .Select(d => new
                    {
                        d.Name,
                        MaxSalary = d.Employees.Select(e => e.Salary).Max()
                    })
                    .Where(d => d.MaxSalary < 30000 || d.MaxSalary > 70000);

                foreach (var department in departments)
                {
                    Console.WriteLine($"{department.Name} - {department.MaxSalary:F2}");
                }
            }
        } 
    }
}