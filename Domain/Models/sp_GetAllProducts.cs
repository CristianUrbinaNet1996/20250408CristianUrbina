namespace Domain.Models
{
    public class sp_GetAllProducts
    {

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public Guid? DiscountPrice { get; set; }

    }
}
