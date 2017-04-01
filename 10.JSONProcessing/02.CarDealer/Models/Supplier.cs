namespace _02.CarDealer.Models
{
    using System.Collections.Generic;

    public class Supplier
    {
        public Supplier()
        {
            this.SuppliedParts = new HashSet<Part>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsImporter { get; set; }

        public virtual ICollection<Part> SuppliedParts { get; set; }
    }
}