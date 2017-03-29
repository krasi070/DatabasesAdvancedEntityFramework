namespace _01.ProductShop.DTOs
{
    using System.Collections.Generic;

    public class SellerDto
    {
        public SellerDto()
        {
            this.SoldProducts = new HashSet<SoldProductDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<SoldProductDto> SoldProducts { get; set; }
    }
}