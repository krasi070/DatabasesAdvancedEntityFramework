namespace _02.SalesDatabase.Models
{
    using System;

    public class Sale
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int StoreLocationId { get; set; }

        public StoreLocation StoreLocation { get; set; }

        public DateTime Date { get; set; }
    }
}