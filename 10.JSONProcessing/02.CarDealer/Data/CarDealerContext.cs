namespace _02.CarDealer.Data
{
    using Models;
    using System.Data.Entity;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
            : base("name=CarDealerContext")
        {
            Database.SetInitializer(new CarDealerInitializer());
        }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<Part> Parts { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Car)
                .WithOptional(c => c.Sale);

            base.OnModelCreating(modelBuilder);
        }
    }
}