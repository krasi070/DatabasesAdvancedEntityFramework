namespace _01.LocalStore
{
    using System.Data.Entity;
    using Models;

    public class LocalStoreInitializer : DropCreateDatabaseAlways<LocalStoreContext>
    {
        protected override void Seed(LocalStoreContext context)
        {
            var apple = new Product()
            {
                Name = "Apple",
                DistributorName = "Apple D.",
                Description = "Just an apple.",
                Price = 0.5m,
                Weight = 0.1m,
                Quantity = 1
            };

            context.Products.Add(apple);

            var chocolate = new Product()
            {
                Name = "Milka Chocolate",
                DistributorName = "Milka",
                Description = "Just chocolate.",
                Price = 3m,
                Weight = 0.15m,
                Quantity = 2
            };

            context.Products.Add(chocolate);

            var bread = new Product()
            {
                Name = "White Bread",
                DistributorName = "Filchev",
                Description = "Plain bread",
                Price = 1m,
                Weight = 0.2m,
                Quantity = 1
            };

            context.Products.Add(bread);

            base.Seed(context);
        }
    }
}