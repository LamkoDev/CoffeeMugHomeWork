using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationProduct.Models
{
    public class ProductCreateRequestDto
    {
        [Required(ErrorMessage = "Prosze podać nazwę")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [RegularExpression(@"^[0-9]*\,?[0-9]+$", ErrorMessage = "Enter a valid price")]
        [Required(ErrorMessage = "Prosze podać cenę")]
        public decimal Price { get; set; }

    }
}
