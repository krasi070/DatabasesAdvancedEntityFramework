namespace _02.SalesDatabase.Models
{
    using System.Collections.Generic;

    public class StoreLocation
    {
        public StoreLocation()
        {
            this.SalesInStore = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string LocationName { get; set; }

        public virtual ICollection<Sale> SalesInStore { get; set; }
    }
}