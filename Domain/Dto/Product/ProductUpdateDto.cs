namespace Domain.Dto.Product
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public decimal? BasePrice { get; set; }
    }
}
