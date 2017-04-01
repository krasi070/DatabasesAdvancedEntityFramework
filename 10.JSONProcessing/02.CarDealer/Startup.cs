namespace _02.CarDealer
{
    using Data;
    using Newtonsoft.Json;
    using System;
    using System.Linq;

    public class Startup
    {
        // Problem 05 - Car Dealer Import Data (in CarDealerInitializer)
        public static void Main()
        {
            //CarDealerContext conetext = new CarDealerContext();
            //conetext.Database.Initialize(true);

            PrintSalesWithDiscount();
        }

        // Problem 06 - Car Dealer Query and Export Data
        // Query 1 – Ordered Customers
        private static void PrintCustomers()
        {
            using (var context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.BirthDate,
                        c.IsYoungDriver,
                        Sales = c.Sales.Select(s => new
                        {
                            Customer = s.Customer.Name,
                            Car = s.Car.Make + " " + s.Car.Model,
                            Discount = s.DiscountPercentage + "%"
                        })
                    })
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver);

                Console.WriteLine(JsonConvert.SerializeObject(customers, Formatting.Indented));
            }
        }

        // Query 2 – Cars from make Toyota
        private static void PrintToyotaCars()
        {
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.Make == "Toyota")
                    .Select(c => new
                    {
                        c.Id,
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    })
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance);

                Console.WriteLine(JsonConvert.SerializeObject(cars, Formatting.Indented));
            }
        }

        // Query 3 – Local Suppliers
        private static void PrintLocalSuppliers()
        {
            using (var context = new CarDealerContext())
            {
                var localSuppliers = context.Suppliers
                    .Where(s => s.IsImporter == false)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        PartsCount = s.SuppliedParts.Count
                    })
                    .OrderByDescending(s => s.PartsCount);

                Console.WriteLine(JsonConvert.SerializeObject(localSuppliers, Formatting.Indented));
            }
        }

        // Query 4 – Cars with Their List of Parts
        private static void PrintCarsWithParts()
        {
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Select(c => new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance,
                        Parts = c.Parts.Select(p => new
                        {
                            p.Name,
                            p.Price
                        })
                    });

                Console.WriteLine(JsonConvert.SerializeObject(cars, Formatting.Indented));
            }
        }

        // Query 5 – Total Sales by Customer
        private static void PrintCustomersWithCars()
        {
            using (var context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count > 0)
                    .Select(c => new
                    {
                        FullName = c.Name,
                        BoughtCars = c.Sales.Count,
                        SpentMoney = c.Sales.Select(s => s.Car.Parts.Sum(p => p.Price)).Sum()
                    })
                    .OrderByDescending(c => c.SpentMoney)
                    .ThenByDescending(c => c.BoughtCars);

                Console.WriteLine(JsonConvert.SerializeObject(customers, Formatting.Indented));
            }
        }

        // Query 6 – Sales with Applied Discount
        private static void PrintSalesWithDiscount()
        {
            using (var context = new CarDealerContext())
            {
                var sales = context.Sales
                    .Select(s => new
                    {
                        Car = new
                        {
                            s.Car.Make,
                            s.Car.Model,
                            s.Car.TravelledDistance
                        },
                        CustomerName = s.Customer.Name,
                        Discount = s.DiscountPercentage * 0.01m,
                        Price = s.Car.Parts.Sum(p => p.Price),
                        PriceWithDiscount = s.Car.Parts.Sum(p => p.Price) * (1 - s.DiscountPercentage * 0.01m)
                    });

                Console.WriteLine(JsonConvert.SerializeObject(sales, Formatting.Indented));
            }
        }
    }
}