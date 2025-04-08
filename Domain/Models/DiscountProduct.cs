namespace Domain.Models;

public partial class DiscountProduct
{
    public Guid Id { get; set; }

    public Guid? IdProduct { get; set; }

    public decimal? DiscountPrice { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
