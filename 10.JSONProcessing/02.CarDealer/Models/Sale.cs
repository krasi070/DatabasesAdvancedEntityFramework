namespace _02.CarDealer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Sale
    {
        public int Id { get; set; }

        public int CarId { get; set; }  

        public virtual Car Car { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int DiscountPercentage { get; set; }
    }
}