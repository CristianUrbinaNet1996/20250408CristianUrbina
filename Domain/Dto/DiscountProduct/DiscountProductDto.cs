namespace Domain.Dto.DiscountProduct;

public partial class DiscountProductDto
{


    public Guid? IdProduct { get; set; }

    public string? Product { get; set; }

    public decimal? DiscountPrice { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }


}
