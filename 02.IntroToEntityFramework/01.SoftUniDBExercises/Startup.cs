namespace _01.SoftUniDBExercises
{
    using System;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using _01.SoftUniDBExercises.Models;

    public class Startup
    {
        public static void Main(string[] args)
        {
            SoftUniContext context = new SoftUniContext();

            CompareNativeQueryAndLinq();
        }

        // Problem 03 - Empoyees Full Information
        private static void PrintAllEmployees(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.EmployeeID,
                    e.FirstName,
                    e.MiddleName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .OrderBy(e => e.EmployeeID);

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary}");
            }
        }

        // Problem 04 - Employees with Salary Over 50 000
        private static void PrintEmployeesWithHighSalary(SoftUniContext context, int salary)
        {
            var employeesWithSalaryOver50000 = context.Employees
                .Where(e => e.Salary > salary)
                .Select(e => e.FirstName);

            foreach (var employee in employeesWithSalaryOver50000)
            {
                Console.WriteLine(employee);
            }
        }

        // Problem 05 - Employees from Research and Development
        private static void PrintEmployeesFromDepartment(SoftUniContext context, string departmentName)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == departmentName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }
        }

        // Problem 06 - Adding a New Address and Updating Employee
        private static void AddAddressToEmployeeWithLastName(SoftUniContext context, string text, int townId, string lastName)
        {
            var employee = context.Employees
                .FirstOrDefault(e => e.LastName == lastName);
            if (employee == null)
            {
                return;
            }

            employee.Address = new Address()
            {
                AddressText = text,
                TownID = townId
            };

            context.SaveChanges();
        }

        private static void PrintEmployeeAddresses(SoftUniContext context)
        {
            var addresses = context.Employees
                .OrderByDescending(e => e.AddressID)
                .Take(10)
                .Select(e => e.Address.AddressText);

            foreach (var address in addresses)
            {
                Console.WriteLine(address);
            }
        }

        // Problem 07 - Find Employees in Period
        private static void PrintEmployeesWithProjectsStartedInPeriod(SoftUniContext context, int startYear, int endYear)
        {
            var employees = context.Employees
                .Where(e => e.Projects
                    .Any(p => p.StartDate.Year >= startYear && p.StartDate.Year <= endYear))
                .Take(30)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    e.Projects
                });

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.ManagerFirstName}");
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine($"--{project.Name} {project.StartDate} {project.EndDate}");
                }
            }
        }

        // Problem 08 - Addresses by Town Name
        private static void PrintAddresses(SoftUniContext context)
        {
            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    NumberOfEmployees = a.Employees.Count
                });

            foreach (var address in addresses)
            {
                Console.WriteLine($"{address.AddressText}, {address.TownName} - {address.NumberOfEmployees} employees");
            }
        }

        // Problem 09 - Employee with ID 147
        private static void PrintEmployeeById(SoftUniContext context, int id)
        {
            var employee = context.Employees
                .FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null)
            {
                Console.WriteLine($"Employee with ID {id} doesn't exist!");
            }

            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            foreach (var project in employee.Projects.OrderBy(p => p.Name))
            {
                Console.WriteLine(project.Name);
            }
        }

        // Problem 10 - Departments with more than 5 Employees
        private static void PrintDepartmentsWithEmployees(SoftUniContext context, int employeesCount)
        {
            var departments = context.Departments
                .Where(d => d.Employees.Count > employeesCount)
                .OrderBy(d => d.Employees.Count);

            foreach (var department in departments)
            {
                Console.WriteLine($"{department.Name} {department.Manager.FirstName}");
                foreach (var employee in department.Employees)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                }
            }
        }

        // Problem 11 - Find Latest 10 Projects
        private static void PrintLastProjects(SoftUniContext context, int amount)
        {
            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(amount)
                .OrderBy(p => p.Name);

            foreach (var project in projects)
            {
                string endDate = "";
                if (project.EndDate != null)
                {
                    endDate = ((DateTime)project.EndDate).ToString("M/d/yyyy h:mm:ss tt");
                }

                Console.WriteLine($"{project.Name} {project.Description} {project.StartDate.ToString("M/d/yyyy h:mm:ss tt")} {endDate}");
            }
        }

        // Problem 12 - Increase Salaries
        private static void IncreaseSalaryOfEmployeesFromDepartment(SoftUniContext context, int percentage)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering" || 
                    e.Department.Name == "Tool Design" || 
                    e.Department.Name == "Marketing" ||
                    e.Department.Name == "Information Services");

            foreach (var employee in employees)
            {
                employee.Salary = employee.Salary * (1 + percentage * 0.01m);
                Console.WriteLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F6})");
            }

            context.SaveChanges();
        }

        // Problem 13 - Find Employees by First Name Starting with ‘SA’
        private static void PrintEmployeesWhoseFirstNameStartsWith(SoftUniContext context, string str)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.ToLower().StartsWith(str.ToLower()));

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F4})");
            }
        }

        // Problem 15 - Delete Project by ID
        private static void DeleteProjectById(SoftUniContext context, int id)
        {
            var projectToRemove = context.Projects.First(p => p.ProjectID == id);
            foreach (var employee in projectToRemove.Employees)
            {
                employee.Projects.Remove(projectToRemove);
            }

            context.Projects.Remove(projectToRemove);

            context.SaveChanges();
        }

        private static void PrintProjects(SoftUniContext context, int amount)
        {
            var projects = context.Projects.Take(amount);
            foreach (var project in projects)
            {
                Console.WriteLine($"{project.Name}");
            }
        }

        // Problem 16 - Remove Towns
        private static void RemoveTown(SoftUniContext context)
        {
            Console.Write("Town: ");
            string town = Console.ReadLine();

            var townToRemove = context.Towns.FirstOrDefault(t => t.Name == town);
            if (townToRemove == null)
            {
                return;
            }

            int count = townToRemove.Addresses.Count;
            var addresses = townToRemove.Addresses.ToList();
            foreach (var address in addresses)
            {
                var employees = context.Employees.Where(e => e.AddressID == address.AddressID);
                foreach (var employee in employees)
                {
                    employee.AddressID = null;
                    employee.Address = null;
                }

                context.Addresses.Remove(address);
            }

            context.Towns.Remove(townToRemove);

            context.SaveChanges();

            string addressStr = count == 1 ? "address" : "addresses";
            string isStr = count == 1 ? "was" : "were";
            Console.WriteLine($"{count} {addressStr} in {town} {isStr} deleted");
        }

        // Problem 17 - Native SQL Query
        private static void CompareNativeQueryAndLinq()
        {
            var timer = new Stopwatch();
            timer.Start();
            PrintNamesWithNativeQuery();
            timer.Stop();
            Console.WriteLine($"Native: {timer.Elapsed}");

            timer.Restart();
            PrintNamesWithNativeQuery();
            timer.Stop();
            Console.WriteLine($"LINQ: {timer.Elapsed}");
        }

        private static void PrintNamesWithNativeQuery()
        {
            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();

            using (connection)
            {
                string query = @"USE SoftUni
                SELECT e.FirstName
                FROM Employees AS e
                INNER JOIN EmployeesProjects AS ep
                ON e.EmployeeID = ep.EmployeeID
                INNER JOIN Projects AS p
                ON ep.ProjectID = p.ProjectID
                WHERE YEAR(p.StartDate) = 2002";
                SqlCommand getEmployeesCmd = new SqlCommand(query, connection);
                SqlDataReader reader = getEmployeesCmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader[0]);
                }

                reader.Close();
            }
        }

        private static void PrintNamesWithLinq()
        {
            SoftUniContext context = new SoftUniContext();

            var projects = context.Projects
                .Where(p => p.StartDate.Year == 2002);
            foreach (var project in projects)
            {
                foreach (var employee in project.Employees)
                {
                    Console.WriteLine($"{employee.FirstName}");
                }
            }
        }
    }
}