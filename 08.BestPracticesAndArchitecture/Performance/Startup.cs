namespace Performance
{
    using Data;
    using Models;
    using System;
    using System.Diagnostics;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            EmployeeContext context = new EmployeeContext();
            Stopwatch stopwatch = new Stopwatch();
            long timePassed = 0L;
            int testCount = 10; // Amount of tests to perform
            for (int i = 0; i < testCount; i++)
            {
                // Clear all query cache
                context.Database.ExecuteSqlCommand("CHECKPOINT; DBCC DROPCLEANBUFFERS;");
                stopwatch.Start();

                // TODO: Method to execute query
                OptimizeQuery(context);
                stopwatch.Stop();
                timePassed += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
            }

            TimeSpan averageTimePassed = TimeSpan.FromMilliseconds(timePassed / (double)testCount);
            Console.WriteLine(averageTimePassed);
        }

        private static void QueryWithEagerLoading(EmployeeContext context)
        {
            var employees = context.Employees
                .Include("Department")
                .Where(e => e.EmployeesProjects.Count == 1)
                .Select(e => new
                {
                    Name = e.FirstName,
                    DepartmentName = e.Department.Name
                })
                .ToList();

            foreach (var e in employees)
            {
                string result = $"{e.Name} - {e.DepartmentName}";
            }
        }

        private static void QueryWithLazyLoading(EmployeeContext context)
        {
            var employees = context.Employees
                .Where(e => e.EmployeesProjects.Count == 1)
                .Select(e => new
                {
                    Name = e.FirstName,
                    DepartmentName = e.Department.Name
                })
                .ToList();

            foreach (var e in employees)
            {
                string result = $"{e.Name} - {e.DepartmentName}";
            }
        }

        private static void OrderByToList(EmployeeContext context)
        {
            var employees = context.Employees
                .Include("Department")
                .OrderBy(e => e.Department.Name)
                .ThenBy(e => e.FirstName)
                .ToList();
        }

        private static void ToListOrderBy(EmployeeContext context)
        {
            var employees = context.Employees
                .Include("Department")
                .ToList()
                .OrderBy(e => e.Department.Name)
                .ThenBy(e => e.FirstName);
        }

        private static void OptimizeQuery(EmployeeContext context)
        {
            var employees = context.Employees
              .Distinct()
              .Where(e => e.Subordinates.Any(s => s.Address.Town.Name.StartsWith("B")))
              .ToList();

            foreach (Employee e in employees)
            {
                string result = $"{e.FirstName}";
            }
        }
    }
}