namespace _03.Projection.Data
{
    using Models;
    using System;
    using System.Data.Entity;

    public class EmployeeInitializer : DropCreateDatabaseAlways<EmployeesContext>
    {
        protected override void Seed(EmployeesContext context)
        {
            Employee manager1 = new Employee()
            {
                FirstName = "Mike",
                LastName = "Lawson",
                Salary = 100234m,
                Birthday = new DateTime(1970, 11, 1),
                Address = "Not real str. 1"
            };

            Employee manager2 = new Employee()
            {
                FirstName = "Bob",
                LastName = "Flanders",
                Salary = 50234m,
                Birthday = new DateTime(1975, 7, 6),
                Address = "Not real str. 10"
            };

            Employee employee1 = new Employee()
            {
                FirstName = "John1",
                LastName = "Simpson1",
                Salary = 12123.00m,
                Birthday = new DateTime(1980, 12, 1),
                Address = "Not real str. 2",
                ManagerId = 1,
                Manager = manager1
            };

            Employee employee2 = new Employee()
            {
                FirstName = "John2",
                LastName = "Simpson2",
                Salary = 12123.00m,
                Birthday = new DateTime(1980, 12, 1),
                Address = "Not real str. 3",
                ManagerId = 2,
                Manager = manager2
            };

            Employee employee3 = new Employee()
            {
                FirstName = "John3",
                LastName = "Simpson3",
                Salary = 12123.00m,
                Birthday = new DateTime(1980, 12, 1),
                Address = "Not real str. 4",
                ManagerId = 1,
                Manager = manager1
            };

            context.Employees.Add(manager1);
            context.Employees.Add(manager2);
            context.Employees.Add(employee1);
            context.Employees.Add(employee2);
            context.Employees.Add(employee3);
            
            context.SaveChanges();

            base.Seed(context);
        }
    }
}