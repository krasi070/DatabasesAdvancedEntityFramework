namespace _01.ProductShop.Data
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class ProductShopInitializer : DropCreateDatabaseAlways<ProductShopContext>
    {
        protected override void Seed(ProductShopContext context)
        {
            string usersJson = File.ReadAllText("../../Import-Json-Resources/users.json");
            string categoriesJson = File.ReadAllText("../../Import-Json-Resources/categories.json");
            string productsJson = File.ReadAllText("../../Import-Json-Resources/products.json");

            context.Users.AddRange(JsonConvert.DeserializeObject<User[]>(usersJson));
            context.Categories.AddRange(JsonConvert.DeserializeObject<Category[]>(categoriesJson));

            this.SaveChanges(context);

            List<Product> products = JsonConvert.DeserializeObject<Product[]>(productsJson).ToList();

            int categoryIndex = 1;
            Random random = new Random();
            foreach (var product in products)
            {
                int buyerId = random.Next(0, context.Users.Count() - 1);
                int sellerId = random.Next(1, context.Users.Count() - 1);
                if (sellerId == buyerId)
                {
                    sellerId = buyerId - 1 > 0 ? buyerId - 1 : buyerId + 1;
                }

                User buyer = null;
                if (buyerId != 0)
                {
                    buyer = context.Users.FirstOrDefault(u => u.Id == buyerId);
                }

                User seller = context.Users.FirstOrDefault(u => u.Id == sellerId);

                product.Buyer = buyer;
                product.Seller = seller;
                product.Categories.Add(context.Categories.FirstOrDefault(c => c.Id == categoryIndex));
                categoryIndex = (categoryIndex + 1) % (context.Categories.Count() + 1);
                product.Categories.Add(context.Categories.FirstOrDefault(c => c.Id == categoryIndex));
                categoryIndex = (categoryIndex + 1) % (context.Categories.Count() + 1);
                product.Categories.Add(context.Categories.FirstOrDefault(c => c.Id == categoryIndex));
                categoryIndex = (categoryIndex + 1) % (context.Categories.Count() + 1);
            }

            context.Products.AddRange(products);
            this.SaveChanges(context);

            base.Seed(context);
        }

        // method gotten from http://stackoverflow.com/questions/10219864/ef-code-first-how-do-i-see-entityvalidationerrors-property-from-the-nuget-pac
        private void SaveChanges(ProductShopContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}