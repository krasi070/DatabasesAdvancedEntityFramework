namespace _02.CarDealer.Data
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

    public class CarDealerInitializer : DropCreateDatabaseAlways<CarDealerContext>
    {
        protected override void Seed(CarDealerContext context)
        {
            Random random = new Random();
            string suppliersJson = File.ReadAllText("../../CarDealer-Data/suppliers.json");
            string partsJson = File.ReadAllText("../../CarDealer-Data/parts.json");
            string carsJson = File.ReadAllText("../../CarDealer-Data/cars.json");
            string customersJson = File.ReadAllText("../../CarDealer-Data/customers.json");

            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJson);
            var parts = JsonConvert.DeserializeObject<List<Part>>(partsJson);
            foreach (var part in parts)
            {
                int index = random.Next(0, suppliers.Count - 1);
                part.Supplier = suppliers[index];
            }

            var cars = JsonConvert.DeserializeObject<List<Car>>(carsJson);
            foreach (var car in cars)
            {
                int numberOfParts = random.Next(10, 20);
                for (int i = 0; i < numberOfParts; i++)
                {
                    int partIndex = random.Next(0, parts.Count - 1);
                    if (!car.Parts.Contains(parts[partIndex]))
                    {
                        car.Parts.Add(parts[partIndex]);
                        parts[partIndex].Cars.Add(car);
                    }
                }
            }

            var customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);

            context.Suppliers.AddRange(suppliers);
            context.Parts.AddRange(parts);
            context.Cars.AddRange(cars);
            context.Customers.AddRange(customers);
            this.SaveChanges(context);

            var sales = new List<Sale>();
            int[] discountPercentages = new int[] { 0, 5, 10, 15, 20, 30, 40, 50 };
            for (int i = 0; i < 20; i++)
            {
                int discountIndex = random.Next(0, discountPercentages.Length - 1);
                int customerId = random.Next(1, context.Customers.Count());
                int carId = random.Next(1, context.Cars.Count());
                while (sales.Any(s => s.Car.Id == carId))
                {
                    carId = random.Next(1, context.Cars.Count());
                }
                
                sales.Add(new Sale()
                {
                    Customer = context.Customers.Find(customerId),
                    Car = context.Cars.Find(carId),
                    DiscountPercentage = discountPercentages[discountIndex]
                });
            }

            context.Sales.AddRange(sales);

            this.SaveChanges(context);

            base.Seed(context);
        }

        // method gotten from http://stackoverflow.com/questions/10219864/ef-code-first-how-do-i-see-entityvalidationerrors-property-from-the-nuget-pac
        private void SaveChanges(CarDealerContext context)
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