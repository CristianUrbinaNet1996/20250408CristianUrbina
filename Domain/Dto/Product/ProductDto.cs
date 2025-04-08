namespace Domain.Dto.Product;

public partial class ProductDto
{


    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal? BasePrice { get; set; }

    public decimal DiscountPrice { get; set; }
}
