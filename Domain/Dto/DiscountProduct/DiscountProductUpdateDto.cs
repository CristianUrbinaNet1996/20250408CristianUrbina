namespace Domain.Dto.DiscountProduct
{
    public class DiscountProductUpdateDto
    {



        public Guid? IdProduct { get; set; }
        public decimal? DiscountPrice { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
