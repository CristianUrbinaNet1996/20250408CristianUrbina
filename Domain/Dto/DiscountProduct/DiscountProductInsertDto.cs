using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.DiscountProduct
{
    public class DiscountProductInsertDto
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid? IdProduct { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal? DiscountPrice { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime? StartDate { get; set; }


    }
}
