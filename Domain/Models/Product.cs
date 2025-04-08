namespace Domain.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public decimal? BasePrice { get; set; }

    public virtual ICollection<DiscountProduct> DiscountProducts { get; set; } = new List<DiscountProduct>();
}
