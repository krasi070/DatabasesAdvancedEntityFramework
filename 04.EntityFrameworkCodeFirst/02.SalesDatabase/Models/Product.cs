namespace _02.SalesDatabase.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.SalesOfProduct = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Sale> SalesOfProduct { get; set; }
    }
}