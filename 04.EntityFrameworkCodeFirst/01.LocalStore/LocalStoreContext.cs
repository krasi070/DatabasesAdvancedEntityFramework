namespace _01.LocalStore
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class LocalStoreContext : DbContext
    {
        public LocalStoreContext()
            : base("name=LocalStoreContext")
        {
            Database.SetInitializer(new LocalStoreInitializer());
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}