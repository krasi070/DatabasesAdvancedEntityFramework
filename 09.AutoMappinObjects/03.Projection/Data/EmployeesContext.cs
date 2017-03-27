namespace _03.Projection.Data
{
    using System.Data.Entity;
    using Models;

    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
            : base("name=EmployeesContext")
        {
            Database.SetInitializer(new EmployeeInitializer());
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}