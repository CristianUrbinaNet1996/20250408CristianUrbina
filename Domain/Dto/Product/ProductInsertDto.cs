using System.ComponentModel.DataAnnotations;

namespace Domain.Dto.Product
{
    public class ProductInsertDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal? BasePrice { get; set; }


    }
}
