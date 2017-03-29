namespace _01.ProductShop.Data
{
    using System.Data.Entity;
    using Models;

    public class ProductShopContext : DbContext
    {
        public ProductShopContext()
            : base("name=ProductShopContext")
        {
            Database.SetInitializer(new ProductShopInitializer());
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOptional(p => p.Buyer)
                .WithMany(b => b.ProductsBought);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Seller)
                .WithMany(s => s.ProductsSold);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }
    }
}